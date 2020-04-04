using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace marimba_web.Models
{
    /// <summary>
    /// Describes a Term's member attendance record.
    /// </summary>
    public class TermAttendance
    {
        /// <summary>
        /// List of all Rehearsals.
        /// </summary>
        public List<Rehearsal> allRehearsals { get; set; }

        /// <summary>
        /// Table that stores Member attendance records for a term.
        /// The first column is the Member GUID and the other columns correspond to each
        /// rehearsal date.
        /// </summary>
        public DataTable attendanceTable { get; set; }

        public TermAttendance(Term term)
        {
            allRehearsals = term.rehearsals.ToList();
            attendanceTable = new DataTable();
            // Add column for Member GUID
            attendanceTable.Columns.Add("memberId", typeof(Guid));
            // Add columns for each rehearsal date
            allRehearsals.ForEach(r =>
            {
                DataColumn col = new DataColumn(r.date.ToShortDateString(), typeof(bool))
                {
                    DefaultValue = false
                };
                attendanceTable.Columns.Add(col);
            });

            // For every Member and every Rehearsal, mark whether they were present or absent for the Rehearsal.
            List<Guid> memberIds = term.members.ToList();
            memberIds.ForEach(id =>
            {
                List<Rehearsal> attendedRehearsals = allRehearsals.Where(r => r.members.Contains(id)).ToList();
                DataRow row = attendanceTable.NewRow();
                row["memberId"] = id;
                attendedRehearsals.ForEach(r => row[r.date.ToShortDateString()] = true);
                attendanceTable.Rows.Add(row);
            });
        }

        /// <summary>
        /// Export attendance record as CSV, in raw bytes.
        /// </summary>
        public byte[] WriteCsvBytes()
        {
            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream);
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            /*
             * Need to manually write CSV file because CsvHelper does not support
             * writing DataTables out of the box.
             */
            // Write CSV column row
            foreach (DataColumn column in attendanceTable.Columns)
            {
                csvWriter.WriteField(column.ColumnName);
            }
            csvWriter.NextRecord();

            // Write data rows
            foreach (DataRow row in attendanceTable.Rows)
            {
                for (var i = 0; i < attendanceTable.Columns.Count; i++)
                {
                    csvWriter.WriteField(row[i]);
                }
                csvWriter.NextRecord();
            }

            streamWriter.Flush();
            return memoryStream.ToArray();
        }
    }
}
