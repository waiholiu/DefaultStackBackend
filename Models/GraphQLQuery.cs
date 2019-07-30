
using System.Linq;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using mvcWithAuth.Data;


namespace mvcWithAuth.Models
{
    public class GraphQLQuery : ObjectGraphType
    {

        private readonly ApplicationDbContext dbContext;

        public GraphQLQuery(ApplicationDbContext _db, IHttpContextAccessor httpContext)
        {
            dbContext = _db;
            Field<PineappleType>(
                "pineapple",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => 
                {
                    var id = context.GetArgument<int>("id");
                    return dbContext.Pineapples.FirstOrDefault(p => p.Id == id);

                });

                

            // Field<PlayerType>(
            //     "randomPlayer",
            //     resolve: context => httpContext.PlayerRepository.GetRandom());

            Field<ListGraphType<PineappleType>>(
                "pineapples",
                resolve: context => dbContext.Pineapples.ToList());

            // Field<ListGraphType<LeagueType>>(
            //     "leagues",
            //     resolve: context =>
            //     {
            //         return dbContext.Leagues.ToListAsync();
            //     }


            // );
        }
    }

   
}


