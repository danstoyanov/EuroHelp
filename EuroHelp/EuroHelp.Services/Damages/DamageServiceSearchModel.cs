namespace EuroHelp.Services.Damages
{
    public class DamageServiceSearchModel
    {
        public string SearchId { get; set; }

        public string SearchEventDate { get; set; }

        public string SearchRegiestrationDate { get; set; }

        public string SearchCompanyName { get; set; }

        public List<DamageServiceListingModel> Damages { get; set; }
    }
}
