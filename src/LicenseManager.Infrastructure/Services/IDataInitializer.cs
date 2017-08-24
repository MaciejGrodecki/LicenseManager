using System.Threading.Tasks;

namespace LicenseManager.Infrastructure.Services
{
    public interface IDataInitializer
    {
         Task SeedAsync();
    }
}