using JWT_Authen.Model;
using JWT_Authen.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT_Authen.Controllers
{
    [Route("api/[controller]")]
    [ApiController,Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IJwtRepository _jwtRepository;

        public ProductController(IJwtRepository iJwtRepository)
        {
            _jwtRepository = iJwtRepository;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetUserCrudAsync()
        {
            var getUserCrud = await _jwtRepository.GetUserCrud();

            return Ok(getUserCrud);

           

        }
        /*[HttpDelete]
        public async Task<IActionResult> DeleteUserCrud(int id)
        {
            var reslt = await _jwtRepository.DeleteUserCrud(id);
            return Ok(reslt);
        }*/

        
        /*[HttpPost("Signin")]
        
        public async Task<IActionResult> SigninUserCrud(string Email)
        {
            var post = await _jwtRepository.UserCrudSignin(Email);
            
            return  Ok(post);
        }*/
       

    }
}
