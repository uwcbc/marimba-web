namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using ClosedXML.Excel;
    using Marimba.Utility;

    class ExcelFile
    {
        /// <summary>
        /// Saves array into an .xlsx file
        /// Note: If a data entry is a double, it is treated as currency.
        /// </summary>
        /// <param name="data">Array of data corresponding to Excel cells</param>
        /// <param name="location">Location where file is to be saved</param>
        /// <param name="dateFormat">The format to use when saving dates</param>
        public static void saveExcel(object[,] data, string location, string dateFormat = null)
        {
            try
            {
                var workbook = new XLWorkbook();
                workbook.Author = ClsStorage.currentClub.strCurrentUser;
                var worksheet = workbook.Worksheets.Add("Sheet 1");

                int rows = data.GetLength(0);
                int columns = data.GetLength(1);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        worksheet.Cell(i + 1, j + 1).Value = data[i, j];
                        if (dateFormat != null && data[i, j] is DateTime)
                        {
                            worksheet.Cell(i + 1, j + 1).Style.DateFormat.SetFormat(dateFormat);
                        }
                        else if (data[i, j] is double)
                        {
                            worksheet.Cell(i + 1, j + 1).Style.NumberFormat.SetFormat("$0.00");
                            worksheet.Cell(i + 1, j + 1).DataType = XLCellValues.Number;
                        }
                    }
                }

                worksheet.Rows().AdjustToContents();
                worksheet.Columns().AdjustToContents();

                workbook.SaveAs(location);
                if (Properties.Settings.Default.playSounds)
                    Sound.Success.Play();
            }
            catch (Exception e)
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Error.Play();
                System.Windows.Forms.MessageBox.Show("File was not saved. " + e.Message);
            }
        }

        public static void saveExcelRowHighlight(object[,] data, string location, IList<ExcelHighlightingInfo> highlightingCriteria)
        {
            try
            {
                var workbook = new XLWorkbook();
                workbook.Author = ClsStorage.currentClub.strCurrentUser;
                var worksheet = workbook.Worksheets.Add("Sheet 1");

                int rows = data.GetLength(0);
                int columns = data.GetLength(1);
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < columns; j++)
                        worksheet.Cell(i + 1, j + 1).Value = data[i, j];
                var range = worksheet.RangeUsed();

                foreach (var criteria in highlightingCriteria)
                    range.Rows(row => row.Cell(criteria.column).GetString() == criteria.matchExpression)
                        .ForEach(row => row.Style.Fill.BackgroundColor = XLColor.FromColor(criteria.colour));

                workbook.SaveAs(location);
                if (Properties.Settings.Default.playSounds)
                    Sound.Success.Play();
            }
            catch (Exception e)
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Error.Play();
                System.Windows.Forms.MessageBox.Show("File was not saved. " + e.Message);
            }
        }

        /// <summary>
        /// Saves the data as a financial statement; mostly just adds some underlining.
        /// Note: If a data entry is a double, it is treated as currency.
        /// </summary>
        /// <param name="data">Financial statement data</param>
        /// <param name="location">File location to save to</param>
        /// <param name="autofit">Whether to autofit the columns</param>
        public static void saveFinancialStatement(object[,] data, string location, bool autofit = false)
        {
            try
            {
                var workbook = new XLWorkbook();
                workbook.Author = ClsStorage.currentClub.strCurrentUser;
                var worksheet = workbook.Worksheets.Add("Sheet 1");

                int rows = data.GetLength(0);
                int columns = data.GetLength(1);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        worksheet.Cell(i + 1, j + 1).Value = data[i, j];
                        if (data[i, j] is double)
                        {
                            worksheet.Cell(i + 1, j + 1).Style.NumberFormat.SetFormat("$0.00");
                            worksheet.Cell(i + 1, j + 1).DataType = XLCellValues.Number;
                        }
                    }
                }

                worksheet.Rows().AdjustToContents();
                worksheet.Columns().AdjustToContents();

                // merge the first two rows
                worksheet.Range(1, 1, 1, columns).Merge();
                worksheet.Range(2, 1, 2, columns).Merge();

                columns -= 2;
                for (int i = 4; i <= rows; i++)
                {
                    for (int j = 2; j < columns; j++)
                    {
                        bool currentCellFilled = data[i - 1, j - 1] != null && !String.IsNullOrEmpty(data[i - 1, j - 1].ToString());
                        bool rightCellFilled = data[i - 1, j] != null && !String.IsNullOrEmpty(data[i - 1, j].ToString());
                        bool belowCellFilled = i < rows && data[i, j - 1] != null && !String.IsNullOrEmpty(data[i, j - 1].ToString());
                        bool belowRightCellFilled = i < rows && data[i, j] != null && !String.IsNullOrEmpty(data[i, j].ToString());

                        if (currentCellFilled && rightCellFilled)
                        {
                            /*
                             * for any entry that deducts from a total amount, then the remaining amount is displayed to the right of the current value
                             * -------------------------------------------
                             * | Non-empty (bottom-bordered) | Non-empty |
                             * -------------------------------------------
                             */
                            worksheet.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        }
                        else if (currentCellFilled && belowRightCellFilled && !belowCellFilled)
                        {
                            /*
                             * for any entry that is the bottom of a column of totals
                             * ---------------------------------------------
                             * | Non-empty (bottom-bordered) | Unknown     |
                             * ---------------------------------------------
                             * | Empty                       | Non-empty   |
                             * ---------------------------------------------
                             */
                            worksheet.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        }
                    }

                    bool lastCellFilled = data[i - 1, columns - 1] != null;
                    bool aboveLastCellFilled = data[i - 2, columns - 1] != null;
                    bool topLeftOfLastCellFilled = data[i - 2, columns - 2] != null;
                    bool belowLastCellFilled = i < rows && data[i, columns - 1] != null;
                    bool lastRow = i == rows;

                    /*
                     * double border applied if above and below rows are free, then double-border the last cell of this row
                     * -----------------------------------------
                     * | Empty   | Empty                       |
                     * -----------------------------------------
                     * | Unknown | Non-empty (double-bordered) |
                     * -----------------------------------------
                     * | Empty   | Empty                       |
                     * -----------------------------------------
                     */
                    if (lastCellFilled && !aboveLastCellFilled && !topLeftOfLastCellFilled && (lastRow || !belowLastCellFilled))
                        worksheet.Cell(i, columns).Style.Border.BottomBorder = XLBorderStyleValues.Double;
                }

                workbook.SaveAs(location);
                if (Properties.Settings.Default.playSounds)
                    Sound.Success.Play();
            }
            catch (Exception e)
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Error.Play();
                System.Windows.Forms.MessageBox.Show("File was not saved. " + e.Message);
            }
        }

        /// <summary>
        /// This allows for manual editing of *most* of the .mrb file in Marimba. It can be edited in Excel and imported back.
        /// </summary>
        /// <param name="location">The file location to save to</param>
        public static void exportMrb(string location)
        {
            int iTotal = ClsStorage.currentClub.strUsers.Count +
                ClsStorage.currentClub.iMember +
                ClsStorage.currentClub.listTerms.Count * 60 +
                ClsStorage.currentClub.budget.Count + 1;
            int iCurrent = 0;

            try
            {
                var workbook = new XLWorkbook();
                workbook.Author = ClsStorage.currentClub.strCurrentUser;
                
                /*
                 * General Sheet
                 */
                var worksheet = workbook.Worksheets.Add("General");

                worksheet.Cell(1, 1).Value = "File Version";
                worksheet.Cell(1, 2).Value = Marimba.Club.FileVersion;
                worksheet.Cell(2, 1).Value = "Club Name";
                worksheet.Cell(2, 2).Value = ClsStorage.currentClub.strName;
                worksheet.Cell(3, 1).Value = "Number of Users";
                worksheet.Cell(3, 2).Value = ClsStorage.currentClub.strUsers.Count;
                worksheet.Cell(4, 1).Value = "Email Address";
                worksheet.Cell(4, 2).Value = ClsStorage.currentClub.emailAddress;
                worksheet.Cell(5, 1).Value = "IMAP Address";
                worksheet.Cell(5, 2).Value = ClsStorage.currentClub.imapServerAddress;
                worksheet.Cell(6, 1).Value = "IMAP SSL";
                worksheet.Cell(6, 2).Value = ClsStorage.currentClub.bImap;
                worksheet.Cell(7, 1).Value = "SMTP Address";
                worksheet.Cell(7, 2).Value = ClsStorage.currentClub.smptServerAddress;
                worksheet.Cell(8, 1).Value = "SMTP Port";
                worksheet.Cell(8, 2).Value = ClsStorage.currentClub.smtpRequiresSSL;
                worksheet.Cell(9, 1).Value = "SMTP SSL";
                worksheet.Cell(9, 2).Value = ClsStorage.currentClub.imapRequiresSSL;

                int row = 10;
                foreach (string[] user in ClsStorage.currentClub.strUsers)
                {
                    for (int j = 0; j < user.Length; j++)
                        worksheet.Cell(row, j + 1).Value = user[j];
                    row++;
                    iCurrent++;
                    Program.home.bwReport.ReportProgress((iCurrent * 100) / iTotal);
                }

                /*
                 * Members Sheet
                 */
                worksheet = workbook.Worksheets.Add("Members");
                worksheet.Cell(1, 1).Value = "Number of Members";
                worksheet.Cell(1, 2).Value = ClsStorage.currentClub.iMember;

                List<string[]> headers = new List<string[]> { new string[] { "First Name", "Last Name", "Type", "Student Number", "Faculty", "Instrument", "E-mail", "Other", "ID", "Signup Time", "Shirt Size", "Multiple Instruments" } };
                worksheet.Cell(2, 1).Value = headers;
                for (int i = 0; i < Enum.GetValues(typeof(Member.Instrument)).Length; i++)
                {
                    worksheet.Cell(2, 1 + headers[0].Length + i).Value = Member.instrumentToString((Member.Instrument)i);
                }

                IList<object[]> memberListToExport = new List<object[]>();
                int dateColumn = 0;
                for (int i = 0; i < ClsStorage.currentClub.iMember; i++)
                {
                    List<object> memberToExport = ClsStorage.currentClub.members[i].exportMember();
                    for (int j = 0; j < memberToExport.Count; j++)
                    {
                        if (memberToExport[j] is DateTime)
                        {
                            memberToExport[j] = ((DateTime)memberToExport[j]).ToString("dd/MM/yyyy hh:mm:ss tt");
                            dateColumn = j + 1;
                        }
                    }

                    if (memberToExport.Last() is bool[])
                    {
                        bool[] instrumentsPlayed = (bool[])memberToExport.Last();
                        memberToExport.RemoveAt(memberToExport.Count - 1);
                        foreach (bool value in instrumentsPlayed)
                            memberToExport.Add(value);
                    }

                    memberListToExport.Add(memberToExport.ToArray());
                    iCurrent++;
                    Program.home.bwReport.ReportProgress((iCurrent * 100) / iTotal);
                }

                worksheet.Cell(3, 1).Value = memberListToExport;
                worksheet.Range(3, dateColumn, ClsStorage.currentClub.iMember, dateColumn).Style.DateFormat.Format = "dd/MM/yyyy hh:mm:ss AM/PM";

                /*
                 * Term Sheet
                 */
                worksheet = workbook.Worksheets.Add("Terms");
                worksheet.Cell(1, 1).Value = "Number of Terms";
                worksheet.Cell(1, 2).Value = ClsStorage.currentClub.listTerms.Count;
                row = 2;
                foreach (Term currentTerm in ClsStorage.currentClub.listTerms)
                {
                    // Preliminary Info
                    worksheet.Cell(row, 1).Value = "Name Of Term";
                    worksheet.Cell(row, 2).Value = currentTerm.strName;
                    row++;
                    worksheet.Cell(row, 1).Value = "Number of Members";
                    worksheet.Cell(row, 2).Value = currentTerm.numMembers;
                    row++;
                    worksheet.Cell(row, 1).Value = "Term Index";
                    worksheet.Cell(row, 2).Value = currentTerm.termIndex;
                    row++;
                    worksheet.Cell(row, 1).Value = "List of Members:";
                    for (int i = 0; i < currentTerm.numMembers; i++)
                    {
                        worksheet.Cell(row, 2 + i).Value = currentTerm.members[i];
                    }

                    row++;
                    worksheet.Cell(row, 1).Value = "Start Date";
                    worksheet.Cell(row, 2).Value = currentTerm.startDate.ToString("dd/MM/yyyy");
                    worksheet.Cell(row, 2).Style.DateFormat.Format = "dd/MM/yyyy";
                    row++;
                    worksheet.Cell(row, 1).Value = "End Date";
                    worksheet.Cell(row, 2).Value = currentTerm.endDate.ToString("dd/MM/yyyy");
                    worksheet.Cell(row, 2).Style.DateFormat.Format = "dd/MM/yyyy";
                    row++;
                    worksheet.Cell(row, 1).Value = "Number of Rehearsals";
                    worksheet.Cell(row, 2).Value = currentTerm.numRehearsals;
                    row++;
                    worksheet.Cell(row, 1).Value = "Rehearsal Dates and Attendance";
                    row++;

                    // rehearsal dates headers
                    for (int j = 0; j < currentTerm.rehearsalDates.Length; j++)
                        worksheet.Cell(row, 2 + j).Value = currentTerm.rehearsalDates[j].ToString("dd/MM/yyyy");
                    worksheet.Row(row).Style.DateFormat.Format = "dd/MM/yyyy";
                    row++;

                    // the actual attendance, along with the member's indexes
                    for (int j = 0; j < currentTerm.numMembers; j++)
                    {
                        worksheet.Cell(row, 1).Value = currentTerm.members[j];
                        for (int k = 0; k < currentTerm.numRehearsals; k++)
                            worksheet.Cell(row, 2 + k).Value = currentTerm.attendance[j, k];

                        row++;
                    }

                    // fees description
                    worksheet.Cell(row, 1).Value = "Number of Other Fees";
                    worksheet.Cell(row, 2).Value = currentTerm.numOtherFees;
                    row++;
                    worksheet.Cell(row, 2).Value = "Membership Fee";
                    for (int j = 0; j < currentTerm.numOtherFees; j++)
                    {
                        worksheet.Cell(row, 4 + j * 2).Value = currentTerm.otherFeesNames[j];
                    }

                    row++;
                    worksheet.Cell(row, 2).Value = currentTerm.membershipFees;
                    for (int j = 0; j < currentTerm.numOtherFees; j++)
                    {
                        worksheet.Cell(row, 4 + j * 2).Value = currentTerm.otherFeesAmounts[j];
                    }

                    row++;
                    
                    // fees record
                    for (int j = 0; j < currentTerm.numMembers; j++)
                    {
                        worksheet.Cell(row, 1).Value = currentTerm.members[j];
                        for (int k = 0; k < currentTerm.numOtherFees + 1; k++)
                        {
                            worksheet.Cell(row, 2 + k * 2).Value = currentTerm.feesPaid[j, k];
                            if (currentTerm.feesPaidDate[j, k].Ticks > 0)
                            {
                                worksheet.Cell(row, 3 + k * 2).Value = currentTerm.feesPaidDate[j, k].ToString("dd/MM/yyyy");
                                worksheet.Cell(row, 3 + k * 2).Style.DateFormat.Format = "dd/MM/yyyy";
                            }
                        }

                        row++;
                    }

                    iCurrent += 60;
                    Program.home.bwReport.ReportProgress((iCurrent * 100) / iTotal);
                }

                /*
                 * Budget Tab
                 */
                worksheet = workbook.Worksheets.Add("Budget");
                worksheet.Cell(1, 1).Value = "Number of Budget Items";
                worksheet.Cell(1, 2).Value = ClsStorage.currentClub.budget.Count;
                headers = new List<string[]> { new string[] { "Name", "Value", "Date Occur", "Date Account", "Category", "Type", "Term", "Comment", "Asset for Depreciation" } };
                worksheet.Cell(2, 1).Value = headers;
                row = 3;
                IList<object[]> budgetToExport = new List<object[]>();
                foreach (BudgetItem item in ClsStorage.currentClub.budget)
                {
                    object[] budgetItemToAdd = item.Export().ToArray();
                    for (int j = 0; j < budgetItemToAdd.Length; j++)
                    {
                        if (budgetItemToAdd[j] is DateTime)
                        {
                            budgetItemToAdd[j] = ((DateTime)budgetItemToAdd[j]).ToString("dd/MM/yyyy");
                        }
                    }

                    budgetToExport.Add(budgetItemToAdd);
                    iCurrent++;
                    Program.home.bwReport.ReportProgress((iCurrent * 100) / iTotal);
                }

                worksheet.Cell(3, 1).Value = budgetToExport;
                int column = 1;
                foreach (object value in budgetToExport[0])
                {
                    if (value is DateTime)
                        worksheet.Range(3, column, ClsStorage.currentClub.budget.Count + 3, column).Style.DateFormat.Format = "dd/MM/yyyy";
                    else if (value is double)
                    {
                        worksheet.Range(3, column, ClsStorage.currentClub.budget.Count + 3, column).Style.NumberFormat.Format = "$0.00";
                        worksheet.Range(3, column, ClsStorage.currentClub.budget.Count + 3, column).DataType = XLCellValues.Number;
                    }

                    column++;
                }

                worksheet.Columns().AdjustToContents();

                /* for integrity purposes, history will not be allowed to be edited this way */

                workbook.SaveAs(location);

                // reset the progress bar
                Program.home.bwReport.ReportProgress(100);
            }
            catch (Exception e)
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Error.Play();
                System.Windows.Forms.MessageBox.Show("File was not saved. " + e.Message);
            }
        }

        /// <summary>
        /// Loads edits made to .mrb file in Excel
        /// </summary>
        /// <param name="location">Location of .xlsx file</param>
        /// <param name="newLocation">Location of the new .mrb file</param>
        /// <param name="currentClub">Current club</param>
        /// <returns>Club with edits</returns>
        public static Club loadFromExcel(string location, string newLocation, Club currentClub)
        {
            // open the Excel file
            var workbook = new XLWorkbook(location);
            var worksheet = workbook.Worksheet("General");
            double version = Convert.ToDouble(worksheet.Cell(1, 2).Value);

            // check to see if this file is designed to work with this version
            if (version >= 2)
            {
                Club output = ClsStorage.currentClub.CloneClub(newLocation);
                output.fileVersion = version;
                output.strName = (string)worksheet.Cell(2, 2).Value;
                int iUser = Convert.ToInt16(worksheet.Cell(3, 2).Value);
                output.strUsers = new List<string[]>(iUser);
                output.emailAddress = (string)worksheet.Cell(4, 2).Value;
                output.imapServerAddress = (string)worksheet.Cell(5, 2).Value;
                output.bImap = (Boolean)worksheet.Cell(6, 2).Value;
                output.smptServerAddress = (string)worksheet.Cell(7, 2).Value;
                output.smtpRequiresSSL = Convert.ToInt16(worksheet.Cell(8, 2).Value);
                output.imapRequiresSSL = (Boolean)worksheet.Cell(9, 2).Value;

                // load Users
                for (int i = 0; i < iUser; i++)
                {
                    string[] newUser = new string[4];
                    for (int j = 0; j < 4; j++)
                        newUser[j] = (string)worksheet.Cell(i + 10, j + 1).Value;

                    output.strUsers.Add(newUser);
                }

                // Members tab
                // load sheet and data
                worksheet = workbook.Worksheet("Members");
                output.iMember = Convert.ToInt16(worksheet.Cell(1, 2).Value);

                // load Members
                bool[] tempMultipleInstruments = new bool[Enum.GetValues(typeof(Member.Instrument)).Length];
                for (int i = 0; i < output.iMember; i++)
                {
                    // if the member does not play multiple instruments
                    if (!(bool)worksheet.Cell(i + 3, 12).Value)
                        output.members[i] = new Member(
                            (string)worksheet.Cell(i + 3, 1).Value,
                            (string)worksheet.Cell(i + 3, 2).Value,
                            (Member.MemberType)Convert.ToInt32(worksheet.Cell(i + 3, 3).Value),
                            Convert.ToUInt32(worksheet.Cell(i + 3, 4).Value),
                            Convert.ToInt32(worksheet.Cell(i + 3, 5).Value),
                            (string)worksheet.Cell(i + 3, 6).Value,
                            (string)worksheet.Cell(i + 3, 7).Value,
                            (string)worksheet.Cell(i + 3, 8).Value,
                            Convert.ToInt16(worksheet.Cell(i + 3, 9).Value),
                            DateTime.ParseExact((string)worksheet.Cell(i + 3, 10).Value, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture),
                            Convert.ToInt32(worksheet.Cell(i + 3, 11).Value));
                    else
                    {
                        // the member plays multiple instruments
                        // create their array of instruments they play first
                        for (int j = 0; j < Enum.GetValues(typeof(Member.Instrument)).Length; j++)
                            tempMultipleInstruments[j] = Convert.ToBoolean(worksheet.Cell(i + 3, 13 + j).Value);
                        output.members[i] = new Member(
                            (string)worksheet.Cell(i + 3, 1).Value,
                            (string)worksheet.Cell(i + 3, 2).Value,
                            (Member.MemberType)Convert.ToInt32(worksheet.Cell(i + 3, 3).Value),
                            Convert.ToUInt32(worksheet.Cell(i + 3, 4).Value),
                            Convert.ToInt32(worksheet.Cell(i + 3, 5).Value),
                            (string)worksheet.Cell(i + 3, 6).Value,
                            (string)worksheet.Cell(i + 3, 7).Value,
                            (string)worksheet.Cell(i + 3, 8).Value,
                            Convert.ToInt16(worksheet.Cell(i + 3, 9).Value),
                            DateTime.ParseExact((string)worksheet.Cell(i + 3, 10).Value, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture),
                            Convert.ToInt32(worksheet.Cell(i + 3, 11).Value),
                            tempMultipleInstruments);
                    }
                }

                // Terms tab
                worksheet = workbook.Worksheet("Terms");

                int row = 2;
                short numTerms = Convert.ToInt16(worksheet.Cell(1, 2).Value);
                output.listTerms = new List<Term>(numTerms);
                for (int i = 0; i < numTerms; i++)
                {
                    Term termToAdd = new Term();
                    termToAdd.strName = (string)worksheet.Cell(row, 2).Value;
                    row++;
                    termToAdd.numMembers = Convert.ToInt16(worksheet.Cell(row, 2).Value);
                    row++;
                    termToAdd.termIndex = Convert.ToInt16(worksheet.Cell(row, 2).Value);
                    row++;
                    for (int j = 0; j < termToAdd.numMembers; j++)
                        termToAdd.members[j] = Convert.ToInt16(worksheet.Cell(row, j + 2).Value);
                    row++;
                    termToAdd.startDate = (worksheet.Cell(row, 2).Value is DateTime) ?
                        (DateTime)worksheet.Cell(row, 2).Value :
                        DateTime.ParseExact((string)worksheet.Cell(row, 2).Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    row++;
                    termToAdd.endDate = (worksheet.Cell(row, 2).Value is DateTime) ?
                        (DateTime)worksheet.Cell(row, 2).Value :
                        DateTime.ParseExact((string)worksheet.Cell(row, 2).Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    row++;
                    termToAdd.numRehearsals = Convert.ToInt16(worksheet.Cell(row, 2).Value);
                    row += 2;

                    // load rehearsal dates
                    termToAdd.rehearsalDates = new DateTime[termToAdd.numRehearsals];
                    for (int j = 0; j < termToAdd.numRehearsals; j++)
                        termToAdd.rehearsalDates[j] = (worksheet.Cell(row, j + 2).Value is DateTime) ?
                            (DateTime)worksheet.Cell(row, j + 2).Value :
                            DateTime.ParseExact((string)worksheet.Cell(row, j + 2).Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    row++;

                    // load attendance
                    termToAdd.attendance = new bool[120, termToAdd.numRehearsals];
                    for (int j = 0; j < termToAdd.numMembers; j++)
                    {
                        for (int k = 0; k < termToAdd.numRehearsals; k++)
                            termToAdd.attendance[j, k] = (bool)worksheet.Cell(row, k + 2).Value;
                        row++;
                    }

                    // load fees
                    termToAdd.numOtherFees = Convert.ToInt32(worksheet.Cell(row, 2).Value);
                    row++;

                    // membership fee
                    termToAdd.membershipFees = (double)worksheet.Cell(row + 1, 2).Value;

                    // other fees
                    termToAdd.otherFeesNames = new string[termToAdd.numOtherFees];
                    termToAdd.otherFeesAmounts = new double[termToAdd.numOtherFees];
                    for (int j = 0; j < termToAdd.numOtherFees; j++)
                    {
                        termToAdd.otherFeesNames[j] = (string)worksheet.Cell(row, 4 + j * 2).Value;
                        termToAdd.otherFeesAmounts[j] = (double)worksheet.Cell(row + 1, 4 + j * 2).Value;
                    }

                    row += 2;

                    // load who has paid
                    termToAdd.feesPaid = new double[120, termToAdd.numOtherFees + 1];
                    termToAdd.feesPaidDate = new DateTime[120, termToAdd.numOtherFees + 1];
                    for (int j = 0; j < termToAdd.numMembers; j++)
                    {
                        for (int k = 0; k < termToAdd.numOtherFees + 1; k++)
                        {
                            termToAdd.feesPaid[j, k] = (double)worksheet.Cell(row, 2 + k * 2).Value;
                            if (termToAdd.feesPaid[j, k] != 0)
                                termToAdd.feesPaidDate[j, k] = (worksheet.Cell(row, 3 + k * 2).Value is DateTime) ?
                                    (DateTime)worksheet.Cell(row, 3 + k * 2).Value :
                                    DateTime.ParseExact((string)worksheet.Cell(row, 3 + k * 2).Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }

                        row++;
                    }

                    output.listTerms.Add(termToAdd);
                }

                // Budget tab

                // load sheet and data
                worksheet = workbook.Worksheet("Budget");

                int iBudget = Convert.ToInt32(worksheet.Cell(1, 2).Value);
                output.budget = new List<BudgetItem>(iBudget);
                List<int> indicesOfDepreciators = new List<int>(iBudget);
                List<int> indicesOfDepreciatedAssets = new List<int>(iBudget);

                for (int i = 0; i < iBudget; i++)
                {
                    BudgetItem newItem = new BudgetItem();
                    newItem.value = (double)worksheet.Cell(i + 3, 2).Value;
                    newItem.name = (string)worksheet.Cell(i + 3, 1).Value;
                    newItem.dateOccur = (worksheet.Cell(i + 3, 3).Value is DateTime) ?
                        (DateTime)worksheet.Cell(i + 3, 3).Value :
                        DateTime.ParseExact((string)worksheet.Cell(i + 3, 3).Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    newItem.dateAccount = (worksheet.Cell(i + 3, 4).Value is DateTime) ?
                        (DateTime)worksheet.Cell(i + 3, 4).Value :
                        DateTime.ParseExact((string)worksheet.Cell(i + 3, 4).Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    newItem.cat = (string)worksheet.Cell(i + 3, 5).Value;
                    newItem.type = (TransactionType)Convert.ToInt32(worksheet.Cell(i + 3, 6).Value);
                    newItem.term = Convert.ToInt32(worksheet.Cell(i + 3, 7).Value);
                    newItem.comment = (string)worksheet.Cell(i + 3, 8).Value;
                    newItem.depOfAsset = null;
                    output.budget.Add(newItem);

                    // if depreciation
                    if (output.budget[i].type == TransactionType.Depreciation)
                    {
                        indicesOfDepreciators.Add(i);
                        indicesOfDepreciatedAssets.Add(Convert.ToInt32(worksheet.Cell(i + 3, 9).Value));
                    }
                }

                for (int i = 0; i < indicesOfDepreciators.Count; i++)
                {
                    int index = indicesOfDepreciators[i];
                    BudgetItem depreciatedAsset = output.budget[indicesOfDepreciatedAssets[i]];
                    output.budget[index].depOfAsset = depreciatedAsset;
                }

                // copy the history from the current club file
                output.historyList = new List<HistoryItem>(currentClub.historyList.Count);
                foreach (HistoryItem item in currentClub.historyList)
                {
                    output.historyList.Add(item);
                }
                
                return output;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("The .xlsx file loaded is not designed to work with this version of Marimba.", "Failed to load changes");
                return null;
            }
        }
    }
}
