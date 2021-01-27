using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using ECMSS.Web.Api.Core;
using ECMSS.Web.Extensions.Auth;
using System.Collections.Generic;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [JwtAuthentication]
    public class DepartmentController : ApiControllerCore
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public IEnumerable<DepartmentDTO> GetDepartments()
        {
            return _departmentService.GetDepartments();
        }
    }
}