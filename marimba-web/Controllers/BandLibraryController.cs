﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace marimba_web.Controllers
{
    public class BandLibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}