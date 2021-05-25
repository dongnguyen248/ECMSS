using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [JwtAuthentication]
    public class TrashController : ApiController
    {
        private readonly ITrashService _trashService;

        public TrashController(ITrashService trashService)
        {
            _trashService = trashService;
        }

        [HttpPost]
        public HttpResponseMessage AddFilesToTrash(Guid[] fileIds)
        {
            try
            {
                var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
                _trashService.AddFilesToTrash(fileIds, empId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public HttpResponseMessage CleanTrash(Guid[] fileIds)
        {
            try
            {
                _trashService.CleanTrash(fileIds);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public HttpResponseMessage RecoverFile(Guid[] fileIds)
        {
            try
            {
                _trashService.RecoverFile(fileIds);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}