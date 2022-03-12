namespace EuroHelp.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int IdMaxLength = 40;
            public const int NameMaxLength = 25;
            public const int DefaultUsernameMaxLength = 20;
        }
        public class InsuranceCompany
        {
            public const int NameMaxLength = 30;
            public const int CodeMinValue = 1;
            public const int CodeMaxValue = 1000;
            public const int BulstatMinValue = 1000;
            public const int BulstatMaxValue = 9999;
            public const int AddressMaxLength = 200;
            public const int EmailMaxLength = 30;
            public const int MaxPhoneNumber = 8;
            public const int MaxMobilePhoneNumber = 12;
            public const int FaxMinValue = 1000;
            public const int FaxMaxValue = 9999;
            public const int NotesMaxLength = 350;
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
