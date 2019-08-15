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
    // [Authorize]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext db;
        public ValuesController(RoleManager<IdentityRole> rm, IHttpContextAccessor contextAccessor, SignInManager<ApplicationUser> signInMgt, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {

            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _signInManager = signInMgt;
            _roleManager = rm;
            db = dbContext;
        }


        // GET api/values
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<string>>> GetAsync()
        {
            // var name = _contextAccessor.HttpContext.User.Claims.First(c => c.Type == "user_id").Value;

            // var newPine = new Pineapple();
            // newPine.name = "my new pineapple";
            // db.Pineapples.Add(newPine);
            // db.SaveChanges();

            // var role = "admin";
            // if (!await _roleManager.RoleExistsAsync(role))
            // {
            //     var create = await _roleManager.CreateAsync(new IdentityRole(role));

            //     if (!create.Succeeded)
            //     {
            //         throw new Exception("Failed to create role");
            //     }
            // }

            // ApplicationUser user = await _userManager.FindByNameAsync(name);

            // await _signInManager.SignInAsync(user,true);
            return new string[] { "value1", "value2" };
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
