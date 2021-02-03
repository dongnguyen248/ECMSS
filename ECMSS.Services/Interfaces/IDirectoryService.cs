using ECMSS.DTO;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IDirectoryService
    {
        IEnumerable<DirectoryDTO> GetTreeDirectories();

        void CreateDirectory(string dirName, string path);

        void DeleteDirectory(int empId, string path);

        DirectoryDTO GetDirFromFileId(int fileId);

        DirectoryDTO GetDirFromId(int dirId);

        DirectoryDTO GetDirFromPath(string path);
    }
}