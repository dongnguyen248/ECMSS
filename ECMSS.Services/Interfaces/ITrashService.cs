namespace ECMSS.Services.Interfaces
{
    public interface ITrashService
    {
        void AddFilesToTrash(int[] fileIds, int empId);

        void CleanTrash(int[] fileIds);
    }
}