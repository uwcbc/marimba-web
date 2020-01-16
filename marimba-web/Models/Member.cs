using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace marimba_web.Models
{
    

    public class Member
    {
        // for now these enums are in the class, public in case we use them elsewhere, probably a better idea 
        // to make them separate classes if they're used in a lot of other places
        public enum StudentType
        {
            Undergrad,
            Grad,
            Alumnus,
            Other
        }

        public enum Faculty
        {
            Math,
            Science
            // TODO: add all faculties
        }

        // may want to  implement functionality to add instruments from Admin module,
        // depends how confident we are that we can list all instruments someone might play
        public enum Instrument
        {
            Trombone
            // TODO: add all instruments
        }

        public enum ShirtSize
        {
            Small,
            Medium,
            Large
            // XL?
        }

        public enum MemberType
        {
            General,
            Exec,
            Admin
        }
        public Guid id { get; set; }
        public string firstName { get; set; }
        public string lastname { get; set; }
        public StudentType category { get; set; }
        public int studentId { get; set; }
        public Faculty faculty { get; set; }
        public Instrument instrument { get; set; }
        public MailAddress email { get; set; }
        public ShirtSize shirtSize { get; set; }
        public MemberType memberType { get; set; }
        public DateTime signupTime { get; set; }
        public bool isPaid { get; set; }
    }
}