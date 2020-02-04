using System.Net.Mail;

using marimba_web.Common;

using CsvHelper.Configuration;

namespace marimba_web.Models
{
    /// <summary>
    /// Model for Elector, used for election purposes.
    /// </summary>
    public class Elector
    {
        /// <summary>
        /// Elector full name
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// Elector instrument
        /// </summary>
        public Marimba.Instrument instrument { get; private set; }

        /// <summary>
        /// Elector email
        /// </summary>
        public MailAddress email { get; private set; }

        /// <summary>
        /// Whether elector has paid all their membership fees
        /// </summary>
        public bool isMembershipPaid { get; private set; }

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
    }

    /// <summary>
    /// Class that maps Elector fields to CSV fields.
    /// </summary>
    public sealed class ElectorCsvMap : ClassMap<Elector>
    {
        public ElectorCsvMap()
        {
            Map(m => m.name).Index(0).Name("name");
            Map(m => m.instrument).Index(1).Name("instrument");
            Map(m => m.email).Index(2).Name("email");
            Map(m => m.isMembershipPaid).Index(3).Name("ismembershippaid");
        }
    }
}
