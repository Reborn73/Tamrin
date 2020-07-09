using Tamrin.Common;
using Tamrin.Data.Contracts;
using Tamrin.Entities.Course;

namespace Tamrin.Data.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository, IScopedDependency
    {
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
