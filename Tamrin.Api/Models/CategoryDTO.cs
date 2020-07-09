using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using Tamrin.Entities.Course;
using Tamrin.WebFramework.Api;

namespace Tamrin.Api.Models
{
    public class CrudCategoryRequestDto : BaseDto<CrudCategoryRequestDto, Category>
    {
        [Display(Name = "دسته بندی پدر")]
        public long? ParentId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} را وارد نمایید.", AllowEmptyStrings = false)]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public string Name { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد نمایید.", AllowEmptyStrings = false)]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public string Title { get; set; }
    }

    public class CrudCategoryResponseDto : BaseDto<CrudCategoryResponseDto, Category>
    {
        public long? ParentId { get; set; }
        public string ParentCategoryName { get; set; }
        public string ParentCategoryTitle { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime LastUpdateDateTime { get; set; }

        public override void CustomMappings(IMappingExpression<Category, CrudCategoryResponseDto> mapping)
        {
            mapping.ForMember(
                    dest => dest.LastUpdateDateTime,
                    config => config.MapFrom(src => src.LastUpdateDateTime ?? src.CreateDateTime)
                );
        }
    }
}
