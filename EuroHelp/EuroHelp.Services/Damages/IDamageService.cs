using EuroHelp.Data.Models;

namespace EuroHelp.Services.Damages
{
    public interface IDamageService
    {
        public void Edit(string id, string damageType, string eventDate,
            string personFirstName, string personSecondName, int? identityNumber);

        public Damage GetDamage(string id);

        public List<DamageServiceListingModel> All();

        public List<DamageServiceListingModel> DamagesByConsumer(string id);

        public List<DamageServiceListingModel> Search(string id, string companyName);

        public string Create(
            string damageType,
            DateTime eventDate,
            int identityNumber,
            string personFirstName,
            string personSecondName,
            string eventPlace,
            string comment,
            string consumerId,
            string companyId,
            string companyName);

        public void Delete(string id);

        public bool IsValid(string id);
    }
}