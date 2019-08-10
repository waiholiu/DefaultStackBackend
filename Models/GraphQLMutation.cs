

using System.Security.Claims;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using mvcWithAuth.Data;

namespace mvcWithAuth.Models
{
    public class GraphQLMutation : ObjectGraphType
    {
        private readonly ApplicationDbContext dbContext;

        public GraphQLMutation(ApplicationDbContext _db, IHttpContextAccessor httpContext, UserManager<ApplicationUser> userManager)
        {
            Name = "Mutations";

            Field<PineappleType>(
                "createPineapple",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PineappleInputType>> { Name = "pineapple" }
                ),
                resolve: context =>
                {
                    var newPineapple = context.GetArgument<Pineapple>("pineapple");
                    var userId = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var user = userManager.FindByNameAsync(userId).Result;
                    newPineapple.ApplicationUserId = user.Id;

                    _db.Pineapples.Add(newPineapple);
                    _db.SaveChanges();
                    return newPineapple;

                });
        }
    }
}
