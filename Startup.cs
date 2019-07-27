using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcWithAuth.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Security.Claims;

namespace mvcWithAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        Configuration.GetConnectionString("DefaultConnection")));

            // this is for DI purposes
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services
                // .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddAuthentication(cfg =>
                    {
                        cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/abtestinghost";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/abtestinghost",
                        ValidateAudience = true,
                        ValidAudience = "abtestinghost",
                        ValidateLifetime = true
                    };

                    options.Events = new JwtBearerEvents
                    {

                        OnTokenValidated = async ctx =>
                        {
                            // grabs the unique id from firebase
                            var name = ctx.Principal.Claims.First(c => c.Type == "user_id").Value;

                            //Get userManager out of DI
                            var _userManager = ctx.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();

                            // retrieves the roles that the user has
                            ApplicationUser user = await _userManager.FindByNameAsync(name);

                            if (user == null)
                            {
                                user = new ApplicationUser(name);
                                user.Email = "fsdfas@fsda.org";
                                user.customField = "haha";

                                var newPineapple = new Pineapple()
                                {
                                    name = "random name of things " + name
                                };
                                user.Pineapples = new List<Pineapple>();
                                user.Pineapples.Add(newPineapple);
                                var result = await _userManager.CreateAsync(user);
                                var roleResult = await _userManager.AddToRoleAsync(user, "admin");

                            }

                            var userRoles = await _userManager.GetRolesAsync(user);

                            // adds the role as a new claim 
                            ClaimsIdentity identity = ctx.Principal.Identity as ClaimsIdentity;
                            if (identity != null)
                            {
                                foreach (var role in userRoles)
                                {
                                    identity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Role, role));
                                }
                            }

                        }

                    };
                });
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                   .AddEntityFrameworkStores<ApplicationDbContext>();

            services.TryAddScoped<SignInManager<ApplicationUser>>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                .AllowAnyMethod()
                                                 .AllowAnyHeader()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseAuthentication();
            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
