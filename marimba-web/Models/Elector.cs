using CsvHelper.Configuration;

using System;
﻿using System.Net.Mail;
using marimba_web.Common;

namespace marimba_web.Models
{
    public class Elector
    {
        public string name { get; set; }
        public Marimba.Instrument instrument { get; set; } 
        public MailAddress email { get; set; }
        public bool isMembershipPaid { get; set; }

        public Elector(string name, Marimba.Instrument instrument, MailAddress email, bool isMembershipPaid)
        {
            this.name = name;
            this.instrument = instrument;
            this.email = email;
            this.isMembershipPaid = isMembershipPaid;
        }

        // Setters
        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetInstrument(Marimba.Instrument instrument)
        {
            this.instrument = instrument;
        }

        public void SetEmail(MailAddress email)
        {
            this.email = email;
        }

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
