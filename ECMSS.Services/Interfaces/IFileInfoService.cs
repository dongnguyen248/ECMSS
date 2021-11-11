using ECMSS.DTO;
using System;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IFileInfoService
    {
        IEnumerable<FileInfoDTO> GetFileInfosByUserId(int empId, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<FileInfoDTO> GetFileInfosByDirId(int dirId, int pageIndex, int pageSize, out int totalRow);

        string[] GetFileUrl(Guid id, int empId, bool isShareUrl);

        string GetFileShareUrl(Guid id, int empId);

        FileInfoDTO GetFileInfo(Guid id);

        List<FileInfoDTO> AddFiles(IEnumerable<FileInfoDTO> fileInfos);

        FileInfoDTO EditFileInfo(FileInfoDTO fileInfo);

        void UploadNewFile(FileInfoDTO fileInfo);

        IEnumerable<FileInfoDTO> GetFavoriteFiles(int empId, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<FileInfoDTO> GetImportantFiles(int empId, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<FileInfoDTO> Search(string searchContent, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<FileInfoDTO> GetDepartmentFiles(int empId, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<FileInfoDTO> GetSharedFiles(int empId, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<FileInfoDTO> GetTrashContents(int empId, int pageIndex, int pageSize, out int totalRow);
    }
}