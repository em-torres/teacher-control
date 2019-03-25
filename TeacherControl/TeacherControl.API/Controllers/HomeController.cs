using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TeacherControl.API.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("The Api is Currently Running");
        }
    }
}