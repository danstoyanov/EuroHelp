namespace EuroHelp.Services.Damages
{
    public interface IDamageService
    {
        public DamageQueryServiceModel All();

        public string Create(
            string id, 
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
    }
}
