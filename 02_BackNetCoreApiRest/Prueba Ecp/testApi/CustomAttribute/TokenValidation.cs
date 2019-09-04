using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using System;
using testEcpApi.Models;
using System.Web.Http.Filters;


namespace testEcpApi.CustomAttribute
{
    public class TokenValidationAttribute : AuthorizeAttribute
    {
    }
}
