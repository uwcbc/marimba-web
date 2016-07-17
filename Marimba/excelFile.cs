using ClosedXML.Excel;
using Marimba.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;

namespace Marimba
{
    class excelFile
    {
        /// <summary>
        /// Saves array into an .xlsx file
        /// Note: If a data entry is a double, it is treated as currency.
        /// </summary>
        /// <param name="data">Array of data corresponding to Excel cells</param>
        /// <param name="location">Location where file is to be saved</param>
        /// <param name="dateFormat">The format to use when saving dates</param>
        /// <returns>Whether the file was successfully saved</returns>
        public static void saveExcel(object[,] data, string location, string dateFormat = null)
        {
            try
            {
                var workbook = new XLWorkbook();
                workbook.Author = clsStorage.currentClub.strCurrentUser;
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
                    sound.success.Play();
            }
            catch (Exception e)
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                System.Windows.Forms.MessageBox.Show("File was not saved. " + e.Message);
            }
        }

        public static void saveExcelRowHighlight(object[,] data, string location, IList<ExcelHighlightingInfo> highlightingCriteria)
        {
            try
            {
                var workbook = new XLWorkbook();
                workbook.Author = clsStorage.currentClub.strCurrentUser;
                var worksheet = workbook.Worksheets.Add("Sheet 1");

                int rows = data.GetLength(0);
                int columns = data.GetLength(1);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        worksheet.Cell(i + 1, j + 1).Value = data[i, j];
                    }
                }
                var range = worksheet.RangeUsed();

                foreach (var criteria in highlightingCriteria)
                {
                    range.Rows(row => row.Cell(criteria.column).GetString() == criteria.matchExpression)
                        .ForEach(row =>row.Style.Fill.BackgroundColor = XLColor.FromColor(criteria.colour));
                }

