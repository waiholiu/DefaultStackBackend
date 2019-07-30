
using GraphQL.Types;
using mvcWithAuth.Data;


namespace mvcWithAuth.Models
{
    public class PineappleType : ObjectGraphType<Pineapple>
    {
        public PineappleType()
        {
            Field(x => x.Id);
            Field(x => x.name, true);
            
            // Field<ListGraphType<SkaterStatisticType>>("skaterSeasonStats",
            //     arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
            //     resolve: context => contextServiceLocator.SkaterStatisticRepository.Get(context.Source.Id), description: "Player's skater stats");
        }
    }
}
