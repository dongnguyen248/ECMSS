using ECMSS.DTO;

namespace ECMSS.Services.Interfaces
{
    public interface IFileFavoriteService
    {
        void AddOrRemoveFavoriteFile(FileFavoriteDTO fileFavoriteDTO);
    }
}