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
    public partial class viewHistory : Form
    {
        public viewHistory()
        {
            InitializeComponent();
        }

        private void viewHistory_Load(object sender, EventArgs e)
        {
            List<ListViewItem> historyList = new List<ListViewItem>();
            lvMain.Items.Clear();
            //show the last twenty changes
            for (int i = clsStorage.currentClub.historyList.Count - 1; i >= 0; i--)
                historyList.Add(new ListViewItem(new string[2]{clsStorage.currentClub.historyList[i].toString(),
                    clsStorage.currentClub.historyList[i].time.ToString()}));
            lvMain.Items.AddRange(historyList.ToArray());
        }
    }
}
