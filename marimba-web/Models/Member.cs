using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using marimba_web.Common;

namespace marimba_web.Models
{
    

    public class Member
    {
 
        public Guid id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Marimba.StudentType studentType { get; set; }
        public uint studentId { get; private set; }
        public Marimba.Faculty faculty { get; set; }
        public Marimba.Instrument instrument { get; set; }
        public MailAddress email { get; set; }
        public Marimba.ShirtSize shirtSize { get; set; }
        public Marimba.MemberType memberType { get; set; }
        public DateTime signupTime { get; set; }
        public bool isPaid { get; set; }
        public bool isSubscribed { get; private set; }


        /// <summary>
        /// Creates an instance of the Member class
        /// </summary>
        /// <param name="firstName">The member's first name</param>
        /// <param name="lastName">The member's last name</param>
        /// <param name="studentType">The type of student that the member is</param>
        /// <param name="studentId">The member's Waterloo student id</param>
        /// <param name="faculty">The faculty that the member belongs to</param>
        /// <param name="instrument">The instrument that the member plays in band</param>
        /// <param name="email">The member's email</param>
        /// <param name="shirtSize">The member's shirt size</param>
        /// <param name="memberType">The type of member in band</param>
        /// <param name="isPaid">Whether the member has paid membership fees</param>
        public Member(string firstName, string lastName, Marimba.StudentType studentType, uint studentId, 
            Marimba.Faculty faculty, Marimba.Instrument instrument, MailAddress email, Marimba.ShirtSize shirtSize,
            Marimba.MemberType memberType = Marimba.MemberType.General, bool isPaid = false)
        {
            this.id = new Guid();
            this.firstName = firstName;
            this.lastName = lastName;
            this.studentType = studentType;
            this.studentId = studentId;
            this.faculty = faculty;
            this.instrument = instrument;
            this.email = email;
            this.shirtSize = shirtSize;
            this.memberType = memberType;
            this.signupTime = DateTime.Now;
            this.isPaid = isPaid;
        }

        /// <summary>
        /// Overloaded constructor that takes a different signupTime
        /// </summary>
        /// <param name="firstName">The member's first name</param>
        /// <param name="lastName">The member's last name</param>
        /// <param name="studentType">The type of student that the member is</param>
        /// <param name="studentId">The member's Waterloo student id</param>
        /// <param name="faculty">The faculty that the member belongs to</param>
        /// <param name="instrument">The instrument that the member plays in band</param>
        /// <param name="email">The member's email</param>
        /// <param name="shirtSize">The member's shirt size</param>
        /// <param name="memberType">The type of member in band</param>
        /// <param name="isPaid">Whether the member has paid membership fees</param>
        public Member(string firstName, string lastName, Marimba.StudentType studentType, uint studentId,
            Marimba.Faculty faculty, Marimba.Instrument instrument, MailAddress email, Marimba.ShirtSize shirtSize, DateTime signupTime,
            Marimba.MemberType memberType = Marimba.MemberType.General,  bool isPaid = false)
        {
            this.id = new Guid();
            this.firstName = firstName;
            this.lastName = lastName;
            this.studentType = studentType;
            this.studentId = studentId;
            this.faculty = faculty;
            this.instrument = instrument;
            this.email = email;
            this.shirtSize = shirtSize;
            this.memberType = memberType;
            this.signupTime = signupTime;
            this.isPaid = isPaid;
        }
    }
}