using System;

namespace ECMSS.Services.Interfaces
{
    public interface IFileImportantService
    {
        void AddOrRemoveImportantFile(Guid fileId, int employeeId);
    }
}