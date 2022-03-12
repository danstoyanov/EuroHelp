namespace EuroHelp.Web.Models.Damages
{
    public class AllDamagesQueryModel
    {
        public string SearchId { get; set; }

        public string SearchEventDate { get; set; }

        public string SearchRegiestrationDate { get; set; }

        public string SearchCompanyName { get; set; }

        public List<DamagesListingViewModel> Damages { get; set; }
    }
}
