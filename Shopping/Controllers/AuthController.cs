using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Repo.IRepo;

namespace Shopping.Controllers
{
    // [Route("api/[controller]")]
    [Authorize]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo authRepo;

        public AuthController(IAuthRepo _authRepo)
        {
            authRepo = _authRepo;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthModal model)
        {
            var user = authRepo.Authenticate(model.Email, model.Password);
            if(user == null)
            {
                return BadRequest(new { message = "Email or Password is incorrect" });
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserModal model)
        {
            bool isUserUnique = authRepo.IsUniqueUser(model.Email);
            if (!isUserUnique)
            {
                return BadRequest(new { message = "User already exists" });
            }
            var user = authRepo.Register(model.Name, model.Email, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "An Erro occured" });
            }
            return Ok(user);
        }

    }
}
