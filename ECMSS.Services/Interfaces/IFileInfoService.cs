using ECMSS.DTO;
using System;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IFileInfoService
    {
        IEnumerable<FileInfoDTO> GetFileInfosByUserId(int empId, string filterExtension, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<FileInfoDTO> GetFileInfosByDirId(int dirId, string filterExtension, int pageIndex, int pageSize, out int totalRow);

        string[] GetFileUrl(Guid id, int empId, bool isShareUrl);

        string GetFileShareUrl(Guid id, int empId);

        FileInfoDTO GetFileInfo(Guid id);

        List<FileInfoDTO> AddFiles(IEnumerable<FileInfoDTO> fileInfos);

        FileInfoDTO EditFileInfo(FileInfoDTO fileInfo);

        void UploadNewFile(FileInfoDTO fileInfo);

        IEnumerable<FileInfoDTO> GetFavoriteFiles(int empId, string filterExtension, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<FileInfoDTO> GetImportantFiles(int empId, string filterExtension, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<FileInfoDTO> Search(string searchContent, string filterExtension, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<FileInfoDTO> GetDepartmentFiles(int empId, string filterExtension, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<FileInfoDTO> GetSharedFiles(int empId, string filterExtension, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<FileInfoDTO> GetTrashContents(int empId, string filterExtension, int pageIndex, int pageSize, out int totalRow);
    }
}