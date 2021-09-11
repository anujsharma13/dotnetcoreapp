using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoreapp.ViewModels
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statuscoderesult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch(statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not ";
                    logger.LogWarning($"404 Error occuredd path= {statuscoderesult.OriginalPath}" +
                        $"and querystring={statuscoderesult.OriginalQueryString}");
                    break;

            }
            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature =
               HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            logger.LogError($"the path {exceptionHandlerPathFeature.Path} threw an exception " + $"{exceptionHandlerPathFeature.Error}");
           
            return View("Error");
        }
    }
}
