using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeacherControl.API.Controllers
{
    [Authorize]
    [Route("api/defaults")]
    public class DefaultValuesController : Controller
    {
        [HttpGet]
        public IActionResult GetConstantsValues()
        {
            return View();
        }
    }
}