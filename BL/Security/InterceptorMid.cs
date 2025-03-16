using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Abstractions;
using Newtonsoft.Json;

namespace BL.Security
{
    public class InterceptorMid
    {
        private readonly RequestDelegate _next;

        public InterceptorMid(RequestDelegate next)
        {
            _next = next;
        }

        public async Task<Task> Invoke(HttpContext httpContext)
        {
            
            return _next(httpContext);
        }
    }
}
