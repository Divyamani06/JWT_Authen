using JWT_Authen.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JWT_Authen.Repository
{
    public interface IJwtRepository
    {
        
        Task<List<UserCrud>> GetUserCrud();
        Task<string> LogInAsync(UserCrud userCrud);
        Task<string> SignUpAsync(UserCrud userCrud);
    }
}
