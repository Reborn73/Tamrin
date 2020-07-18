using Tamrin.Common;

namespace Tamrin.Services.DataInitializer
{
    public interface IDataInitializer : IScopedDependency
    {
        void InitializeData();
    }
}