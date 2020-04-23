using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ViewApplication.Controllers
{
    [AllowAnonymous]
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("404")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        [Route("400")]
        public new IActionResult BadRequest()
        {
            return View();
        }

        [Route("500")]
        public IActionResult ServerError()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}