using CRUD_NET5.Application.Systems.User;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRUD_NET5.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _userService.GetAll();
            return Ok(user);
        }

  
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

    
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

      
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

      
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
