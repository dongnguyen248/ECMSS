namespace ECMSS.Services.Interfaces
{
    public  interface IFileImportantService
    {
        void AddOrRemoveImportantFile(int fileId, int employeeId);
    }
}
