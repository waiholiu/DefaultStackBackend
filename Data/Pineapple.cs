using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace mvcWithAuth.Data
{

    public class Pineapple
    {

        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity)]
        public int Id {set; get;}
        public string name{set;get;}

        
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }

}