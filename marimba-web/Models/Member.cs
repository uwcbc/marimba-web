using System;
using System.Net.Mail;
using CsvHelper.Configuration;
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
        public decimal debtsOwed { get; set; }

        /// <summary>
        /// The full name of the member.
        /// </summary>
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", firstName, lastName);
            }
        }

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
        /// <param name="debtsOwed">Amount of debt (e.g. from fees) owed</param>
        public Member(string firstName, string lastName, Marimba.StudentType studentType, uint studentId, 
            Marimba.Faculty faculty, Marimba.Instrument instrument, MailAddress email, Marimba.ShirtSize shirtSize,
            Marimba.MemberType memberType = Marimba.MemberType.General, bool isPaid = false, decimal debtsOwed = 0m)
        {
            this.id = Guid.NewGuid();
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
            this.debtsOwed = debtsOwed;
            this.isSubscribed = true;
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
        /// <param name="debtsOwed">Amount of debt (e.g. from fees) owed</param>
        public Member(string firstName, string lastName, Marimba.StudentType studentType, uint studentId,
            Marimba.Faculty faculty, Marimba.Instrument instrument, MailAddress email, Marimba.ShirtSize shirtSize, DateTime signupTime,
            Marimba.MemberType memberType = Marimba.MemberType.General, bool isPaid = false, decimal debtsOwed = 0m)
        {
            this.id = Guid.NewGuid();
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
            this.debtsOwed = debtsOwed;
        }

        /// <summary>
        /// Subscribes the member
        /// </summary>
        public void Subscribe()
        {
            isSubscribed = true;
        }

        /// <summary>
        /// Unsubscribes the member
        /// </summary>
        public void Unsubscribe()
        {
            isSubscribed = false;
        }

        /// <summary>
        /// Return whether member is a UW student (undergraduate or graduate).
        /// </summary>
        public bool IsUWStudent()
        {
            return studentType == Marimba.StudentType.Grad || studentType == Marimba.StudentType.Undergrad;
        }
    }

    // These mappings for CSV could be done as attributes to the properties, but done explicitly for more control.
    public sealed class MemberProfileMap : ClassMap<Member> {
        public MemberProfileMap()
        {
            // Could have auto-mapped but this specifies the order and names explicitly.
            Map(m => m.firstName).Index(0).Name("FirstName");
            Map(m => m.lastName).Index(1).Name("LastName");
            Map(m => m.email).Index(2).Name("Email");
            Map(m => m.faculty).Index(3).Name("Faculty");
            Map(m => m.isSubscribed).Index(4).Name("isSubscribed");
            Map(m => m.studentId).Index(5).Name("Student ID");
            Map(m => m.debtsOwed).Index(6).Name("Debts");
            Map(m => m.isPaid).Index(7).Name("Paid?");
            Map(m => m.shirtSize).Index(8).Name("Shirt Size");
            Map(m => m.studentType).Index(9).Name("Student Type");
            Map(m => m.instrument).Index(10).Name("Instrument");
            Map(m => m.signupTime).Index(11).Name("Signup Time");
        }
    }

    public sealed class MemberFinancialMap : ClassMap<Member>
    {
        public MemberFinancialMap()
        {
            Map(m => m.FullName).Index(0).Name("Full Name");
            Map(m => m.email).Index(1).Name("Email");
            Map(m => m.debtsOwed).Index(2).Name("Debts");
            Map(m => m.isPaid).Index(3).Name("Paid?");
        }
    }
}
