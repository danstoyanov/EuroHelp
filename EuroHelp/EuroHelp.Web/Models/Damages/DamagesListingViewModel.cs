namespace EuroHelp.Web.Models.Damages
{
    public class DamagesListingViewModel
    {
        public string? Name { get; set; }

        public string CompanyName { get; set; }

        public string EventDate { get; set; }

        public string RegistrationDate { get; set; }

        public string EventType { get; set; }

        public int? BulgarianRegNumber { get; set; }

        public int? ForeignRegNumber { get; set; }

        public string? Property { get; set; }

        public string? InjuredPerson { get; set; }

        public string? NotifiedBy { get; set; }
    }
}
