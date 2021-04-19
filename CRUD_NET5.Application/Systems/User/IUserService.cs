using CRUD_NET5.ViewModels.Common;
using CRUD_NET5.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_NET5.Application.Systems.User
{
    public interface IUserService
    {
        Task<APIResult<string>> Authenticate(LoginRequest request);
        Task<APIResult<bool>> RegisterUser(RegisterRequest request);
        Task<APIResult<List<UserVM>>> GetAll();
    }
}
