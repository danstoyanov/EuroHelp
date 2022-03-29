namespace EuroHelp.Services.Damages
{
    public class DamageServiceListingModel
    {
        public string Id { get; set; }

        public string? DamageType { get; set; }

        public string? CompanyName { get; set; }

        public string InsuranceCompanyId {get; set;}

        public string EventDate { get; set; }

        public string RegisterDate { get; set; }

        public int? IdentityNumber { get; set; }

        public string PersonFirstName { get; set; }

        public string PersonSecondName { get; set; }

        public string EventPlace { get; set; }

        public string Comment { get; set; }
    }
}
