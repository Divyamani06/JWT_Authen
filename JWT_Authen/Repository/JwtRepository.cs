using JWT_Authen.Context;
using JWT_Authen.Model;
using JWT_Authen.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JWT_Authen.Repository
{
    public class JwtRepository : IJwtRepository
    {



        private readonly ApplicationDbContext _context;
        private readonly JWTToken _jwtToken;
       

        public JwtRepository(ApplicationDbContext applicationDbContext, IOptions<JWTToken> options)
        {

            _context = applicationDbContext;
            _jwtToken = options.Value;
          

        }

        /// <summary>
        /// Sigin 
        /// </summary>
        /// <param name="Signup"></param>
        /// <returns></returns>

        public async Task<string> SignUpAsync(UserCrud userCrud)
        {
            //Encryption Password
            var encryped = EncryptPassword(userCrud.Password);

            var token = new UserCrud
            {
                Email = userCrud.Email,
                Password = encryped 
            };
            //Checking  User exited
            var data = await _context.UserCruds.Where(x => x.Email == userCrud.Email).ToListAsync();

            if (data.Count != 0)
            {
                return "User Already Exit";
            }

            _context.UserCruds.Add(token);

            //JWT creation
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.UTF8.GetBytes(_jwtToken.Token);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name,token.Email)
                    }),
                Expires = DateTime.UtcNow.AddYears(5),

                SigningCredentials = new SigningCredentials
                                   (new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };

            var tokens = tokenHandler.CreateToken(tokenDescriptor);
            var fianleToken = tokenHandler.WriteToken(tokens);
            await _context.SaveChangesAsync();
            return fianleToken;
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        public async Task<string> LogInAsync(UserCrud userCrud)
        {
            //Encryption Password
            var encrrypted = EncryptPassword(userCrud.Password);

            //Checking Email and Password
            var userlogin = await _context.UserCruds.FirstOrDefaultAsync(x => x.Email == userCrud.Email);
            if (userlogin == null)
            {
                return "user not found";

            }

            
            if (userlogin.Password != encrrypted)
            {
                return "password missmatch";
            }
           
            //JWT Creation
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_jwtToken.Token);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name,userCrud.Email)
                    }),
                Expires = DateTime.UtcNow.AddYears(5),

                SigningCredentials = new SigningCredentials
                                   (new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };
            var tokens = tokenHandler.CreateToken(tokenDescriptor);
            var fianleToken = tokenHandler.WriteToken(tokens);
            return fianleToken;


        }

        /// <summary>
        /// Get 
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserCrud>> GetUserCrud()
        {
            var get = await _context.UserCruds.Select(x => new UserCrud()
            {
                Id = x.Id,
                Email = x.Email,
                Password = x.Password
            }).ToListAsync();
            return get;
        }

        public static string EncryptPassword(string password)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        /*public async Task<UserCrud>  DeleteUserCrud(int id)
        {
            var result = await _context.UserCruds.FindAsync(id);
            _context.UserCruds.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }
        */

    }

}
