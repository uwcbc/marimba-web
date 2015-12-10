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
        /// </summary>
        /// <param name="data">Array of data corresponding to Excel cells</param>
        /// <param name="location">Location where file is to be saved</param>
        /// <param name="autofit">Whether or not to autofit the cell sizes</param>
        public static void saveExcel(object[,] data, string location, bool autofit = false, string dateColumns = null, string dateFormat = null)
        {

            //first, set it up
            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.Visible = false;
            Excel.Workbook ExcelWorkbook = ExcelApp.Workbooks.Add(Type.Missing);
            Excel.Worksheet ExcelWorksheet = ExcelWorkbook.Sheets[1];
            try
            {
                //next, fill in the data
                int row = data.GetLength(0);
                int column = data.GetLength(1);
                Excel.Range rng = ExcelWorksheet.Range[ExcelWorksheet.Cells[1, 1], ExcelWorksheet.Cells[row, column]];
                rng.set_Value(null, data);
                if (dateColumns != null)
                {
                    ExcelWorksheet.Columns[dateColumns].NumberFormat = dateFormat;
                }

                if (autofit)
                    ExcelWorksheet.Columns.AutoFit();

                ExcelWorkbook.SaveAs(location, Excel.XlFileFormat.xlWorkbookDefault);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorksheet);
                ExcelApp.Workbooks.Close();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorkbook);
                ExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelApp);
            }
            catch(System.Runtime.InteropServices.COMException)
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                System.Windows.Forms.MessageBox.Show("File was not saved. Unable to save file that is currently open.");
                //dump garbage
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorksheet);
                ExcelWorkbook.Close(false);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorkbook);
                ExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelApp);
            }
        }

        public static void saveExcelRowHighlight(object[,] data, string location, int col1, object crit1, System.Drawing.Color colour1, int col2, object crit2, System.Drawing.Color colour2)
        {

            //first, set it up
            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.Visible = false;
            Excel.Workbook ExcelWorkbook = ExcelApp.Workbooks.Add(Type.Missing);
            Excel.Worksheet ExcelWorksheet = ExcelWorkbook.Sheets[1];
            try
            {
                //next, fill in the data
                int row = data.GetLength(0);
                int column = data.GetLength(1);
                Excel.Range rng = ExcelWorksheet.Range[ExcelWorksheet.Cells[1, 1], ExcelWorksheet.Cells[row, column]];
                rng.set_Value(null, data);

                //do the highlighting
                for (int i = 1; i <= row; i++)
                {
                    if (data[i - 1, col1] == crit1)
                        ExcelWorksheet.Range[ExcelWorksheet.Cells[i, 1], ExcelWorksheet.Cells[i, column]].Interior.Color = colour1;
                    else if (data[i - 1, col2].ToString() == crit2.ToString())
                        ExcelWorksheet.Range[ExcelWorksheet.Cells[i, 1], ExcelWorksheet.Cells[i, column]].Interior.Color = colour2;
                }

                ExcelWorkbook.SaveAs(location, Excel.XlFileFormat.xlWorkbookDefault);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorksheet);
                ExcelApp.Workbooks.Close();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorkbook);
                ExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelApp);
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                System.Windows.Forms.MessageBox.Show("File was not saved. Unable to save file that is currently open.");
                //dump garbage
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorksheet);
                ExcelWorkbook.Close(false);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorkbook);
                ExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelApp);
            }
        }

        public static void saveFinancialStatement(object[,] data, string location, bool autofit = false)
        {
            //this differs from saveExcel in that it adds the nice underlining one would expect of a financial statement

            //first, set it up
            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.Visible = false;
            Excel.Workbook ExcelWorkbook = ExcelApp.Workbooks.Add(Type.Missing);
            Excel.Worksheet ExcelWorksheet = ExcelWorkbook.Sheets[1];

            try
            {
                //next, fill in the data
                int row = data.GetLength(0);
                int column = data.GetLength(1);
                Excel.Range rng = ExcelWorksheet.Range[ExcelWorksheet.Cells[1, 1], ExcelWorksheet.Cells[row, column]];
                rng.set_Value(null, data);

                if (autofit)
                    ExcelWorksheet.Columns.AutoFit();

                column -= 2;

                //merge the first two rows
                ExcelWorksheet.Range[ExcelWorksheet.Cells[1, 1], ExcelWorksheet.Cells[1, column]].Merge();
                ExcelWorksheet.Range[ExcelWorksheet.Cells[2, 1], ExcelWorksheet.Cells[2, column]].Merge();

                //add underlining
                for (int j = 4; j <= row; j++)
                {
                    for (int i = 2; i < column; i++)
                    {
                        if (!String.IsNullOrEmpty((string)data[j - 1, i - 1]) && !String.IsNullOrEmpty((string)data[j - 1, i]))
                            ExcelWorksheet.Cells[j, i].Borders(9).LineStyle = Excel.XlLineStyle.xlContinuous;
                        else if (!String.IsNullOrEmpty((string)data[j - 1, i - 1]) && j < row && !String.IsNullOrEmpty((string)data[j, i]) && String.IsNullOrEmpty((string)data[j, i - 1]))
                            ExcelWorksheet.Cells[j, i].Borders(9).LineStyle = Excel.XlLineStyle.xlContinuous;
                    }
                    //add double line if applicable
                    if (data[j - 1, column - 1] != null && data[j - 2, column - 1] == null && data[j - 2, column - 2] == null && (j == row || data[j, column - 1] == null))
                        ExcelWorksheet.Cells[j, column].Borders(9).LineStyle = Excel.XlLineStyle.xlDouble;
                }

                ExcelWorkbook.SaveAs(location, Excel.XlFileFormat.xlWorkbookDefault);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorksheet);
                ExcelApp.Workbooks.Close();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorkbook);
                ExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelApp);
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                System.Windows.Forms.MessageBox.Show("File was not saved. Unable to save file that is currently open.");
                //dump garbage
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorksheet);
                ExcelWorkbook.Close(false);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelWorkbook);
                ExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ExcelApp);
            }
        }

        /// <summary>
        /// This allows for manual editing of *most* of the .mrb file in Marimba. It can be edited in Excel and imported back.
        /// </summary>
        /// <param name="location">The file location to save to</param>
        public static void exportMrb(string location)
        {
            int iTotal = clsStorage.currentClub.iUser + clsStorage.currentClub.iMember + clsStorage.currentClub.sTerm*60 + clsStorage.currentClub.budget.Count + 1;
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
            object[,] data = new object[9 + clsStorage.currentClub.iUser, 4];
            data[0, 0] = "File Version";
            data[0, 1] = Marimba.club.FILE_VERSION;
            data[1, 0] = "Club Name";
            data[1, 1] = clsStorage.currentClub.strName;
            data[2, 0] = "Number of Users";
            data[2, 1] = clsStorage.currentClub.iUser;
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

            for (int i = 0; i<clsStorage.currentClub.iUser;i++)
            {
                for (int j = 0; j < 4; j++)
                    data[row, j] = clsStorage.currentClub.strUsers[i, j];
                row++;
                iCurrent++;
                Program.home.bwReport.ReportProgress((iCurrent*100) / iTotal);
            }

            Excel.Range updateRange = ExcelWorksheet.Range[ExcelWorksheet.Cells[1, 1], ExcelWorksheet.Cells[9 + clsStorage.currentClub.iUser, 4]];
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
            data = new object[1+clsStorage.currentClub.sTerm*373, 120];

            data[row, 0] = "Number of Terms";
            data[row, 1] = clsStorage.currentClub.sTerm;
            row++;

            for (int i = 0; i < clsStorage.currentClub.sTerm; i++ )
            {
                data[row, 0] = "Name Of Term";
                data[row, 1] = clsStorage.currentClub.terms[i].strName;
                row++;
                data[row, 0] = "Number of Members";
                data[row, 1] = clsStorage.currentClub.terms[i].sMembers;
                row++;
                data[row, 0] = "Term Index";
                data[row, 1] = clsStorage.currentClub.terms[i].sNumber;
                row++;
                data[row, 0] = "List of Members:";
                row++;

                //list of members
                for(int j = 0; j< clsStorage.currentClub.terms[i].sMembers; j++)
                    data[row, j] = clsStorage.currentClub.terms[i].members[j];
                row++;
                data[row, 0] = "Start Date";
                data[row, 1] = clsStorage.currentClub.terms[i].startDate.ToOADate();
                row++;
                data[row, 0] = "End Date";
                data[row, 1] = clsStorage.currentClub.terms[i].endDate.ToOADate();
                row++;
                data[row, 0] = "Number of Rehearsals";
                data[row, 1] = clsStorage.currentClub.terms[i].sRehearsals;
                row++;
                data[row, 0] = "Rehearsal Dates and Attendance";
                row++;

                //rehearsal dates headers
                for (int j = 0; j < clsStorage.currentClub.terms[i].sRehearsals; j++)
                    data[row, j + 1] = clsStorage.currentClub.terms[i].rehearsalDates[j].ToOADate();
                row++;

                //the actual attendance, along with the member's indexes
                for (int j = 0; j < clsStorage.currentClub.terms[i].sMembers; j++)
                {
                    for (int k = 0; k < clsStorage.currentClub.terms[i].sRehearsals + 1; k++)
                    {
                        if (k == 0)
                            data[row, k] = clsStorage.currentClub.terms[i].members[j];
                        else
                            data[row, k] = clsStorage.currentClub.terms[i].attendance[j, k-1];
                    }
                    row++;
                }

                //fees
                data[row, 0] = "Number of Other Fees";
                data[row, 1] = clsStorage.currentClub.terms[i].iOtherFees;
                row++;
                data[row, 1] = "Membership Fee";
                data[row+1, 1] = clsStorage.currentClub.terms[i].membershipFees;
                for (int j = 0; j < clsStorage.currentClub.terms[i].iOtherFees; j++ )
                {
                    data[row, j * 2 + 3] = clsStorage.currentClub.terms[i].strOtherFees[j];
                    data[row + 1, j * 2 + 3] = clsStorage.currentClub.terms[i].dOtherFees[j];
                }
                row += 2;

                //the fees paid, with the member's names
                for (int j = 0; j < clsStorage.currentClub.terms[i].sMembers; j++)
                {
                    for (int k = 0; k < clsStorage.currentClub.terms[i].iOtherFees + 2; k++)
                    {
                        if (k == 0)
                            data[row, k] = clsStorage.currentClub.terms[i].members[j];
                        else
                        {
                            data[row, k * 2 - 1] = clsStorage.currentClub.terms[i].feesPaid[j, k - 1];
                            data[row, k * 2] = clsStorage.currentClub.terms[i].feesPaidDate[j, k - 1].ToOADate();
                        }     
                    }
                    row++;
                }
                iCurrent+=60;
                Program.home.bwReport.ReportProgress((iCurrent * 100) / iTotal);
            }

            updateRange = ExcelWorksheet.Range[ExcelWorksheet.Cells[1, 1], ExcelWorksheet.Cells[1 + clsStorage.currentClub.sTerm * 373, 120]];
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

            //for integrity purposes, election and history will not be allowed to be edited this way

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
                output.iUser = Convert.ToInt16(valueArray[3, 2]);
                output.strEmail = (string)valueArray[4, 2];
                output.strImap = (string)valueArray[5, 2];
                output.bImap = (Boolean)valueArray[6, 2];
                output.strSmtp = (string)valueArray[7, 2];
                output.iSmtp = Convert.ToInt16(valueArray[8, 2]);
                output.bSmtp = (Boolean)valueArray[9, 2];

                //load Users
                for (int i = 0; i < output.iUser; i++)
                    for (int j = 0; j < 4; j++)
                        output.strUsers[i, j] = (string)valueArray[i + 10, j+1];

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
                output.sTerm = Convert.ToInt16(valueArray[1,2]);
                output.terms = new term[output.sTerm];
                for (int i = 0; i < output.sTerm;i++)
                {
                    output.terms[i] = new term();
                    output.terms[i].strName = (string)valueArray[row, 2];
                    row++;
                    output.terms[i].sMembers = Convert.ToInt16(valueArray[row, 2]);
                    row++;
                    output.terms[i].sNumber = Convert.ToInt16(valueArray[row, 2]);
                    row += 2;
                    for (int j = 0; j < output.terms[i].sMembers; j++)
                        output.terms[i].members[j] = Convert.ToInt16(valueArray[row, j + 1]);
                    row++;
                    output.terms[i].startDate = DateTime.FromOADate((double)valueArray[row, 2]);
                    row++;
                    output.terms[i].endDate = DateTime.FromOADate((double)valueArray[row, 2]);
                    row++;
                    output.terms[i].sRehearsals = Convert.ToInt16(valueArray[row, 2]);
                    row+=2;
                    //load rehearsal dates
                    output.terms[i].rehearsalDates = new DateTime[output.terms[i].sRehearsals];
                    for (int j = 0; j < output.terms[i].sRehearsals; j++)
                        output.terms[i].rehearsalDates[j] = DateTime.FromOADate((double)valueArray[row, j + 2]);
                    row++;


                    //load attendance
                    output.terms[i].attendance = new bool[120, output.terms[i].sRehearsals];
                    for(int j = 0; j  <output.terms[i].sMembers;j++)
                    {
                        for (int k = 0; k < output.terms[i].sRehearsals; k++)
                            output.terms[i].attendance[j, k] = (bool)valueArray[row, k + 2];
                        row++;
                    }

                    //load fees
                    output.terms[i].iOtherFees = Convert.ToInt32(valueArray[row, 2]);
                    row++;
                    //membership fee
                    output.terms[i].membershipFees = (double)valueArray[row+1, 2];
                    //other fees
                    output.terms[i].strOtherFees = new string[output.terms[i].iOtherFees];
                    output.terms[i].dOtherFees = new double[output.terms[i].iOtherFees];
                    for(int j = 0; j < output.terms[i].iOtherFees;j++)
                    {
                        output.terms[i].strOtherFees[j] = (string)valueArray[row, 4 + j * 2];
                        output.terms[i].dOtherFees[j] = (double)valueArray[row+1, 4 + j * 2];
                    }
                    row += 2;


                    //load who has paid
                    output.terms[i].feesPaid = new double[120, output.terms[i].iOtherFees+1];
                    output.terms[i].feesPaidDate = new DateTime[120, output.terms[i].iOtherFees + 1];
                    for(int j = 0; j < output.terms[i].sMembers;j++)
                    {
                        for(int k = 0; k < output.terms[i].iOtherFees+1; k++)
                        {
                            output.terms[i].feesPaid[j, k] = (double)valueArray[row, 2 + k * 2];
                            if (output.terms[i].feesPaid[j, k] != 0)
                                output.terms[i].feesPaidDate[j, k] = DateTime.FromOADate((double)valueArray[row, 3 + k * 2]);
                        }
                        row++;
                    }
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
                output.iHistory = currentClub.iHistory;
                output.historyList = currentClub.historyList;

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
