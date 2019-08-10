
using System;
using GraphQL.Types;
using Microsoft.AspNetCore.Identity;
using mvcWithAuth.Data;


namespace mvcWithAuth.Models
{
    public class PineappleType : ObjectGraphType<Pineapple>
    {
        public PineappleType(UserManager<ApplicationUser> userManager)
        {
            
            Field(x => x.Id);
            Field(x => x.name, true);
            Field<ApplicationUserType>(
                            "createdBy",
                            resolve: context =>
                            {
                                return userManager.FindByIdAsync(context.Source.ApplicationUserId);
                                // data.GetFriends(context.Source)
                            });

            // Field<ObjectGraphType<ApplicationUserType>>(
            //     "friends",
            //     resolve: context =>
            //     {
            //         return userManager.FindByIdAsync(context.Source.ApplicationUserId);
            //         // data.GetFriends(context.Source)
            //     });


            // ApplicationUserType>(
            //     "creator",
            //     resolve: context =>
            //     {
            //         Console.WriteLine("hello");
            //         return userManager.FindByIdAsync(context.Source.ApplicationUserId).Result;
            //     },true            );

            // Field<ListGraphType<ApplicationUserType>>("applicationUserType",
            //     arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
            //     resolve: context =>
            //     {
            //         var user = userManager.FindByIdAsync(context.Source.ApplicationUserId).Result;
            //         return user;

            //     }
            //     , description: "Pineapple creator");

        }
    }
}

