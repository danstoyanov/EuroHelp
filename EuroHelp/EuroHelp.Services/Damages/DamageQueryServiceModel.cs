namespace EuroHelp.Services.Damages
{
    public class DamageQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int CarsPerPage { get; init; }

        public int TotalDamages { get; init; }

        public IEnumerable<DamageServiceListingModel> Damages { get; init; }

    }
}
