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

        /// <summary>
        /// Get the attendance records for the given Term.
        /// </summary>
        /// <returns>
        /// TermAttendance record for the given Term. It contains a list of all rehearsals as well as
        /// the main attendance table.
        /// </returns>
        [HttpGet]
        public IActionResult GetAttendanceForTerm(string termName)
        {
            Term term = context.Terms.First(t => t.name == termName);
            return new ObjectResult(new TermAttendance(term));
        }

        /// <summary>
        /// Get the attendance record for the given Term as a CSV file.
        /// </summary>
        /// <param name="term">The Term</param>
        /// <returns>CSV file</returns>
        public IActionResult GetAttendanceAsCsv(Term term)
        {
            byte[] csvBytes = new TermAttendance(term).WriteCsvBytes();
            string fileName = term.name + ".csv";
            return File(csvBytes, "text/csv", fileName);
        }

        /// <summary>
        /// Update a Term's attendance record for a particular Member for a particular Rehearsal.
        /// </summary>
        /// <param name="termName">Term name</param>
        /// <param name="memberId">Member GUID</param>
        /// <param name="rehearsalDate">Rehearsal date</param>
        /// <param name="isPresent">Whether the Member should be marked as present</param>
        /// <returns>OkResult if successful</returns>
        [HttpPost]
        public IActionResult UpdateAttendanceForTerm(string termName, Guid memberId, DateTime rehearsalDate, bool isPresent)
        {
            Term term = context.Terms.First(t => t.name == termName);
            // TODO: Consider defining a universal date-time format
            Rehearsal rehearsal = term.rehearsals.First(r => r.date == rehearsalDate);
            bool containsMember = rehearsal.members.Contains(memberId);

            // Update attendance
            if (containsMember && !isPresent)
            {
                rehearsal.members.Remove(memberId);
            }
            else if (!containsMember && isPresent)
            {
                rehearsal.members.Add(memberId);
            }

            context.SaveChanges();
            return new OkResult();
        }


        public MembersController(MarimbaDbContext context)
        {
            this.context = context;
        }
    }
}
