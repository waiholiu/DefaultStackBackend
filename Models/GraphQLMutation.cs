 

using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using mvcWithAuth.Data;

namespace mvcWithAuth.Models
{
    public class GraphQLMutation : ObjectGraphType
    {
        private readonly ApplicationDbContext dbContext;

        public GraphQLMutation(ApplicationDbContext _db, IHttpContextAccessor httpContext)
        {
            Name = "CreatePlayerMutation";

            Field<PineappleType>(
                "createPineapple",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PineappleInputType>> { Name = "pineapple" }
                ),
                resolve: context =>
                {
                    var newPineapple = context.GetArgument<Pineapple>("pineapple");
                    // player.ApplicationUserId = httpContext.HttpContext.Principal.claim.User.

                    // var name = ctx.Principal.Claims.First(c => c.Type == "user_id").Value;

                    //         //Get userManager out of DI
                    //         var _userManager = ctx.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();

                    //         // retrieves the roles that the user has
                    //         ApplicationUser user = await _userManager.FindByNameAsync(name);

                    _db.Pineapples.Add(newPineapple);
                    _db.SaveChanges();
                    return newPineapple;

                });
        }
    }
}
