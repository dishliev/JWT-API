using JWT_API.Data;
using JWT_API.Helpers;
using JWT_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWT_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly JwtSettings jwtSettings;

        public UserController(DatabaseContext context, JwtSettings jwtSettings)
        {
            _context = context;
            this.jwtSettings = jwtSettings;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes =
            Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        public IActionResult GetToken(UserLogin userLogins)
        {
            var token = new UserToken();
            var valid = _context.Users.Any(x => x.UserName == userLogins.UserName);
            if (valid)
            {
                var user = _context.Users.FirstOrDefault(x => x.UserName == userLogins.UserName);
                string password = user.Password;
                if (!PasswordHash.ValidatePassword(userLogins.Password, password))
                {
                    return BadRequest($"wrong password");
                }
                else
                {
                    token = JwtHelpers.GenTokenkey(new UserToken()
                    {
                        GuidId = Guid.NewGuid(),
                        UserName = user.UserName,
                        Id = user.Id,
                    }, jwtSettings);
                }
            }
            else
            {
                return BadRequest($"wrong password");
            }

            object o = new { token = token.Token };
            return Ok(o);
        }
    }
}