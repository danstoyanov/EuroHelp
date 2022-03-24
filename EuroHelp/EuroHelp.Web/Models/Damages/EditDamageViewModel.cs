namespace EuroHelp.Web.Models.Damages
{
    public class EditDamageViewModel
    {
        public string Id { get; set; }

        public string? DamageType { get; set; }

        public string CompanyName { get; set; }

        public string EventDate { get; set; }

        public string RegistrationDate { get; set; }

        public string? PersonFirstName{ get; set; }

        public string? PersonSecondName { get; set; }

        public int? IdentityNumber { get; set; }
    }
}
