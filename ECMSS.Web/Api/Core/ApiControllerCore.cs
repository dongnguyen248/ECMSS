using ECMSS.DTO;
using ECMSS.Services.Interfaces;
using ECMSS.Web.Extensions.Auth;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace ECMSS.Web.Api.Core
{
    public class ApiControllerCore : ApiController
    {
        protected EmployeeDTO _emp;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            if (controllerContext.Request.Headers.Authorization != null)
            {
                string epLiteId = JwtManager.GetPrincipal(controllerContext.Request.Headers.Authorization.Parameter).Identity.Name;
                var employeeService = DependencyResolver.Current.GetService<IEmployeeService>();
                _emp = employeeService.GetEmployeeByEpLiteId(epLiteId);
            }
        }
    }
}