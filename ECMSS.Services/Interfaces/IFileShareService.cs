using ECMSS.DTO;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IFileShareService
    {
        void AddFileShares(IEnumerable<FileShareDTO> fileShares);
    }
}