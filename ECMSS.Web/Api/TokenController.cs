﻿using ECMSS.DTO;
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

        public TokenController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public string GetToken(string epLiteId)
        {
            EmployeeDTO curEmp = _employeeService.GetEmployeeByEpLiteId(epLiteId);
            if (curEmp != null)
            {
                string token = JwtManager.GenerateToken(curEmp.EpLiteId, curEmp.Id);
                return token;
            }
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}