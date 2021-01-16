using ECMSS.DTO;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IDirectoryService
    {
        IEnumerable<DirectoryDTO> GetTreeDirectories();

        string GetPathFromFileId(int fileId);

        string GetPathFromDirId(int dirId);
    }
}