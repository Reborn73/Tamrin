using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Tamrin.Common;
using Tamrin.Common.Exceptions;
using Tamrin.Common.Utilities;
using Tamrin.Data.Contracts;
using Tamrin.Entities.Common;

namespace Tamrin.WebFramework.Api
{
    public class CrudController<TRequestDto, TResponseDto, TEntity> : BaseController
    where TRequestDto : BaseDto<TRequestDto, TEntity, long>, new()
    where TResponseDto : BaseDto<TResponseDto, TEntity, long>, new()
    where TEntity : class, IBaseEntity, new()
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public CrudController(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<TResponseDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            if (list == null || list.Count == 0)
                throw new AppException(ApiResultStatusCode.ListEmpty, ApiResultStatusCode.ListEmpty.ToDisplay());

            return Ok(list);
        }

        [HttpGet("{id:long}")]
        public virtual async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            var result = await _repository.TableNoTracking.ProjectTo<TResponseDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

            if (result == null)
                throw new NotFoundException(ApiResultStatusCode.NotFound.ToDisplay());

            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TRequestDto dto, CancellationToken cancellationToken)
        {
            var entity = dto.ToEntity(_mapper);
            await _repository.AddAsync(entity, cancellationToken);
            var resultRequestDto = await _repository.TableNoTracking.ProjectTo<TResponseDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(e => e.Id == entity.Id, cancellationToken);
            return Ok(resultRequestDto);
        }

        [HttpPut("{id:long}")]
        public virtual async Task<IActionResult> Update(long id, TRequestDto dto, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(cancellationToken, id);

            if (entity == null)
                throw new NotFoundException(ApiResultStatusCode.NotFound.ToDisplay());

            entity = dto.ToEntity(_mapper, entity);
            await _repository.UpdateAsync(entity, cancellationToken);
            var resultRequestDto = await _repository.TableNoTracking.ProjectTo<TResponseDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(e => e.Id == entity.Id, cancellationToken);
            return Ok(resultRequestDto);
        }

        [HttpDelete("{id:long}")]
        public virtual async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(cancellationToken, id);

            if (entity == null)
                throw new NotFoundException(ApiResultStatusCode.NotFound.ToDisplay());

            await _repository.DeleteAsync(entity, cancellationToken);
            return Ok();
        }
    }
}
