using ECMSS.DTO;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IFileInfoService
    {
        IEnumerable<FileInfoDTO> GetFileInfos();
        IEnumerable<FileInfoDTO> GetFileInfosByDirId(int dirId);
        string[] GetFileUrl(int id);
        void UploadNewFile(FileInfoDTO fileInfo);
    }
}