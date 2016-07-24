using Marimba.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Marimba
{
    public partial class termFinancials : Form
    {
        public termFinancials()
        {
            InitializeComponent();
        }

        private void cbTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            //update limbo information
            ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].checkLimbo();
            btnExport.Enabled = true;
            //balance sheet
            lvBalance.Items.Clear();
            lvBalance.Items.Add("Balance Sheet");
            lvBalance.Items.Add("As of " + ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].endDate.ToLongDateString());
            lvBalance.Items.Add("");
            lvBalance.Items.Add("Assets");
            lvBalance.Items.Add("Current Assets");
            //calculate cash
            double dCash = 0, dAssets = 0;
            //prepare for 12 sources of assets
            double[] dAsset = new double[12];
            double[] dDep = new double[12];
            string[] strAsset = new string[12];

            foreach (BudgetItem item in ClsStorage.currentClub.budget)
            {

                //ignore all budget items that take effect after the last date of the term
                if (item.dateAccount <
                        ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].endDate.AddDays(1))
                {
                    //cash
                    if (item.type == TransactionType.Revenue)
                    {
                        dCash += item.value;
                        dAssets += item.value;
                    }
                    else if (item.type == TransactionType.Asset)
                    {
                        dCash -= item.value;
                    }
                    else if (item.type == TransactionType.Expense)
                    {
                        dCash -= item.value;
                        dAssets -= item.value;
                    }
                    //depreciation is not cash, so ignore it for cash
                    else
                    {
                        dAssets -= item.value;
                    }
                    //capital assets & depreciation
                    //ignore fully depreciated assets up to this date
                    if (item.type == TransactionType.Asset && !ClsStorage.currentClub.DetermineIsFullyDepreciated(item, ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].endDate.AddDays(1))
                        || item.type == TransactionType.Depreciation
                        && !ClsStorage.currentClub.DetermineIsFullyDepreciated(item.depOfAsset, ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].endDate.AddDays(1)))
                    {
                        int iIndex = iRevenueSearch(0, 11, item.cat, strAsset, true);
                        if (iIndex == -1) //asset category does not exist
                        {
                            //find the spot where it SHOULD be
                            iIndex = iRevenueSearch(0, 11, item.cat, strAsset, false);
                            //move everything else in the array over
                            for (int j = 9; j > iIndex; j--)
                            {
                                strAsset[j] = strAsset[j - 1];
                                if (item.type == TransactionType.Asset)
                                    dAsset[j] = dAsset[j - 1];
                                else
                                    dDep[j] = dDep[j - 1];


                            }
                            strAsset[iIndex] = item.cat;
                            if (item.type == TransactionType.Asset)
                                dAsset[iIndex] = item.value;
                            else
                                dDep[iIndex] = item.value;
                        }
                        else //asset category already exists
                        {
                            if (item.type == TransactionType.Asset)
                                dAsset[iIndex] += item.value;
                            else
                                dDep[iIndex] += item.value;
                        }
                    }
                }
            }
            lvBalance.Items.Add(new ListViewItem(new string[3] { " Cash", "", dCash.ToString("C") }));
            lvBalance.Items.Add(new ListViewItem(new string[4] { "Total Current Assets", "", "", dCash.ToString("C") }));
            lvBalance.Items.Add("Capital Assets");

            for (int i = 0; i < 12; i++)
            {
                if (strAsset[i] == null || strAsset[i] == "")
                    break;
                else if (dDep[i] == 0)
                    lvBalance.Items.Add(new ListViewItem(new string[3] { " " + strAsset[i], "", dAsset[i].ToString("C") }));
                else
                {
                    lvBalance.Items.Add(new ListViewItem(new string[2] { " " + strAsset[i], dAsset[i].ToString("C") }));
                    lvBalance.Items.Add(new ListViewItem(new string[3] { "  Less: Accumulated Amortization", dDep[i].ToString("C"), (dAsset[i] - dDep[i]).ToString("C") }));
                }
            }
            
            lvBalance.Items.Add(new ListViewItem(new string[4] { "Total Capital Assets", "", "", (dAssets-dCash).ToString("C") }));
            lvBalance.Items.Add("");
            lvBalance.Items.Add(new ListViewItem(new string[4] { "Total Assets", "", "", dAssets.ToString("C") }));
            lvBalance.Items.Add("");
            //liabilities and equity is pretty easy because it is essentially all retained earnings
            //perhaps this section may be more complicated in the future
            lvBalance.Items.Add("Liabilities And Equity");
            lvBalance.Items.Add("Equity");
            lvBalance.Items.Add(new ListViewItem(new string[3] { " Retained Earnings", "", dAssets.ToString("C") }));
            lvBalance.Items.Add("");
            lvBalance.Items.Add(new ListViewItem(new string[4] { "Total Liabilities And Equity", "", "", dAssets.ToString("C") }));


            //income statement
            //we'll do a separate loop to keep it clean and easier to follow
            lvIncome.Items.Clear();
            lvIncome.Items.Add("Income Statement");
            lvIncome.Items.Add("For the term " + ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].strName);
            lvIncome.Items.Add("");
            //membership and other fees
            double dDiscounts = 0, dUncollected = 0, dLimbo = 0;
            double[] dOther = new double[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numOtherFees];
            //cost of membership
            double dButtons = 0, dRentalSub = 0, dWMF = 0;

            //prepare for 10 sources of revenue
            double[] dRev = new double[10];
            string[] strRev = new string[10];
            strRev[0] = "Membership Fee";
            //prepare for 30 sources of expenses
            double[] dExp = new double[30];
            string[] strExp = new string[30];
            //Automate handling of most items
            //Membership fees are special (have to track discounts, waives, uncollected, etc.)
            //Waives are a pain

            //membership fees
            for (int i = 0; i < ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numMembers;i++)
            {
                //not a limbo member and paid nothing
                if (!ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].limboMembers[i] &&
                    ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaid[i, 0] == 0) //uncollected fee
                    dUncollected += ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].membershipFees;
                //discounts
                //we will add membership fee waives later
                else if (!ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].limboMembers[i] &&
                    ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaid[i, 0] < ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].membershipFees)
                    dDiscounts += ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].membershipFees - ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaid[i, 0];
                //add limbo members who paid membership fees
                else if (ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].limboMembers[i])
                    dLimbo += ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaid[i, 0];
                //add up other revenue sources
                for (int j = 0; j < ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numOtherFees; j++)
                    dOther[j] += ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaid[i, j + 1];
            }
            foreach (BudgetItem item in ClsStorage.currentClub.budget)
            {              
                //income statement, so ignore anything not affecting term
                if (item.term == cbTerm.SelectedIndex)
                {
                    if (item.type == TransactionType.Revenue)
                    {
                        int iIndex = iRevenueSearch(0, 9, item.cat, strRev, true);
                        if (iIndex == -1) //revenue category does not exist
                        {
                            //find the spot where it SHOULD be
                            iIndex = iRevenueSearch(0, 9, item.cat, strRev, false);
                            //move everything else in the array over
                            for (int j = 9; j > iIndex; j--)
                            {
                                strRev[j] = strRev[j - 1];
                                dRev[j] = dRev[j - 1];
                            }
                            strRev[iIndex] = item.cat;
                            dRev[iIndex] = item.value;
                        }
                        else //revenue category already exists
                            dRev[iIndex] += item.value;
                    }
                    else if (item.type != TransactionType.Asset)
                    {
                        if (item.cat == "Waived Membership Fee")
                        {
                            //this is a tricky case
                            //we never actually collected a waived membership fee
                            //BUT, it was recorded in revenue as being recorded
                            //so we remove it here
                            dDiscounts += item.value;
                        }
                        //copied from revenue method
                        int iIndex = iRevenueSearch(0, 29, item.cat, strExp, true);
                        if (iIndex == -1) //expense category does not exist
                        {
                            //find the spot where it SHOULD be
                            iIndex = iRevenueSearch(0, 29, item.cat, strExp, false);
                            //move everything else in the array over
                            for (int j = 29; j > iIndex; j--)
                            {
                                strExp[j] = strExp[j - 1];
                                dExp[j] = dExp[j - 1];
                            }
                            strExp[iIndex] = item.cat;
                            dExp[iIndex] = item.value;
                        }
                        else //revenue category already exists
                            dExp[iIndex] += item.value;
                    }
                }
            }
            //now write it all up
            //I have commented out the old method of calculating membership fees
            //I think this method is better
            //NOTE: The cash flow method of calculating all of this should eventally be implemented

            double dInflow = 0, dOutflow = 0;
            for (int i = 0; i < 10; i++)
                dInflow += dRev[i];
            for (int i = 0; i < 30; i++)
                dOutflow += dExp[i];

            for (int i = 0; i < 10; i++)
            {
                if (strRev[i] == null || strRev[i] == "")
                    break;
                else if(strRev[i] == "Membership Fee")
                    lvIncome.Items.Add(new ListViewItem(new string[2] {"Membership Fee",
                (dRev[i]+dUncollected+dDiscounts).ToString("C")}));
                else
                    lvIncome.Items.Add(new ListViewItem(new string[2] { strRev[i], dRev[i].ToString("C") }));
            }
            lvIncome.Items.Add(new ListViewItem(new string[3] {"Gross Revenue","",
                (dInflow + dDiscounts + dUncollected).ToString("C")}));
            lvIncome.Items.Add(new ListViewItem(new string[3] { " Less: Discounts", "", dDiscounts.ToString("C") }));
            lvIncome.Items.Add(new ListViewItem(new string[3] { " Less: Uncollected Membership Fees", "", dUncollected.ToString("C") }));
            lvIncome.Items.Add(new ListViewItem(new string[4] { "Net Revenue", "", "", dInflow.ToString("C") }));

            //Cost of Membership is items that are directly proportional to members or are direct costs associated with a membership
            lvIncome.Items.Add("Cost of Membership");
            if (iRevenueSearch(0, 29, "Buttons", strExp, true) != -1)
                dButtons = dExp[iRevenueSearch(0, 29, "Buttons", strExp, true)];
            if (iRevenueSearch(0, 29, "Rental Subsidy", strExp, true) != -1)
                dRentalSub = dExp[iRevenueSearch(0, 29, "Rental Subsidy", strExp, true)];
            if (iRevenueSearch(0, 29, "Waived Membership Fee", strExp, true) != -1)
                dWMF = dExp[iRevenueSearch(0, 29, "Waived Membership Fee", strExp, true)];
            dOutflow -= dWMF;
            lvIncome.Items.Add(new ListViewItem(new string[3] { " Buttons", "", dButtons.ToString("C") }));
            lvIncome.Items.Add(new ListViewItem(new string[3] { " Rental Subsidies", "", dRentalSub.ToString("C") }));
            lvIncome.Items.Add(new ListViewItem(new string[4] { "Cost of Membership", "", "", (dButtons + dRentalSub).ToString("C") }));
            lvIncome.Items.Add(new ListViewItem(new string[4] { "Gross Profit", "", "", (dInflow - dButtons - dRentalSub).ToString("C") }));
            lvIncome.Items.Add("");
            lvIncome.Items.Add("Operating Expenses");
            for (int i = 0; i < 30; i++)
            {
                if (strExp[i] == null || strExp[i] == "")
                    break;
                else if(strExp[i] != "Buttons" && strExp[i] != "Rental Subsidy" && strExp[i] != "Waived Membership Fee")
                    lvIncome.Items.Add(new ListViewItem(new string[3] { " " + strExp[i], "", dExp[i].ToString("C") }));
            }
            lvIncome.Items.Add(new ListViewItem(new string[4] { "Total Operating Expenses", "", "", (dOutflow-dButtons-dRentalSub).ToString("C")}));
            lvIncome.Items.Add("");
            lvIncome.Items.Add(new ListViewItem(new string[4] { "Net Income", "", "", (dInflow-dOutflow).ToString("C") }));
            lvIncome.Items.Add("");
            lvIncome.Items.Add(new ListViewItem(new string[4] { "Number of Members", "", "", Convert.ToString(ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numMembers
                - ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].iLimbo) }));
            lvIncome.Items.Add(new ListViewItem(new string[4] { "Profit per Member", "", "", ((dInflow-dOutflow)/
                (ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numMembers - ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].iLimbo)).ToString("C") }));

            //cash flow statement
            //cash is of course different from revenue
            //here we will be using dates as a measure as opposed to the term
            double dBegCash = 0;
            //we will reuse many of the previous variables

            //prepare for 10 sources of revenue
            dRev = new double[10];
            strRev = new string[10];
            strRev[0] = "Membership Fee";
            //prepare for 30 sources of expenses
            dExp = new double[30];
            strExp = new string[30];

            //dValue is a temporary variable to help calculate cash flow for an individual item
            double dValue = 0;
            foreach (BudgetItem item in ClsStorage.currentClub.budget)
            {
                //if occured before the start of the term
                if (item.dateAccount <
                    ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].startDate)
                {
                    if (item.type == TransactionType.Revenue)
                        dBegCash += item.value;
                    else if (item.type == TransactionType.Asset ||
                        item.type == TransactionType.Expense)
                        dBegCash -= item.value;
                }
                //occured in this term
                else if (item.dateAccount < ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].endDate.AddDays(1))
                {
                    dValue = item.value;
                    //ignore depreciation
                    if (item.type == TransactionType.Depreciation)
                        dValue = 0;
                    //note to self: this section could be improved with arrays
                    //I decided against arrays here because the categories aren't yet final
                    //And the types for each category are also not yet final
                    //And it would cause a lot of headache if I had to add a category later and did not use this less efficient method

                    else if (item.type == TransactionType.Expense || item.type == TransactionType.Asset)
                    {
                        if (item.cat == "Waived Membership Fee")
                        {
                            //this is a tricky case
                            //we never actually collected a waived membership fee
                            //BUT, it was recorded in revenue as being recorded
                            //so we remove it here
                            dRev[iRevenueSearch(0, 9, "Membership Fee", strRev, true)] -= item.value;
                        }
                        else
                        {
                            //copied from revenue method
                            int iIndex = iRevenueSearch(0, 29, item.cat, strExp, true);
                            if (iIndex == -1) //revenue category does not exist
                            {
                                //find the spot where it SHOULD be
                                iIndex = iRevenueSearch(0, 29, item.cat, strExp, false);
                                //move everything else in the array over
                                for (int j = 29; j > iIndex; j--)
                                {
                                    strExp[j] = strExp[j - 1];
                                    dExp[j] = dExp[j - 1];
                                }
                                strExp[iIndex] = item.cat;
                                dExp[iIndex] = item.value;
                            }
                            else //revenue category already exists
                                dExp[iIndex] += item.value;
                        }
                    }
                    else //revenue
                    {
                        //see if revenue category already exists
                        //NOTE TO SELF: this section is a model for how other sections could be modified to be
                        //more flexible
                        int iIndex = iRevenueSearch(0, 9, item.cat, strRev, true);
                        if (iIndex == -1) //revenue category does not exist
                        {
                            //find the spot where it SHOULD be
                            iIndex = iRevenueSearch(0, 9, item.cat, strRev, false);
                            //move everything else in the array over
                            for (int j = 9; j > iIndex; j--)
                            {
                                strRev[j] = strRev[j - 1];
                                dRev[j] = dRev[j - 1];
                            }
                            strRev[iIndex] = item.cat;
                            dRev[iIndex] = item.value;
                        }
                        else //revenue category already exists
                            dRev[iIndex] += item.value;
                    }
                }

            }
            dInflow = 0;
            dOutflow = 0;
            for (int i = 0; i < 10; i++)
                dInflow += dRev[i];
            for (int i = 0; i < 30; i++)
                dOutflow += dExp[i];
            //put it all together
            lvCash.Items.Clear();
            lvCash.Items.Add("Cash Flow Statement");
            lvCash.Items.Add("For the term " + ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].strName);
            lvCash.Items.Add("");
            lvCash.Items.Add("Cash Inflow");
            for (int i = 0; i < 10; i++)
            {
                if (strRev[i] == null || strRev[i] == "")
                    break;
                else
                    lvCash.Items.Add(new ListViewItem(new string[3] { " " + strRev[i], "", dRev[i].ToString("C") }));
            }
            lvCash.Items.Add(new ListViewItem(new string[4] { "Total Cash Inflows", "", "", dInflow.ToString("C") }));
            lvCash.Items.Add("");
            lvCash.Items.Add("Cash Outflow");
            for (int i = 0; i < 30; i++)
            {
                if (strExp[i] == null || strExp[i] == "")
                    break;
                else
                    lvCash.Items.Add(new ListViewItem(new string[3] { " " + strExp[i], "", dExp[i].ToString("C") }));
            }
            lvCash.Items.Add(new ListViewItem(new string[4] { "Total Cash Outflow", "", "", dOutflow.ToString("C") }));
            lvCash.Items.Add("");
            lvCash.Items.Add(new ListViewItem(new string[4] { "Net Cash Flow", "", "", (dInflow - dOutflow).ToString("C") }));
            lvCash.Items.Add("");
            lvCash.Items.Add(new ListViewItem(new string[4] { "Beginning Cash Balance", "", "", dBegCash.ToString("C") }));
            lvCash.Items.Add(new ListViewItem(new string[4] { "Net Cash Flow", "", "", (dInflow - dOutflow).ToString("C") }));
            lvCash.Items.Add(new ListViewItem(new string[4] { "Ending Cash Balance", "", "", (dBegCash + dInflow - dOutflow).ToString("C") }));
        }

        int iRevenueSearch(int low, int high, string search, string[] strSource, bool bFind)
        {
            if (low >= high && String.Compare(search, strSource[low]) != 0) //didn't find it
            {
                if (bFind)
                    return -1;
                else if (strSource[low] == null)
                    return low;
                else
                    //this is just a fancy way of adding 1 if needed
                    return low + (String.Compare(search, strSource[low]) + 1) / 2;
            }
            else
            {
                int mid = (low + high) / 2;
                if (strSource[mid] == null || String.Compare(search, strSource[mid]) < 0)
                    return iRevenueSearch(low, mid - 1, search, strSource, bFind);
                else if (String.Compare(search, strSource[mid]) > 0)
                    return iRevenueSearch(mid + 1, high, search, strSource, bFind);
                else //found it!
                    return mid;
            }
        }

        private void termFinancials_Load(object sender, EventArgs e)
        {
            cbTerm.Items.AddRange(ClsStorage.currentClub.GetTermNames());
            //if we default to selecting current term, do so!
            if (Properties.Settings.Default.selectCurrentTerm)
                cbTerm.SelectedIndex = ClsStorage.currentClub.listTerms.Count - 1;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            int iTerm = cbTerm.SelectedIndex;
            if (iTerm != -1 && svdSave.ShowDialog() == DialogResult.OK)
            {
                //1 = excel file
                if (svdSave.FilterIndex == 1)
                {
                    //balance sheet export
                    if (tcMain.SelectedIndex == 0)
                        writeFinancialStatement(lvBalance, svdSave.FileName);
                    //income statement export
                    else if (tcMain.SelectedIndex == 1)
                        writeFinancialStatement(lvIncome, svdSave.FileName);
                    //cash flow statement export
                    else
                        writeFinancialStatement(lvCash, svdSave.FileName);
                }
                //2 = csv file
                else if (svdSave.FilterIndex == 2)
                {
                    using (CsvFileWriter writer = new CsvFileWriter(svdSave.FileName))
                    {
                        //balance sheet export
                        if (tcMain.SelectedIndex == 0)
                            writeFinancialStatement(lvBalance, writer);
                        //income statement export
                        else if (tcMain.SelectedIndex == 1)
                            writeFinancialStatement(lvIncome, writer);
                        //cash flow statement export
                        else
                            writeFinancialStatement(lvCash, writer);
                        if (Properties.Settings.Default.playSounds)
                            Sound.Success.Play();
                    }
                }
            }
        }

        private void writeFinancialStatement(ListView lv, CsvFileWriter writer)
        {
            int length = lv.Items.Count;
            for (int i = 0; i < length; i++)
            {
                CsvRow row = new CsvRow();
                int width = lv.Items[i].SubItems.Count;
                for (int j = 0; j < width; j++)
                    row.Add(lv.Items[i].SubItems[j].Text);
                writer.WriteRow(row);
            }
        }

        private void writeFinancialStatement(ListView lv, string strLocation)
        {
            int length = lv.Items.Count;
            //none of them exceed 6 in length, so this is fine
            object[,] output = new object[length, 6];
            double curValue;
            for (int i = 0; i < length; i++)
            {
                int width = lv.Items[i].SubItems.Count;
                for (int j = 0; j < width; j++)
                {
                    if (double.TryParse(lv.Items[i].SubItems[j].Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out curValue))
                        output[i, j] = curValue;
                    else
                        output[i, j] = lv.Items[i].SubItems[j].Text;
                }
            }
            ExcelFile.saveFinancialStatement(output, strLocation, true);
        }
    }
}
