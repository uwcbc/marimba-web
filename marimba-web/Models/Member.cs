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
        private Guid id;
        private string firstName;
        private string lastname;
        private StudentType category;
        private int studentId;
        private Faculty faculty;
        private Instrument instrument;
        private MailAddress email;
        private ShirtSize shirtSize;
        private MemberType memberType;
        private DateTime signupTime;
        private bool isPaid;
    }
}