namespace ECMSS.Services.Interfaces
{
    public interface IFileFavoriteService
    {
        void AddOrRemoveFavoriteFile(int fileId, int employeeId);
    }
}