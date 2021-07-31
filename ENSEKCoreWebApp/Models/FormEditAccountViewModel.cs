using System.ComponentModel.DataAnnotations;

namespace ENSEKCoreWebApp.Models
{
    public class FormEditAccountViewModel
    {
       
        [Display(Name = "Account Id")]
        public int? AccountId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

    }
}
