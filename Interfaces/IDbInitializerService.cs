using System.Threading.Tasks;

namespace HardwareStore.Interfaces
{
    public interface IDbInitializerService
    {
        void Initialize();
        Task SeedData();
    }
}
