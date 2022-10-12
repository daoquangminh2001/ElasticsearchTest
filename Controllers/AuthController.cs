using System;
using System.Threading.Tasks;
using ElasticsearchTest.Input;
using ElasticsearchTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElasticsearchTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpGet("getalluser")/*, Authorize("admin")*/]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                
                return Ok(_auth.GetUsers());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("createuser")]
        public async Task<IActionResult> Create(CreateUserInput input)
        {
            
            try
            {
                var resutl= _auth.Create_Use(input);
                return Ok(resutl);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}