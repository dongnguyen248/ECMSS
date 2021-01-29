using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using System.Net;
using System.Web.Http;

namespace ECMSS.Web.Api
{
    [AllowAnonymous]
    public class TokenController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private const int EXPIRE_MINUTES = 60;

        public TokenController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public string GetToken(string epLiteId)
        {
            var employee = _employeeService.GetEmployeeByEpLiteId(epLiteId);
            if (employee != null)
            {
                string token = JwtManager.GenerateToken(employee.EpLiteId, employee.Id, EXPIRE_MINUTES);
                return token;
            }
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        [HttpGet]
        public IHttpActionResult GetTokenV2(string epLiteId)
        {
            var employee = _employeeService.GetEmployeeByEpLiteId(epLiteId);
            if (employee != null)
            {
                string token = JwtManager.GenerateToken(employee.EpLiteId, employee.Id, EXPIRE_MINUTES);
                return Ok(new { token, empName = $"{employee.LastName} {employee.FirstName}" });
            }
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}