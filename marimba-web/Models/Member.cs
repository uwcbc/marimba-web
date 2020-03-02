using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Mail;
using CsvHelper.Configuration;
using marimba_web.Common;
using marimba_web.Models.Converters;

namespace marimba_web.Models
{
    /// <summary>
    /// Model for Members of the band
    /// </summary>
    public class Member
    {
        /// <summary>
        /// The database ID of the Member
        /// </summary>
        public Guid id { get; private set; }

        /// <summary>
        /// The first name of the Member
        /// </summary>
        public string firstName { get; private set; }

        /// <summary>
        /// The last name of the Member
        /// </summary>
        public string lastName { get; private set; }

        /// <summary>
        /// The type of student the Member is
        /// </summary>
        public Marimba.StudentType studentType { get; private set; }

        /// <summary>
        /// The Member's UW student ID (0 if not a UW student)
        /// </summary>
        public uint studentId { get; private set; }

        /// <summary>
        /// The Member's faculty
        /// </summary>
        public Marimba.Faculty faculty { get; private set; }

        private List<Marimba.Instrument> instruments;
        /// <summary>
        /// The primary instrument the Member plays in band
        /// </summary>
        public ReadOnlyCollection<Marimba.Instrument> Instruments { get
            {
                return instruments.AsReadOnly();
            }
          }

        /// <summary>
        /// The Member's email address
        /// </summary>
        public MailAddress email { get; private set; }

        /// <summary>
        /// The Member's shirt size
        /// </summary>
        public Marimba.ShirtSize shirtSize { get; private set; }

        /// <summary>
        /// The type of the Member (i.e. general, exec, etc.)
        /// </summary>
        public Marimba.MemberType memberType { get; private set; }

        /// <summary>
        /// True if this member is a conductor, false otherwise.
        /// </summary>
        public bool IsConductor { get; internal set; } = false;

        /// <summary>
        /// The time that the Member signed up for band
        /// </summary>
        public DateTime signupTime { get; private set; }

        /// <summary>
        /// Value indicating whether the Member is subscribed for emails
        /// </summary>
        public bool isSubscribed { get; private set; }

        /// <summary>
        /// The debt that the Member owes to the band
        /// </summary>
        public decimal debtsOwed { get; private set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", firstName, lastName);
            }
        }

        /// <summary>
        /// Parameterless constructor, which is needed for parsing CSV files.
        /// </summary>
        public Member() {
            // Always generate GUID instantiating new Member
            this.id = Guid.NewGuid();
        }

        /// <summary>
        /// Creates an instance of the Member class
        /// </summary>
        /// <param name="firstName">The member's first name</param>
        /// <param name="lastName">The member's last name</param>
        /// <param name="studentType">The type of student that the member is</param>
        /// <param name="studentId">The member's Waterloo student id</param>
        /// <param name="faculty">The faculty that the member belongs to</param>
        /// <param name="instruments">The instrument that the member plays in band</param>
        /// <param name="email">The member's email</param>
        /// <param name="shirtSize">The member's shirt size</param>
        /// <param name="memberType">The type of member in band</param>
        /// <param name="debtsOwed">Amount of debt (e.g. from fees) owed</param>
        public Member(string firstName, string lastName, Marimba.StudentType studentType, uint studentId, 
            Marimba.Faculty faculty, IEnumerable<Marimba.Instrument> instruments, MailAddress email, Marimba.ShirtSize shirtSize,
            Marimba.MemberType memberType = Marimba.MemberType.General, decimal debtsOwed = 0m)
        {
            this.id = Guid.NewGuid();
            this.firstName = firstName;
            this.lastName = lastName;
            this.studentType = studentType;
            this.studentId = studentId;
            this.faculty = faculty;
            this.instruments = new List<Marimba.Instrument>(instruments);
            this.email = email;
            this.shirtSize = shirtSize;
            this.memberType = memberType;
            this.signupTime = DateTime.Today;
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
        /// <param name="instruments">The instrument that the member plays in band</param>
        /// <param name="email">The member's email</param>
        /// <param name="shirtSize">The member's shirt size</param>
        /// <param name="memberType">The type of member in band</param>
        /// <param name="debtsOwed">Amount of debt (e.g. from fees) owed</param>
        public Member(string firstName, string lastName, Marimba.StudentType studentType, uint studentId,
            Marimba.Faculty faculty, IEnumerable<Marimba.Instrument> instruments, MailAddress email, Marimba.ShirtSize shirtSize, DateTime signupTime,
            Marimba.MemberType memberType = Marimba.MemberType.General, decimal debtsOwed = 0m)
        {
            this.id = Guid.NewGuid();
            this.firstName = firstName;
            this.lastName = lastName;
            this.studentType = studentType;
            this.studentId = studentId;
            this.faculty = faculty;
            this.instruments = new List<Marimba.Instrument>(instruments);
            this.email = email;
            this.shirtSize = shirtSize;
            this.memberType = memberType;
            this.signupTime = signupTime;
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

        /// <summary>
        /// Determines if this member plays the given instrument or not.
        /// </summary>
        /// <param name="instrument">The instrument to check for.</param>
        /// <returns>True if this member plays that instrument, false otherwise.</returns>
        public bool DoesPlayInstrument(Marimba.Instrument instrument)
        {
            return Instruments.Contains(instrument);
        }
    }

    /// <summary>
    /// Class that maps Member fields to CSV fields.
    /// </summary>
    public sealed class MemberCsvMap : ClassMap<Member>
    {
        public MemberCsvMap()
        {
            Map(m => m.signupTime).Index(0);
            Map(m => m.firstName).Index(1);
            Map(m => m.lastName).Index(2);
            Map(m => m.studentId).Index(3);
            Map(m => m.studentType).Index(4);
            Map(m => m.email).Index(5).TypeConverter<EmailConverter>();
            /*
             * The fields below are all enums. CsvHelper will automatically parse
             * these as ints (enum value) or strings (enum name).
             */
            Map(m => m.Instruments).Index(6);
            Map(m => m.faculty).Index(7);
            Map(m => m.shirtSize).Index(8);
            Map(m => m.IsConductor).Index(9);
        }
    }
}
