using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marimba
{
    /// <summary>
    /// Allows other Forms to open a new window to allow users to select multiple terms.
    /// This class is technically named incorrectly; it should be SelectTermsForm.
    /// Once the user hits submit, ClsStorage's selected terms field will be populated with
    /// the results of this selection.
    /// If the user cancels, ClsStorage's selected terms field will be cleared.
    /// </summary>
    public partial class SelectTermForm : Form
    {
        public SelectTermForm()
        {
            InitializeComponent();
        }

        private void SelectTermForm_Load(object sender, EventArgs e)
        {
            // populate the terms list
            lbTerms.Items.Clear();
            string[] termNames = ClsStorage.currentClub.GetTermNames();
            lbTerms.Items.AddRange(termNames);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lbTerms.SelectedIndices.Count <= 0)
            {
                MessageBox.Show("Please select some terms.", "No terms selected.");
                return;
            }

            List<Term> terms = new List<Term>();
            // add the selected terms to the list of selected terms in ClsStorage
            foreach (int index in lbTerms.SelectedIndices) {
                Term currentTerm = ClsStorage.currentClub.listTerms.ElementAt(index);
                terms.Add(currentTerm);
            }

            ClsStorage.setSelectedTerms(terms);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClsStorage.setSelectedTerms(new List<Term>());
            Close();
        }
    }
}
