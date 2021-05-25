using ECMSS.DTO;
using System;
using System.Collections.Generic;

namespace ECMSS.Services.Interfaces
{
    public interface IFileShareService
    {
        IEnumerable<FileShareDTO> GetFileShares(Guid fileId);

        void AddFileShares(IEnumerable<FileShareDTO> fileShares);

        void EditFileShares(IEnumerable<FileShareDTO> fileShares, Guid fileId);
    }
}