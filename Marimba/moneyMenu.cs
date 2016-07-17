using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Marimba
{
    public partial class moneyMenu : Form
    {
        public addFees newfees = new addFees();
        public moneyMenu()
        {
            InitializeComponent();
        }

        public void btnBudget_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
            Form accountsummary = new budgetView();
            accountsummary.ShowDialog();
        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
            Form additem = new addBudgetItem(-1);
            additem.ShowDialog();
        }

        public void btnFees_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
            
            newfees.ShowDialog();
        }

        public void btnExport_Click(object sender, EventArgs e)
        {
            //export the atomized budget
            //I'll do more summarized budgets later
            if (svdSave.ShowDialog() == DialogResult.OK)
            {
                //1 = excel file
                if (svdSave.FilterIndex == 1)
                {
                    //first, set up string array to be sent to Excel
                    object[,] output = new object[1 + clsStorage.currentClub.iMember, 9];
                    output[0, 0] = "Term";
                    output[0, 1] = "Description";
                    output[0, 2] = "Debit";
                    output[0, 3] = "Credit";
                    output[0, 4] = "Category";
                    output[0, 5] = "Type";
                    output[0, 6] = "Date of Occurrence";
                    output[0, 7] = "Date in Account";
                    output[0, 8] = "Comments";

                    int i = 0;
                    foreach (budgetItem item in clsStorage.currentClub.budget)
                    {
                        output[i + 1, 0] = clsStorage.currentClub.listTerms[item.term].strName;
                        output[i + 1, 1] = item.name;
                        if (item.type == (int)club.money.Revenue)
                            output[i + 1, 3] = item.value;
                        else
                            output[i + 1, 2] = item.value;
                        output[i + 1, 4] = item.cat;
                        output[i + 1, 5] = Convert.ToString((club.money)item.type);
                        output[i + 1, 6] = item.dateOccur;
                        output[i + 1, 7] = item.dateAccount;
                        output[i + 1, 8] = item.comment;
                        i++;
                    }
                    excelFile.saveExcel(output, svdSave.FileName, "dd/MM/yy");
                }
                //2 = CSV file
                else if (svdSave.FilterIndex == 2)
                {
                    using (CsvFileWriter writer = new CsvFileWriter(svdSave.FileName))
                    {
                        CsvRow firstrow = new CsvRow();
                        firstrow.Add("Term");
                        firstrow.Add("Description");
                        firstrow.Add("Debit");
                        firstrow.Add("Credit");
                        firstrow.Add("Category");
                        firstrow.Add("Type");
                        firstrow.Add("Date of Occurrence");
                        firstrow.Add("Date in Account");
                        firstrow.Add("Comments");
                        writer.WriteRow(firstrow);
                        foreach (budgetItem item in clsStorage.currentClub.budget)
                        {
                            CsvRow row = new CsvRow();
                            row.Add(clsStorage.currentClub.listTerms[item.term].strName);
                            row.Add(item.name);
                            if (item.type == (int)club.money.Revenue)
                            {
                                row.Add("");
                                row.Add(String.Format("${0}", item.value));
                            }
                            else
                            {
                                row.Add(String.Format("${0}", item.value));
                                row.Add("");
                            }
                            row.Add(item.cat);
                            row.Add(Convert.ToString((club.money)item.type));
                            row.Add(item.dateOccur.ToShortDateString());
                            row.Add(item.dateAccount.ToShortDateString());
                            row.Add(item.comment);
                            writer.WriteRow(row);
                        }
                    }
                    if (Properties.Settings.Default.playSounds)
                        sound.success.Play();
                }
            }
        }

        public void btnTermSummary_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
            Form termSummary = new termFinancials();
            termSummary.ShowDialog();
        }
    }
}
