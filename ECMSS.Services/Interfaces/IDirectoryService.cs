using ECMSS.DTO;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IDirectoryService
    {
        IEnumerable<DirectoryDTO> GetTreeDirectories();

        void CreateDirectory(string dirName, string parentName);

        string GetPathFromFileId(int fileId);

        string GetPathFromDirId(int dirId);
    }
}