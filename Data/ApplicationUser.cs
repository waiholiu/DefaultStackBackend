using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace mvcWithAuth.Data
{

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(string userName) : base(userName){}
        public string customField {get; set;}

        public virtual List<Pineapple> Pineapples {get;set;}

    }

}