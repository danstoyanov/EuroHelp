using EuroHelp.Data;
using EuroHelp.Data.Models;

namespace EuroHelp.Services.Damages
{
    public class DamageService : IDamageService
    {
        private readonly EuroHelpDbContext data;

        public DamageService(EuroHelpDbContext data)
           => this.data = data;

        public DamageQueryServiceModel All()
        {
            var damageQuery = this.data.Damages.AsQueryable();

            return new DamageQueryServiceModel
            {

            };
        }

        public string Create(string id, string damageType, string eventDate, int identityNumber, string personFirstName, string personSecondName, string eventPlace, string comment, string consumerId, string companyId, string companyName)
        {
            var damageData = new Damage
            {
                Id = id,
                DamageType = damageType,
                EventDate = DateTime.Parse(eventDate),
                IdentityNumber = identityNumber,
                PersonFirstName = personFirstName,
                PersonSecondName = personSecondName,
                EventPlace = eventPlace,
                Comment = comment,
                ConsumerId = consumerId,
                CompanyId = companyId,
                CompanyName = companyName
            };

            this.data.Damages.Add(damageData);
            this.data.SaveChanges();

            return damageData.Id;
        }
    }
}
