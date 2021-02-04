using ECMSS.DTO;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IFileFavoriteService
    {
        void AddOrRemoveFavoriteFile(FileFavoriteDTO fileFavoriteDTO);

        void AddFavoriteFiles(IEnumerable<FileFavoriteDTO> fileFavorites);
    }
}