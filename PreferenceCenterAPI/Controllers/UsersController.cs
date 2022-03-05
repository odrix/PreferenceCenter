using Microsoft.AspNetCore.Mvc;
using PreferenceCenterAPI.Models;
using PreferenceCenterAPI.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreferenceCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<Users>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var userPreference = _userService.Get(id);
            if (userPreference == null)
                return NotFound();

            return new JsonResult(userPreference);
        }

        [HttpGet("{email}")]
        public IActionResult Get(string email)
        {
            var userPreference = _userService.Get(email);
            if (userPreference == null)
                return NotFound();

            return new JsonResult(userPreference);
        }

        [HttpPost]
        public IActionResult Post([FromBody]string email)
        {
            try
            {
                UserPreference createdUser = _userService.Add(email);
                return StatusCode((int)HttpStatusCode.Created, createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _userService.Delete(id);
        }

        [HttpDelete("{email}")]
        public void Delete(string email)
        {
            _userService.Delete(email);
        }
    }
}
