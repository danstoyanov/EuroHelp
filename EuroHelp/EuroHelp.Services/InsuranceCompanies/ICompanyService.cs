using EuroHelp.Data.Models;

namespace EuroHelp.Services.InsuranceCompanies
{
    public interface ICompanyService
    {
        public string Create(string id, 
            string name, 
            int bulstat, 
            string address, 
            string phoneNumber, 
            string mobilePhoneNumber, 
            string email, 
            int fax, 
            string notes,
            string employeeId);

        public bool IsCompanyContains(string id);
    }
}
