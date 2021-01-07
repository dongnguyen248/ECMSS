using ECMSS.DTO;

namespace ECMSS.Services.Interfaces
{
    public interface IFileHistoryService
    {
        void UploadFile(FileHistoryDTO fileHistory);
    }
}