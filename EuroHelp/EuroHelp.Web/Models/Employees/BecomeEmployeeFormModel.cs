using System.ComponentModel.DataAnnotations;

using static EuroHelp.Web.Global.GlobalModelsConstants.Employee;

namespace EuroHelp.Web.Models.Employees
{
    public class BecomeEmployeeFormModel
    {
        [Required]
        [MinLength(EmployeeMinNameLength)]
        [MaxLength(EmployeeMaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberLength)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
