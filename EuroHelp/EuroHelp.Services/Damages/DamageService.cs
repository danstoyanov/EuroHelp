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

        public List<DamageServiceListingModel> GetAll()
                =>  this.data.Damages
                    .Select(d => new DamageServiceListingModel
                    {
                        Id = d.Id,
                        DamageType = d.DamageType,
                        PersonFirstName = d.PersonFirstName,
                        PersonSecondName = d.PersonSecondName,
                        CompanyName = d.CompanyName,
                        EventDate = d.EventDate.ToString("MMMM dd, yyyy"),
                        RegisterDate = d.RegistrationDate.ToString("MMMM dd, yyyy"),
                        IsApproved = d.IsApproved
                    })
                    .ToList();

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
                IsApproved = d.IsApproved
            })
            .ToList();

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
            string companyName)
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

        public void Edit(string id,
            string damageType,
            string eventDate,
            string personFirstName,
            string personSecondName,
            int? identityNumber)
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

        public DamageQueryServiceModel All(
            string damageType,
            string searchTerm,
            DamageSorting sorting,
            int currentPage,
            int damagesPerPage)
        {
            var damagesQuery = this.data.Damages.AsQueryable();

            if (!string.IsNullOrWhiteSpace(damageType))
            {
                damagesQuery = damagesQuery.Where(d => d.DamageType == damageType);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                damagesQuery = damagesQuery.Where(d =>
                (d.PersonFirstName + " " + d.PersonSecondName)
                .ToLower()
                .Contains(searchTerm.ToLower()));
            }

            damagesQuery = sorting switch
            {
                DamageSorting.EventDate => damagesQuery.OrderByDescending(d => d.EventDate),
                DamageSorting.RegisterDate => damagesQuery.OrderByDescending(d => d.RegistrationDate),
                DamageSorting.InsuranceCompany => damagesQuery.OrderByDescending(d => d.CompanyName)
            };

            var totalDamages = damagesQuery.Count();

            var damages = GetDamages(damagesQuery
                .Skip((currentPage - 1) * damagesPerPage)
                .Take(damagesPerPage));

            return new DamageQueryServiceModel
            {
                TotalDamages = totalDamages,
                CurrentPage = currentPage,
                CarsPerPage = damagesPerPage,
                Damages = damages
            };
        }

        public static IEnumerable<DamageServiceListingModel> GetDamages(IQueryable<Damage> damageQuery)
            => damageQuery
                .Select(d => new DamageServiceListingModel
                {
                    Id = d.Id,
                    DamageType = d.DamageType,
                    EventPlace = d.EventPlace,
                    CompanyName = d.CompanyName,
                    PersonFirstName = d.PersonFirstName,
                    PersonSecondName = d.PersonSecondName,
                    IsApproved = d.IsApproved
                })
                .ToList();

        public IEnumerable<string> AllDamageTypes()
            => this.data
                .Damages
                .Select(d => d.DamageType)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

        public string ChangeStatus(string id)
        {
            var damage = this.data.Damages
                .Where(d => d.Id == id)
                .FirstOrDefault();

            damage.IsApproved = "YES";

            this.data.SaveChanges();

            return damage.IsApproved;
        }
    }
}