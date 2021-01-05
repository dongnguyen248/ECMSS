using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace ECMSS.Web.Controllers
{
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public IEnumerable<DepartmentDTO> GetDepartments()
        {
            return _departmentService.GetDepartments();
        }
    }
}