                workbook.SaveAs(location);
                if (Properties.Settings.Default.playSounds)
                    sound.success.Play();
            }
            catch (Exception e)
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
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
                workbook.Author = clsStorage.currentClub.strCurrentUser;
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

                        /*
                         * for any entry that deducts from a total amount, then the remaining amount is displayed to the right of the current value
                         * -------------------------------------------
                         * | Non-empty (bottom-bordered) | Non-empty |
                         * -------------------------------------------
                         */
                        if (currentCellFilled && rightCellFilled) {
                            worksheet.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        }
                        /*
                         * for any entry that is the bottom of a column of totals
                         * ---------------------------------------------
                         * | Non-empty (bottom-bordered) | Unknown     |
                         * ---------------------------------------------
                         * | Empty                       | Non-empty   |
                         * ---------------------------------------------
                         */
                        else if (currentCellFilled && belowRightCellFilled && !belowCellFilled)
                        {
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
                    sound.success.Play();
            }
            catch (Exception e)
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                System.Windows.Forms.MessageBox.Show("File was not saved. " + e.Message);
            }
        }

        /// <summary>
        /// This allows for manual editing of *most* of the .mrb file in Marimba. It can be edited in Excel and imported back.
        /// </summary>
        /// <param name="location">The file location to save to</param>
        public static void exportMrb(string location)
        {
            int iTotal = clsStorage.currentClub.strUsers.Count + clsStorage.currentClub.iMember + clsStorage.currentClub.listTerms.Count*60 + clsStorage.currentClub.budget.Count + 1;
            int iCurrent = 0;

            string[] writableList;

            //first, set it up
            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.Visible = false;
            Excel.Workbook ExcelWorkbook = ExcelApp.Workbooks.Add(Type.Missing);
            Excel.Worksheet ExcelWorksheet = ExcelWorkbook.Sheets[1];
            ExcelWorksheet.Name = "General";
            ExcelApp.ScreenUpdating = false;

            //now, we'll use a similar approach to how the club would actually be saved
            int row = 9;
            object[,] data = new object[9 + clsStorage.currentClub.strUsers.Count, 4];
            data[0, 0] = "File Version";
            data[0, 1] = Marimba.club.FILE_VERSION;
            data[1, 0] = "Club Name";
            data[1, 1] = clsStorage.currentClub.strName;
            data[2, 0] = "Number of Users";
            data[2, 1] = clsStorage.currentClub.strUsers.Count;
            data[3, 0] = "Email Address";
            data[3, 1] = clsStorage.currentClub.strEmail;
            data[4, 0] = "IMAP Address";
            data[4, 1] = clsStorage.currentClub.strImap;
            data[5, 0] = "IMAP SSL";
            data[5, 1] = clsStorage.currentClub.bImap;
            data[6, 0] = "SMTP Address";
            data[6, 1] = clsStorage.currentClub.strSmtp;
            data[7, 0] = "SMTP Port";
            data[7, 1] = clsStorage.currentClub.iSmtp;
            data[8, 0] = "SMTP SSL";
            data[8, 1] = clsStorage.currentClub.bSmtp;

            foreach (string[] user in clsStorage.currentClub.strUsers)
            {
                for (int j = 0; j < 4; j++)
                    data[row, j] = user[j];
                row++;
                iCurrent++;
                Program.home.bwReport.ReportProgress((iCurrent*100) / iTotal);
            }

            Excel.Range updateRange = ExcelWorksheet.Range[ExcelWorksheet.Cells[1, 1], ExcelWorksheet.Cells[9 + clsStorage.currentClub.strUsers.Count, 4]];
            updateRange.set_Value(null, data);

            //Member Tab
            
            ExcelWorkbook.Sheets.Add(After: ExcelWorksheet);
            ExcelWorksheet = ExcelWorkbook.Sheets[2];
            ExcelWorksheet.Name = "Members";
            //reset row for new tab
            row = 0;
            //reset data for new tab
            data = new object[2 + clsStorage.currentClub.iMember, 12 + Enum.GetValues(typeof(member.instrument)).Length];

            data[row, 0] = "Number of Members";
            data[row, 1]= clsStorage.currentClub.iMember;
            row++;

            //Note: Will need to add multiple instruments later
            writableList = new string[] {"First Name", "Last Name", "Type","Student Number", "Faculty", "Instrument", "E-mail", "Other", "ID", "Signup Time", "Shirt Size", "Multiple Instruments"};
            for (int i = 0; i < writableList.Length; i++)
                data[row, i] = writableList[i];
            for (int i = 0; i < Enum.GetValues(typeof(member.instrument)).Length; i++)
                data[row, i + writableList.Length] = member.instrumentToString((member.instrument)i);
            row++;

            object[] memberDetails;
            for(int i = 0; i < clsStorage.currentClub.iMember; i++)
            {
                memberDetails = clsStorage.currentClub.members[i].exportMember().ToArray();
                for (int j = 0; j < writableList.Length; j++)
                    data[row, j] = memberDetails[j];
                //if the member plays multiple instruments, include that here
                if ((bool)memberDetails[writableList.Length - 1])
                    for (int j = 0; j < Enum.GetValues(typeof(member.instrument)).Length; j++)
                        data[row, j + memberDetails.Length - 1] = ((bool[])(memberDetails[memberDetails.Length - 1]))[j];
                        row++;
                iCurrent++;
                Program.home.bwReport.ReportProgress((iCurrent * 100) / iTotal);
            }

            updateRange = ExcelWorksheet.Range[ExcelWorksheet.Cells[1, 1], ExcelWorksheet.Cells[2 + clsStorage.currentClub.iMember, 12 + Enum.GetValues(typeof(member.instrument)).Length]];
            updateRange.set_Value(null, data);

            //Term Tab

            ExcelWorkbook.Sheets.Add(After: ExcelWorksheet);
            ExcelWorksheet = ExcelWorkbook.Sheets[3];
            ExcelWorksheet.Name = "Terms";
            //reset row for new tab
            row = 0;
            //reset data for new tab
            data = new object[1+clsStorage.currentClub.listTerms.Count*373, 120];

            data[row, 0] = "Number of Terms";
            data[row, 1] = clsStorage.currentClub.listTerms.Count;
            row++;

            foreach (term currentTerm in clsStorage.currentClub.listTerms)
            {
                data[row, 0] = "Name Of Term";
                data[row, 1] = currentTerm.strName;
                row++;
                data[row, 0] = "Number of Members";
                data[row, 1] = currentTerm.sMembers;
                row++;
                data[row, 0] = "Term Index";
                data[row, 1] = currentTerm.sNumber;
                row++;
                data[row, 0] = "List of Members:";
                row++;

                //list of members
                for(int j = 0; j< currentTerm.sMembers; j++)
                    data[row, j] = currentTerm.members[j];
                row++;
                data[row, 0] = "Start Date";
                data[row, 1] = currentTerm.startDate.ToOADate();
                row++;
                data[row, 0] = "End Date";
                data[row, 1] = currentTerm.endDate.ToOADate();
                row++;
                data[row, 0] = "Number of Rehearsals";
                data[row, 1] = currentTerm.sRehearsals;
                row++;
                data[row, 0] = "Rehearsal Dates and Attendance";
                row++;

                //rehearsal dates headers
                for (int j = 0; j < currentTerm.sRehearsals; j++)
                    data[row, j + 1] = currentTerm.rehearsalDates[j].ToOADate();
                row++;

                //the actual attendance, along with the member's indexes
                for (int j = 0; j < currentTerm.sMembers; j++)
                {
                    for (int k = 0; k < currentTerm.sRehearsals + 1; k++)
                    {
                        if (k == 0)
                            data[row, k] = currentTerm.members[j];
                        else
                            data[row, k] = currentTerm.attendance[j, k-1];
                    }
                    row++;
                }

                //fees
                data[row, 0] = "Number of Other Fees";
                data[row, 1] = currentTerm.iOtherFees;
                row++;
                data[row, 1] = "Membership Fee";
                data[row+1, 1] = currentTerm.membershipFees;
                for (int j = 0; j < currentTerm.iOtherFees; j++ )
                {
                    data[row, j * 2 + 3] = currentTerm.strOtherFees[j];
                    data[row + 1, j * 2 + 3] = currentTerm.dOtherFees[j];
                }
                row += 2;

                //the fees paid, with the member's names
                for (int j = 0; j < currentTerm.sMembers; j++)
                {
                    for (int k = 0; k < currentTerm.iOtherFees + 2; k++)
                    {
                        if (k == 0)
                            data[row, k] = currentTerm.members[j];
                        else
                        {
                            data[row, k * 2 - 1] = currentTerm.feesPaid[j, k - 1];
                            data[row, k * 2] = currentTerm.feesPaidDate[j, k - 1].ToOADate();
                        }     
                    }
                    row++;
                }
                iCurrent+=60;
                Program.home.bwReport.ReportProgress((iCurrent * 100) / iTotal);
            }

            updateRange = ExcelWorksheet.Range[ExcelWorksheet.Cells[1, 1], ExcelWorksheet.Cells[1 + clsStorage.currentClub.listTerms.Count * 373, 120]];
            updateRange.set_Value(null, data);

            // Budget Tab

            ExcelWorkbook.Sheets.Add(After: ExcelWorksheet);
            ExcelWorksheet = ExcelWorkbook.Sheets[4];
            ExcelWorksheet.Name = "Budget";
            //reset row for new tab
            row = 0;
            //reset data for new tab
            data = new object[2+clsStorage.currentClub.budget.Count, 9];

            data[row, 0] = "Number of Budget Items";
            data[row, 1] = clsStorage.currentClub.budget.Count;
            row++;

            writableList = new string[] {"Name", "Value", "Date Occur","Date Account", "Category", "Type", "Term", "Comment", "Asset for Depreciation"};
            for (int i = 0; i < writableList.Length; i++)
                data[row, i] = writableList[i];
            row++;

            foreach (budgetItem item in clsStorage.currentClub.budget)
            {
                data[row, 0] = item.name;
                data[row, 1] = item.value;
                data[row, 2] = item.dateOccur.ToOADate();
                data[row, 3] = item.dateAccount.ToOADate();
                data[row, 4] = item.cat;
                data[row, 5] = item.type;
                data[row, 6] = item.term;
                data[row, 7] = item.comment;
                //only include for depreciation assets
                if (item.type == 1)
                    data[row, 8] = clsStorage.currentClub.budget.IndexOf(item.depOfAsset);
                row++;
                iCurrent++;
                Program.home.bwReport.ReportProgress((iCurrent * 100) / iTotal);
            }

            updateRange = ExcelWorksheet.Range[ExcelWorksheet.Cells[1, 1], ExcelWorksheet.Cells[2 + clsStorage.currentClub.budget.Count, 9]];
            updateRange.set_Value(null, data);

            //for integrity purposes, history will not be allowed to be edited this way

            ExcelApp.ScreenUpdating = true;
            

            //finally, we can save and close
            ExcelWorkbook.SaveAs(location, Excel.XlFileFormat.xlWorkbookDefault);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorksheet);
            ExcelApp.Workbooks.Close();
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorkbook);
            ExcelApp.Quit();
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelApp);

            //reset the progress bar
            Program.home.bwReport.ReportProgress(100);
        }

        /// <summary>
        /// Loads edits made to .mrb file in Excel
        /// </summary>
        /// <param name="location">Location of .xlsx file</param>
        /// <param name="currentClub">Current club</param>
        /// <returns>Club with edits</returns>
        public static club loadFromExcel(string location, string newLocation, club currentClub)
        {
            club output = clsStorage.currentClub.clubClone(newLocation);

            //open the Excel file
            Excel.Application ExcelApp = new Excel.Application();
            Excel.Workbook workbook = ExcelApp.Workbooks.Open(location);
            //open the first sheet, General
            Excel.Worksheet worksheet = workbook.Sheets[1];

            //get the data in the sheet
            Excel.Range excelRange = worksheet.UsedRange;
	        object[,] valueArray = (object[,])excelRange.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);

            int version = Convert.ToInt32(valueArray[1, 2]);
            //check to see if this file is designed to work with this version
            if(version >= 2)
            {
                output.strName = (string)valueArray[2, 2];
                int iUser = Convert.ToInt16(valueArray[3, 2]);
                output.strUsers = new List<string[]>(iUser);
                output.strEmail = (string)valueArray[4, 2];
                output.strImap = (string)valueArray[5, 2];
                output.bImap = (Boolean)valueArray[6, 2];
                output.strSmtp = (string)valueArray[7, 2];
                output.iSmtp = Convert.ToInt16(valueArray[8, 2]);
                output.bSmtp = (Boolean)valueArray[9, 2];

                //load Users
                for (int i = 0; i < iUser; i++)
                {
                    string[] newUser = new string[4];
                    for (int j = 0; j < 4; j++)
                    {
                        newUser[j] = (string)valueArray[i + 10, j + 1];
                    }
                    output.strUsers.Add(newUser);
                }

                //Members tab

                //load sheet and data
                worksheet = workbook.Sheets[2];
                excelRange = worksheet.UsedRange;
                valueArray = (object[,])excelRange.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);
                valueArray = replaceNulls(valueArray);

                output.iMember = Convert.ToInt16(valueArray[1, 2]);
                //load Members
                bool[] tempMultipleInstruments = new bool[Enum.GetValues(typeof(member.instrument)).Length];
                for (int i = 0; i < output.iMember;i++)
                {
                    //if the member does not play multiple instruments
                    if(!(bool)valueArray[i+3,12])
                        output.members[i] = new member((string)valueArray[i + 3, 1], (string)valueArray[i + 3, 2], Convert.ToInt32(valueArray[i + 3, 3]), Convert.ToUInt32(valueArray[i + 3, 4]),
                            Convert.ToInt32(valueArray[i + 3, 5]), (string)valueArray[i + 3, 6], (string)valueArray[i + 3, 7], (string)valueArray[i + 3, 8],
                            Convert.ToInt16(valueArray[i + 3, 9]), DateTime.FromOADate((double)valueArray[i + 3, 10]), Convert.ToInt32(valueArray[i + 3, 11]));
                    else
                    {
                        //the member plays multiple instruments
                        //create their array of instruments they play first

                        for (int j = 0; j < Enum.GetValues(typeof(member.instrument)).Length; j++)
                            tempMultipleInstruments[j] = Convert.ToBoolean(valueArray[i + 3, 13 + j]);
                        output.members[i] = new member((string)valueArray[i + 3, 1], (string)valueArray[i + 3, 2], Convert.ToInt32(valueArray[i + 3, 3]), Convert.ToUInt32(valueArray[i + 3, 4]),
                            Convert.ToInt32(valueArray[i + 3, 5]), (string)valueArray[i + 3, 6], (string)valueArray[i + 3, 7], (string)valueArray[i + 3, 8],
                            Convert.ToInt16(valueArray[i + 3, 9]), DateTime.FromOADate((double)valueArray[i + 3, 10]), Convert.ToInt32(valueArray[i + 3, 11]), tempMultipleInstruments);
                    }
                }

                //Terms tab
                //This is the awful one

                //load sheet and data
                worksheet = workbook.Sheets[3];
                excelRange = worksheet.UsedRange;
                valueArray = (object[,])excelRange.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);
                valueArray = replaceNulls(valueArray);

                int row = 2;
                short sTerm = Convert.ToInt16(valueArray[1,2]);
                output.listTerms = new List<term>(sTerm);
                for (int i = 0; i < sTerm; i++)
                {
                    term termToAdd = new term();
                    termToAdd.strName = (string)valueArray[row, 2];
                    row++;
                    termToAdd.sMembers = Convert.ToInt16(valueArray[row, 2]);
                    row++;
                    termToAdd.sNumber = Convert.ToInt16(valueArray[row, 2]);
                    row += 2;
                    for (int j = 0; j < termToAdd.sMembers; j++)
                        termToAdd.members[j] = Convert.ToInt16(valueArray[row, j + 1]);
                    row++;
                    termToAdd.startDate = DateTime.FromOADate((double)valueArray[row, 2]);
                    row++;
                    termToAdd.endDate = DateTime.FromOADate((double)valueArray[row, 2]);
                    row++;
                    termToAdd.sRehearsals = Convert.ToInt16(valueArray[row, 2]);
                    row+=2;
                    //load rehearsal dates
                    termToAdd.rehearsalDates = new DateTime[termToAdd.sRehearsals];
                    for (int j = 0; j < termToAdd.sRehearsals; j++)
                        termToAdd.rehearsalDates[j] = DateTime.FromOADate((double)valueArray[row, j + 2]);
                    row++;


                    //load attendance
                    termToAdd.attendance = new bool[120, termToAdd.sRehearsals];
                    for(int j = 0; j  <termToAdd.sMembers;j++)
                    {
                        for (int k = 0; k < termToAdd.sRehearsals; k++)
                            termToAdd.attendance[j, k] = (bool)valueArray[row, k + 2];
                        row++;
                    }

                    //load fees
                    termToAdd.iOtherFees = Convert.ToInt32(valueArray[row, 2]);
                    row++;
                    //membership fee
                    termToAdd.membershipFees = (double)valueArray[row+1, 2];
                    //other fees
                    termToAdd.strOtherFees = new string[termToAdd.iOtherFees];
                    termToAdd.dOtherFees = new double[termToAdd.iOtherFees];
                    for(int j = 0; j < termToAdd.iOtherFees;j++)
                    {
                        termToAdd.strOtherFees[j] = (string)valueArray[row, 4 + j * 2];
                        termToAdd.dOtherFees[j] = (double)valueArray[row+1, 4 + j * 2];
                    }
                    row += 2;


                    //load who has paid
                    termToAdd.feesPaid = new double[120, termToAdd.iOtherFees+1];
                    termToAdd.feesPaidDate = new DateTime[120, termToAdd.iOtherFees + 1];
                    for(int j = 0; j < termToAdd.sMembers;j++)
                    {
                        for(int k = 0; k < termToAdd.iOtherFees+1; k++)
                        {
                            termToAdd.feesPaid[j, k] = (double)valueArray[row, 2 + k * 2];
                            if (termToAdd.feesPaid[j, k] != 0)
                                termToAdd.feesPaidDate[j, k] = DateTime.FromOADate((double)valueArray[row, 3 + k * 2]);
                        }
                        row++;
                    }

                    output.listTerms.Add(termToAdd);
                }

                //Budget tab

                //load sheet and data
                worksheet = workbook.Sheets[4];
                excelRange = worksheet.UsedRange;
                valueArray = (object[,])excelRange.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);
                valueArray = replaceNulls(valueArray);

                int iBudget = Convert.ToInt32(valueArray[1, 2]);
                output.budget = new List<budgetItem>(iBudget);
                List<int> indicesOfDepreciators = new List<int>(iBudget);
                List<int> indicesOfDepreciatedAssets = new List<int>(iBudget);

                for (int i = 0; i < iBudget; i++)
                {
                    budgetItem newItem = new budgetItem();
                    newItem.value = (double)valueArray[i + 3, 2];
                    newItem.name = (string)valueArray[i + 3, 1];
                    newItem.dateOccur = DateTime.FromOADate((double)valueArray[i + 3, 3]);
                    newItem.dateAccount = DateTime.FromOADate((double)valueArray[i + 3, 4]);
                    newItem.cat = (string)valueArray[i + 3, 5];
                    newItem.type = Convert.ToInt32(valueArray[i + 3, 6]);
                    newItem.term = Convert.ToInt32(valueArray[i + 3, 7]);
                    newItem.comment = (string)valueArray[i + 3, 8];
                    newItem.depOfAsset = null;
                    output.budget.Add(newItem);

                    // if depreciation
                    if (output.budget[i].type == 1)
                    {
                        indicesOfDepreciators.Add(i);
                        indicesOfDepreciatedAssets.Add(Convert.ToInt32(valueArray[i + 3, 9]));
                    }
                }

                for (int i = 0; i < indicesOfDepreciators.Count; i++)
                {
                    int index = indicesOfDepreciators[i];
                    budgetItem depreciatedAsset = output.budget[indicesOfDepreciatedAssets[i]];
                    output.budget[index].depOfAsset = depreciatedAsset;
                }

                //copy the history from the current club file
                output.historyList = new List<history>(currentClub.historyList.Count);
                foreach (history item in currentClub.historyList)
                {
                    output.historyList.Add(item);
                }

                //close the workbook, try to prevent any massive memory leaks
                GC.Collect();
                GC.WaitForPendingFinalizers();

                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelRange);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(worksheet);
                workbook.Close(false, location, null);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(workbook);
                ExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelApp);
                
                return output;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("The .xlsx file loaded is not designed to work with this version of Marimba.", "Failed to load changes");
                return null;
            }
        }

        static object[,] replaceNulls(object[,] array)
        {
            int length, height;
            length = array.GetLength(0);
            height = array.GetLength(1);
            for (int i = 1; i <= length; i++)
                for (int j = 1; j <= height; j++)
                    if (array[i, j] == null)
                        array[i, j] = "";
            return array;
        }
    }
}
