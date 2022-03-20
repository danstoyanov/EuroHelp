namespace EuroHelp.Services.References
{
    public interface IReferenceService
    {
        public ReferenceExportFomFile GenerateFile(string startDate, string endDate);
    }
}
