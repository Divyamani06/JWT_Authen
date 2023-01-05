using JWT_Authen.Model;
using JWT_Authen.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT_Authen.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
       
        private readonly IJwtRepository _iJwtRepso;

        public NameController(IJwtRepository jwtRepository)
        {
           
            _iJwtRepso = jwtRepository;


        }


        //Signup API
        [AllowAnonymous]
        [HttpPost("SignUpUser")]
        public async Task<IActionResult> AddSignUpUser(UserCrud userCrud)
        {
            
            var token = await _iJwtRepso.SignUpAsync(userCrud);
            return Ok(token);
        }

        //Login API
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(UserCrud userCrud)
        {
            var post = await _iJwtRepso.LogInAsync(userCrud);
           
            return Ok(post);
        }

    }
}
