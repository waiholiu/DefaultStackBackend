 
using GraphQL;
using GraphQL.Types;

namespace mvcWithAuth.Models
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(IDependencyResolver resolver): base(resolver)
        {
            Query = resolver.Resolve<GraphQLQuery>();
            // Mutation = resolver.Resolve<GraphQLMutation>();
        }
    }
}


 