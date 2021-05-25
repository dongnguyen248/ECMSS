using ECMSS.DTO;
using System;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IDirectoryService
    {
        IEnumerable<DirectoryDTO> GetTreeDirectories(int empId);

        void CreateDirectory(string dirName, string path);

        void DeleteDirectory(int empId, string path);

        DirectoryDTO GetDirFromFileId(Guid fileId);

        DirectoryDTO GetDirFromId(int dirId);

        DirectoryDTO GetDirFromPath(string path);
    }
}