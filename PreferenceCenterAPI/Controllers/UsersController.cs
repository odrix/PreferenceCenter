using Microsoft.AspNetCore.Mvc;
using PreferenceCenterAPI.Domain;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreferenceCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
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
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.UnprocessableEntity, ex.Message);
            }
        }


        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            if (_userService.Delete(id))
                return Ok();
            else
                return NoContent();
        }

        [HttpDelete("{email}")]
        public IActionResult Delete(string email)
        {
            if (_userService.Delete(email))
                return Ok();
            else
                return NoContent();
        }
    }
}
