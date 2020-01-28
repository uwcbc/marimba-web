using System.Net.Mail;

using marimba_web.Common;

using CsvHelper.Configuration;

namespace marimba_web.Models
{
    public class Elector
    {
        public string name { get; set; }
        public Marimba.Instrument instrument { get; set; } 
        public MailAddress email { get; set; }
        public bool isMembershipPaid { get; set; }

        /// <summary>
        /// Creates an instance of the Elector class
        /// </summary>
        /// <param name="name">Elector name</param>
        /// <param name="instrument">Elector instrument</param>
        /// <param name="email">Elector email</param>
        /// <param name="isMembershipPaid">Whether elector has paid off all membership fees</param>
        public Elector(string name, Marimba.Instrument instrument, MailAddress email, bool isMembershipPaid)
        {
            this.name = name;
            this.instrument = instrument;
            this.email = email;
            this.isMembershipPaid = isMembershipPaid;
        }

        /// <summary>
        /// Sets the elector name
        /// </summary>
        public void SetName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Sets the elector instrument
        /// </summary>
        public void SetInstrument(Marimba.Instrument instrument)
        {
            this.instrument = instrument;
        }

        /// <summary>
        /// Sets the elector email
        /// </summary>
        public void SetEmail(MailAddress email)
        {
            this.email = email;
        }

        /// <summary>
        /// Sets whether elector has paid all membership fees
        /// </summary>
        public void SetMembershipPaid(bool isMembershipPaid)
        {
            this.isMembershipPaid = isMembershipPaid;
        }
    }
    
    // CSV column index mapping
    public sealed class ElectorMap : ClassMap<Elector>
    {
        public ElectorMap()
        {
            Map(m => m.name).Index(0).Name("name");
            Map(m => m.instrument).Index(1).Name("instrument");
            Map(m => m.email).Index(2).Name("email");
            Map(m => m.isMembershipPaid).Index(3).Name("ismembershippaid");
        }
    }
}
