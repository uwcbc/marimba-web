using System;
using System.Net.Mail;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace marimba_web.Models.Converters
{
    public class EmailConverter: DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return new MailAddress(text);
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is MailAddress mailAddress)
            {
                return mailAddress.Address;
            }

            return base.ConvertToString(value, row, memberMapData);
        }
    }
}

