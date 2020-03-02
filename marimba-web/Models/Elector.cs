using System.Net.Mail;

using marimba_web.Common;

using CsvHelper.Configuration;
using System.Collections.Generic;

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
        public List<Marimba.Instrument> instruments { get; private set; }

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
        /// <param name="instruments">Elector instrument</param>
        /// <param name="email">Elector email</param>
        /// <param name="isMembershipPaid">Whether elector has paid off all membership fees</param>
        public Elector(string name, IEnumerable<Marimba.Instrument> instruments, MailAddress email, bool isMembershipPaid)
        {
            this.name = name;
            this.instruments = new List<Marimba.Instrument>(instruments);
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
            Map(m => m.instruments).Index(1).Name("instrument");
            Map(m => m.email).Index(2).Name("email");
            Map(m => m.isMembershipPaid).Index(3).Name("ismembershippaid");
        }
    }
}
