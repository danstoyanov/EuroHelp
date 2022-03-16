namespace EuroHelp.Web.Models.Damages
{
    public class DamagesListingViewModel
    {
        public string Id { get; set; }

        public string? DamageType { get; set; }

        public string? CompanyName { get; set; }

        public string EventDate { get; set; }

        public string RegisterDate { get; set; }

        public string EventType { get; set; }

        public int? IdentityNumber { get; set; }

        public string PersonFirstName { get; set; }

        public string PersonSecondName { get; set; }

        public string EventPlace { get; set; }

        public string Comment { get; set; }
    }
}
