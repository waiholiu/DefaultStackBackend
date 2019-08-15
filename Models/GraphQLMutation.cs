

using System;
using System.Linq;
using System.Security.Claims;
using GraphQL;
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

            Field<StringGraphType>(
                "testingMigration",

                resolve: context => "migration graphql works"
            );
            // Field<PineappleType>(
            //     "createPineapple",
            //     arguments: new QueryArguments(
            //         new QueryArgument<NonNullGraphType<PineappleInputType>> { Name = "pineapple" }
            //     ),
            //     resolve: context =>
            //     {
            //         var newPineapple = context.GetArgument<Pineapple>("pineapple");
            //         var userId = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //         var user = userManager.FindByNameAsync(userId).Result;
            //         newPineapple.ApplicationUserId = user.Id;

            //         _db.Pineapples.Add(newPineapple);
            //         _db.SaveChanges();
            //         return newPineapple;

            //     });

            // Field<BooleanGraphType>(
            //     "deletePineapple",
            //     arguments: new QueryArguments(
            //         new QueryArgument<IntGraphType> { Name = "pineappleId" }
            //     ),
            //     resolve: context =>
            //     {
            //         var pineappleId = context.GetArgument<int>("pineappleId");
            //         // var userId = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //         // var user = userManager.FindByNameAsync(userId).Result;
            //         // newPineapple.ApplicationUserId = user.Id;

            //         var deletingPineapple = _db.Pineapples.FirstOrDefault(p => p.Id == pineappleId);
            //         if(deletingPineapple == null)
            //             throw new ExecutionError("pineapple does not exist");

            //         _db.Pineapples.Remove(deletingPineapple);
            //         _db.SaveChanges();

            //         // _db.SaveChanges();
            //         // return newPineapple;

            //         Console.WriteLine("deleted");
            //         return true;

            //     });
        }
    }
}
