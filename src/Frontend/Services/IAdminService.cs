using System.Threading.Tasks;

namespace Frontend.Services
{

    public interface IAdminService
    {
        Task<bool> AllowAdminUserCreationAsync();
    }

}
