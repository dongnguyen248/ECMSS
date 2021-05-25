using ECMSS.DTO;
using System;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IFileInfoService
    {
        IEnumerable<FileInfoDTO> GetFileInfosByUserId(int empId);

        IEnumerable<FileInfoDTO> GetFileInfosByDirId(int dirId);

        string[] GetFileUrl(Guid id, int empId, bool isShareUrl);

        string GetFileShareUrl(Guid id, int empId);

        FileInfoDTO GetFileInfo(Guid id);

        FileInfoDTO AddNewFile(FileInfoDTO fileInfo);

        List<FileInfoDTO> AddFiles(IEnumerable<FileInfoDTO> fileInfos);

        void EditFileInfo(FileInfoDTO fileInfo);

        void UploadNewFile(FileInfoDTO fileInfo);

        IEnumerable<FileInfoDTO> GetFavoriteFiles(int empId);

        IEnumerable<FileInfoDTO> GetImportantFiles(int empId);

        IEnumerable<FileInfoDTO> Search(string searchContent);

        IEnumerable<FileInfoDTO> GetDepartmentFiles(int empId);

        IEnumerable<FileInfoDTO> GetSharedFiles(int empId);

        IEnumerable<FileInfoDTO> GetTrashContents(int empId);
    }
}