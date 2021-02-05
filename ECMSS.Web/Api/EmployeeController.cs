using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using System.Collections.Generic;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [JwtAuthentication]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IEnumerable<EmployeeDTO> GetEmployeesByName(string empName)
        {
            return _employeeService.GetEmployeesByName(empName);
        }

        [AllowAnonymous]
        [HttpGet]
        public EmployeeDTO GetEmployeeFromToken()
        {
            var empId = int.Parse(JwtManager.ExtractFromHeader(ActionContext)["Id"]);
            return _employeeService.GetEmployeeById(empId);
        }
    }
}