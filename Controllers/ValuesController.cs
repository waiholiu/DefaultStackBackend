using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace testWebAPIFB.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public ValuesController(IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager)
        {

            _contextAccessor = contextAccessor;
            _userManager = userManager;

        }


        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync()
        {
            var name = _contextAccessor.HttpContext.User.Claims.First(c => c.Type == "user_id").Value;



            if (await _userManager.FindByNameAsync(name) == null)
            {
                var user = new IdentityUser(name);
                user.Email = "fsdfas@fsda.org";
                await _userManager.CreateAsync(user);
            }
            return new string[] { name, "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
