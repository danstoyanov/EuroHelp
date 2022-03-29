using EuroHelp.Data;
using EuroHelp.Data.Models;

namespace EuroHelp.Services.Damages
{
    public class DamageService : IDamageService
    {
        private readonly EuroHelpDbContext data;

        public DamageService(EuroHelpDbContext data)
           => this.data = data;

        public Damage GetDamage(string id)
        {
            var damage = this.data.Damages
                .Where(d => d.Id == id)
                .FirstOrDefault();

            return damage;
        }

        public List<DamageServiceListingModel> All()
        {
            var damagesQuery = this.data.Damages
                .OrderByDescending(d => d.RegistrationDate)
                .AsQueryable();

            var damages = damagesQuery
                .Select(d => new DamageServiceListingModel
                {
                    Id = d.Id,
                    DamageType = d.DamageType,
                    CompanyName = d.CompanyName,
                    EventDate = d.EventDate.ToString("dd/MM/yyyy"),
                    RegisterDate = d.RegistrationDate.ToString("dd/MM/yyyy"),
                    IdentityNumber = d.IdentityNumber,
                    InsuranceCompanyId = d.CompanyId,
                    PersonFirstName = d.PersonFirstName,
                    PersonSecondName = d.PersonSecondName,
                    EventPlace = d.EventPlace,
                })
                .ToList();

            return damages;
        }

        public List<DamageServiceListingModel> DamagesByConsumer(string id)
            => this.data
            .Damages
            .Where(d => d.ConsumerId == id)
            .Select(d => new DamageServiceListingModel
            {
                Id = d.Id,
                DamageType = d.DamageType,
                CompanyName = d.CompanyName,
                EventDate = d.EventDate.ToString("dd/MM/yyyy"),
                RegisterDate = d.RegistrationDate.ToString("dd/MM/yyyy"),
                IdentityNumber = d.IdentityNumber,
                PersonFirstName = d.PersonFirstName,
                PersonSecondName = d.PersonSecondName,
                EventPlace = d.EventPlace,
            })
            .ToList();

        public string Create(string damageType, DateTime eventDate, int identityNumber, string personFirstName,
            string personSecondName, string eventPlace, string comment, string consumerId, string companyId, string companyName)
        {
            var damageData = new Damage
            {
                DamageType = damageType,
                EventDate = eventDate,
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

        public void Delete(string id)
        {
            var damage = this.data.Damages
                .Where(d => d.Id == id)
                .FirstOrDefault();

            if (damage != null)
            {
                this.data.Remove(damage);
                this.data.SaveChanges();
            }
        }

        public bool IsValid(string id)
        {
            var damage = this.data.Damages
                .Where(d => d.Id == id)
                .FirstOrDefault();

            if (damage == null)
            {
                return false;
            }

            return true;
        }

        public void Edit(string id, string damageType, string eventDate,
            string personFirstName, string personSecondName, int? identityNumber)
        {
            var damage = this.data.Damages
                .Where(d => d.Id == id)
                .FirstOrDefault();

            damage.DamageType = damageType;
            damage.EventDate = DateTime.Parse(eventDate);
            damage.PersonFirstName = personFirstName;
            damage.PersonSecondName = personSecondName;
            damage.IdentityNumber = identityNumber;

            this.data.SaveChanges();
        }

        //FIX !!!
        public List<DamageServiceListingModel> Search(string id, string companyName)
        {
            var damagesQuery = this.data.Damages.AsQueryable();

            if (id != null)
            {
                damagesQuery = damagesQuery
                    .Where(d => d.Id == id)
                    .OrderByDescending(d => d.CompanyName)
                    .AsQueryable();
            }
            else if (companyName != null)
            {
                damagesQuery = damagesQuery
                    .Where(d => d.CompanyName == companyName)
                    .OrderByDescending(d => d.CompanyName)
                    .AsQueryable();
            }

            var damages = damagesQuery
                  .Select(d => new DamageServiceListingModel
                  {
                      Id = d.Id,
                      DamageType = d.DamageType,
                      RegisterDate = d.RegistrationDate.ToString("dd/MM/yyyy"),
                      EventDate = d.EventDate.ToString("dd/MM/yyyy"),
                      CompanyName = d.CompanyName
                  })
                  .ToList();

            return damages.OrderByDescending(d => d.EventDate).ToList();
        }
    }
}