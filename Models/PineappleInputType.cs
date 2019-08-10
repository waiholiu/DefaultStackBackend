 

using GraphQL.Types;
using mvcWithAuth.Data;

namespace mvcWithAuth.Models
{
    public class PineappleInputType : InputObjectGraphType
    {
        public PineappleInputType()
        {
            Name = "PineappleInput";
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}
