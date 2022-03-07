namespace EuroHelp.Web.Models.Companies
{
    public class AddCompanyFormModel
    {
        public int Id { get; set; }  

        public string Name { get; set; }   

        public int Code { get; set; }

        public int Bulstat { get; set; }

        public string CompanyEnglName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string Email { get; set; }


        public string FAX { get; set; }

        public string Notes { get; set; }
    }
}
