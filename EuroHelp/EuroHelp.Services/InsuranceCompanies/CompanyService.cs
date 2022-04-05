using EuroHelp.Data;
using EuroHelp.Data.Models;

namespace EuroHelp.Services.InsuranceCompanies
{
    public class CompanyService : ICompanyService
    {
        private readonly EuroHelpDbContext data;

        public CompanyService(EuroHelpDbContext data)
            => this.data = data;

        public List<AllInsuranceCompaniesServiceModel> GetAll()
            => this.data.InsuranceCompanies
            .Select(ic => new AllInsuranceCompaniesServiceModel
            {
                Id = ic.Id,
                Name = ic.Name,
                Bulstat = ic.Bulstat,
                FAX = ic.FAX,
                Address = ic.Address,
                PhoneNumber = ic.PhoneNumber,
                Email = ic.Email,
                Status = ic.Status,
            })
            .ToList();  

        public string Create(
            string name,
            int bulstat,
            string address,
            string phoneNumber,
            string mobilePhoneNumber,
            string email,
            int fax,
            string notes,
            string employeeId)
        {
            var comapnyData = new InsuranceCompany
            {
                Name = name,
                Bulstat = bulstat,
                Address = address,
                PhoneNumber = phoneNumber,
                MobilePhoneNumber = mobilePhoneNumber,
                Email = email,
                FAX = fax,
                Notes = notes,
                EmployeeId = employeeId,
            };

            this.data.InsuranceCompanies.Add(comapnyData);
            this.data.SaveChanges();

            return comapnyData.Id;
        }

        public bool IsCompanyContains(string id)
            => this.data.InsuranceCompanies
                .Any(c => c.Id == id);

        public IEnumerable<InsuranceCompaniesServiceModel> GetInsuranceCompanies()
            => this.data
                .InsuranceCompanies
                .Select(c => new InsuranceCompaniesServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public InsuranceCompany GetCompany(string id)
        {
            var company = this.data
                .InsuranceCompanies
                .Where(c => c.Id == id)
                .FirstOrDefault();

            return company;
        }

        public string ChangeStatus(string id)
        {
            var insuranceCompany = this.data.InsuranceCompanies
                .Where(ic => ic.Id == id)
                .FirstOrDefault();

            if (insuranceCompany.Status.ToLower() == "active")
            {
                insuranceCompany.Status = "Non active";
            }
            else if (insuranceCompany.Status.ToLower() == "non active")
            {
                insuranceCompany.Status = "Active";
            }

            this.data.SaveChanges(); 

            return insuranceCompany.Status;
        }
    }
}
