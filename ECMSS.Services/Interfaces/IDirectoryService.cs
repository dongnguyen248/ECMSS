using ECMSS.DTO;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IDirectoryService
    {
        IEnumerable<DirectoryDTO> GetDirectories();
        string GetPathFromFileId(int fileId);
    }
}
