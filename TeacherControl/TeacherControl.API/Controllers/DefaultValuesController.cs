﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TeacherControl.API.Controllers
{
    public class DefaultValuesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}