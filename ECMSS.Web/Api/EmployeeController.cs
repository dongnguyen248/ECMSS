using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
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
    }
}