using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marimba_web.Data;
using marimba_web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace marimba_web.Controllers
{
    public class MembersController : Controller
    {
        private readonly MarimbaDbContext context;

        public IActionResult Attendance()
        {
            return View();
        }

        public IActionResult Directory()
        {
            return View();
        }

        public IActionResult Records()
        {
            return View();
        }

        public IActionResult Drive()
        {
            return View();
        }

        public IActionResult Elections()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        // NOTE: To view the front-end, comment out lines 48-61 until we figure out how to deal with the database 

        /// <summary>
        /// Gets a list of all members from the database
        /// </summary>
        /// <returns>A Task representing the operation</returns>
        [HttpGet]
        public async Task<IList<Member>> GetMembersAsync()
        {
            return await context.Members.ToListAsync();
        }

        public MembersController(MarimbaDbContext context)
        {
            this.context = context;
        }
    }
}