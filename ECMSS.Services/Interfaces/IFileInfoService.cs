using ECMSS.DTO;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IFileInfoService
    {
        IEnumerable<FileInfoDTO> GetFileInfosByUserId(int empId);

        IEnumerable<FileInfoDTO> GetFileInfosByDirId(int dirId);

        string[] GetFileUrl(int id);

        void UploadNewFile(FileInfoDTO fileInfo);

        IEnumerable<FileInfoDTO> GetFavoriteFiles(int empId);

        IEnumerable<FileInfoDTO> GetImportantFiles(int empId);
    }
}