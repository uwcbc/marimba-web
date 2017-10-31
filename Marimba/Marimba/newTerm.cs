﻿namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using Marimba.Utility;

    public partial class newTerm : Form
    {
        public newTerm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == String.Empty)
                {
                    if (Properties.Settings.Default.playSounds)
                        Sound.Error.Play();
                    MessageBox.Show("Please enter a name for the term.");
                }
                else if (mcTermStart.SelectionStart.CompareTo(mcRehearsalStart.SelectionStart) > 0 ||
                    mcTermEnd.SelectionStart.CompareTo(mcRehearsalStart.SelectionStart.AddDays(7 * Convert.ToInt32(updNumRehearsals.Value))) < 0)
                {
                    if (Properties.Settings.Default.playSounds)
                        Sound.Error.Play();
                    MessageBox.Show("Adding term failed. Not all rehearsals are in the term.");
                }
                else
                {

                    // create array of other fees owed (or possibly owed)
                    string[] otherfeesnames;
                    double[] otherfeesvalue;
                    // use this to track if we were successful in creating a term
                    bool success = false;
                    // create array of rehearsals
                    DateTime[] rehearsals = new DateTime[Convert.ToInt32(updNumRehearsals.Value)];
                    for (int i = 0; i < Convert.ToInt32(updNumRehearsals.Value); i++)
                        rehearsals[i] = mcRehearsalStart.SelectionStart.Date.AddDays(7 * i);
                    if (txtOther.Text != String.Empty)
                    {
                        string[] otherfees = txtOther.Text.Split('\n');
                        int iNumFees = otherfees.Length;
                        otherfeesnames = new string[iNumFees];
                        otherfeesvalue = new double[iNumFees];
                        for (int i = 0; i < iNumFees; i++)
                        {
                            otherfeesnames[i] = otherfees[i].Split(',')[0].Trim();
                            otherfeesvalue[i] = Convert.ToDouble(otherfees[i].Split(',')[1].Trim());
                        }
                        success = ClsStorage.currentClub.AddTerm(
                            txtName.Text,
                            Convert.ToInt16(ClsStorage.currentClub.listTerms.Count + 1),
                            Convert.ToInt16(updNumRehearsals.Value),
                            mcTermStart.SelectionStart.Date,
                            mcTermEnd.SelectionStart.Date,
                            rehearsals,
                            Convert.ToDouble(txtMembershipFee.Text),
                            otherfeesvalue,
                            otherfeesnames);
                    }
                    else
                    {
                        // no other fees
                        success = ClsStorage.currentClub.AddTerm(
                            txtName.Text,
                            Convert.ToInt16(ClsStorage.currentClub.listTerms.Count + 1),
                            Convert.ToInt16(updNumRehearsals.Value),
                            mcTermStart.SelectionStart.Date,
                            mcTermEnd.SelectionStart.Date,
                            rehearsals,
                            Convert.ToDouble(txtMembershipFee.Text));
                    }

                    if (success)
                    {
                        if (Properties.Settings.Default.playSounds)
                            Sound.Success.Play();
                        MessageBox.Show("Term added successfully!");
                        ClsStorage.currentClub.AddHistory(txtName.Text, ChangeType.NewTerm);
                        this.Close();
                    }
                }
            }
            catch
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Error.Play();
                MessageBox.Show("Adding term failed. Please verify the input provided is valid.");
            }
        }
    }
}
