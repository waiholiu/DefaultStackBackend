
using GraphQL.Types;
using mvcWithAuth.Data;


namespace mvcWithAuth.Models
{
    public class ApplicationUserType : ObjectGraphType<ApplicationUser>
    {
        public ApplicationUserType()
        {
            
            Field(x => x.Id);
            Field(x => x.Email, true); 
            Field(x => x.UserName);
            
        }
    }
}
