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
    public class MemberController : Controller
    {
        private readonly MarimbaDbContext context;

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets a list of all members from the database
        /// </summary>
        /// <returns>A Task representing the operation</returns>
        [HttpGet]
        public async Task<IList<Member>> GetMembersAsync()
        {
            return await context.Members.ToListAsync();
        }

        public MemberController(MarimbaDbContext context)
        {
            this.context = context;
        }
    }
}