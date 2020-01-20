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
        public uint studentId { get; set; }
        public Marimba.Faculty faculty { get; set; }
        public Marimba.Instrument instrument { get; set; }
        public MailAddress email { get; set; }
        public Marimba.ShirtSize shirtSize { get; set; }
        public Marimba.MemberType memberType { get; set; }
        public DateTime signupTime { get; set; }
        public bool isPaid { get; set; }
    }
}