using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvcWithAuth.Data;

namespace testWebAPIFB.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext db;
        public ValuesController(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {

            _contextAccessor = contextAccessor;
            _userManager = userManager;
            db = dbContext;
        }


        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync()
        {
            var name = _contextAccessor.HttpContext.User.Claims.First(c => c.Type == "user_id").Value;

            var newPine = new Pineapple();
            newPine.name = "my new pineapple";
            db.Pineapples.Add(newPine);
            db.SaveChanges();

            if (await _userManager.FindByNameAsync(name) == null)
            {
                var user = new ApplicationUser(name);
                user.Email = "fsdfas@fsda.org";
                user.customField = "haha";

                var newPineapple = new Pineapple()
                {
                    name = "random name of things " + name
                };
                user.Pineapples = new List<Pineapple>();
                user.Pineapples.Add(newPineapple);
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
