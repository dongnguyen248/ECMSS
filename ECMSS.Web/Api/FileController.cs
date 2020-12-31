using ECMSS.Utilities;
using ECMSS.Web.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    public class FileController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Upload(FileViewModel fileVM)
        {
            string filePath = $@"{ConfigHelper.Read("FileUploadPath")}{fileVM.Owner}\{fileVM.Id}\{fileVM.Version}\";
            string fullPath = filePath + fileVM.FileName;
            FileHelper.SaveFile(fullPath, fileVM.FileData);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}