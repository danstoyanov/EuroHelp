namespace EuroHelp.Web.Global
{
    public class GlobalModelsConstants
    {
        public class InsuranceCompany
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;
            public const int MinBulstatValue = 5;
            public const int MaxBulstatValue = 9999;
            public const int AddressMinLength = 10;
            public const int AddressMaxLength = 100;
            public const int PhoneNumberLength = 8;
            public const int MobilePhoneNumberLength = 10;
            public const string EmailRegEx = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            public const int FaxMinLength = 10000;
            public const int FaxMaxLength = 99999;
            public const int NotesMaxLength = 500;
        }

        public class Damage
        {
            public const int NameMaxLength = 30;
            public const int BgRegMinValue = 10000;
            public const int BgRegMaxValue = 99999;
            public const int OtherRegMinValue = 10000;
            public const int OtherRegMaxValue = 99999;
        }
    }
}
