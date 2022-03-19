using EuroHelp.Data;
using EuroHelp.Data.Models;

namespace EuroHelp.Services.InsuranceCompanies
{
    public class CompanyService : ICompanyService
    {
        private readonly EuroHelpDbContext data;

        public CompanyService(EuroHelpDbContext data)
        {
            this.data = data;
        }

        public string Create(string id, string name, int bulstat, string address, string phoneNumber, string mobilePhoneNumber, string email, int fax, string notes, string employeeId)
        {
            var comapnyData = new InsuranceCompany
            {
                Id = id,
                Name = name,
                Bulstat = bulstat,
                Address = address,
                PhoneNumber = phoneNumber,
                MobilePhoneNumber = mobilePhoneNumber,
                Email = email,
                FAX = fax,
                Notes = notes,
                EmployeeId = employeeId
            };

            this.data.InsuranceCompanies.Add(comapnyData);
            this.data.SaveChanges();

            return comapnyData.Id;
        }
    }
}
