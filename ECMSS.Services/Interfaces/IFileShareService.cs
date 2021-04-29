using ECMSS.DTO;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IFileShareService
    {
        IEnumerable<FileShareDTO> GetFileShares(int fileId);

        void AddFileShares(IEnumerable<FileShareDTO> fileShares);

        void EditFileShares(IEnumerable<FileShareDTO> fileShares, int fileId);
    }
}