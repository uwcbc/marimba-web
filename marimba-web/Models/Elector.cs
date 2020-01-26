using System;
using System.Net.Mail;

namespace marimba_web.Models
{
    public class Elector
    {
        public string name { get; set; }
        public MailAddress email { get; set; }
        public bool isPaidMembership { get; set; }
    }
}
