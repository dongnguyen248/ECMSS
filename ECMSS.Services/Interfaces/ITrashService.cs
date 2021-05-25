using System;

namespace ECMSS.Services.Interfaces
{
    public interface ITrashService
    {
        void AddFilesToTrash(Guid[] fileIds, int empId);

        void CleanTrash(Guid[] fileIds);

        void RecoverFile(Guid[] fileIds);
    }
}