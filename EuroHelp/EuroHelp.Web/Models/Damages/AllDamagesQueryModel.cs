using System.ComponentModel.DataAnnotations;

using EuroHelp.Services.Damages;

namespace EuroHelp.Web.Models.Damages
{
    public class AllDamagesQueryModel
    {
        public const int DamagesPerPage = 3;

        public string DamageType { get; init; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public DamageSorting Sorting { get; init; }

        public int TotalDamages { get; set; }

        public IEnumerable<string> DamageTypes { get; set; }

        public IEnumerable<DamageServiceListingModel> Damages { get; set; }

        // DOWN ><><><><!!!

        public string SearchId { get; set; }

        public string SearchEventDate { get; set; }

        public string SearchRegiestrationDate { get; set; }

        public string SearchCompanyName { get; set; }
    }
}

