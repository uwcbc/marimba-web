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
    public partial class editMultiInstruments : Form
    {
        int index;
        public editMultiInstruments(int iMemberIndex)
        {
            index = iMemberIndex;
            InitializeComponent();
        }

        private void editMultiInstruments_Load(object sender, EventArgs e)
        {
            //add image lists
            lvPlays.SmallImageList = Program.home.instrumentSmall;
            lvAvailable.SmallImageList = Program.home.instrumentSmall;

            //add the instruments to the listviews
            lvPlays.BeginUpdate();
            lvAvailable.BeginUpdate();
            List<ListViewItem> doesNotPlay = new List<ListViewItem>();
            List<ListViewItem> doesPlay = new List<ListViewItem>();

            //go through the instrument
            //if the member plays the instrument, add it

            if(clsStorage.currentClub.members[index].bMultipleInstruments)
            {
                foreach (Member.Instrument instrument in Enum.GetValues(typeof(Member.Instrument)))
                {
                     if(clsStorage.currentClub.members[index].playsInstrument[(int)instrument])
                         doesPlay.Add(new ListViewItem(Member.instrumentToString(instrument),Member.instrumentIconIndex(instrument)));
                     else
                         doesNotPlay.Add(new ListViewItem(Member.instrumentToString(instrument),Member.instrumentIconIndex(instrument)));
                }                
            }
            //if the member currently does not play multiple instruments, then every instrument (except their current one) is available
            else
                foreach (Member.Instrument instrument in Enum.GetValues(typeof(Member.Instrument)))
                {
                    if(clsStorage.currentClub.members[index].curInstrument==instrument)
                        doesPlay.Add(new ListViewItem(Member.instrumentToString(instrument), Member.instrumentIconIndex(instrument)));
                    else
                        doesNotPlay.Add(new ListViewItem(Member.instrumentToString(instrument), Member.instrumentIconIndex(instrument)));
                }
            lvAvailable.Items.AddRange(doesNotPlay.ToArray());
            lvPlays.Items.AddRange(doesPlay.ToArray());
            lvAvailable.Sort();
            lvPlays.Sort();

            lvAvailable.EndUpdate();
            lvPlays.EndUpdate();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            lvAvailable.BeginUpdate();
            lvPlays.BeginUpdate();
            foreach(ListViewItem newInstrument in lvAvailable.SelectedItems)
            {
                lvAvailable.Items.Remove(newInstrument);
                lvPlays.Items.Add(newInstrument);                
            }
            lvPlays.EndUpdate();
            lvAvailable.EndUpdate();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lvAvailable.BeginUpdate();
            lvPlays.BeginUpdate();
            foreach (ListViewItem newInstrument in lvPlays.SelectedItems)
            {
                //user can't remove the instrument they play
                if (Member.stringToInstrument(newInstrument.Text) != clsStorage.currentClub.members[index].curInstrument)
                {
                    lvPlays.Items.Remove(newInstrument);
                    lvAvailable.Items.Add(newInstrument);                    
                }
            }
            lvPlays.EndUpdate();
            lvAvailable.EndUpdate();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //if we only play one instrument, we sure aren't saving space for 24!
            if(lvPlays.Items.Count == 1)
            {
                clsStorage.currentClub.members[index].bMultipleInstruments = false;
                clsStorage.currentClub.members[index].playsInstrument = null;
            }
            else
            {
                clsStorage.currentClub.members[index].bMultipleInstruments = true;
                //create new array for storing information
                clsStorage.currentClub.members[index].playsInstrument = new bool[Enum.GetValues(typeof(Member.Instrument)).Length];

                //marked each played instrument as played
                foreach(ListViewItem playsIt in lvPlays.Items)
                    clsStorage.currentClub.members[index].playsInstrument[(int)Member.stringToInstrument(playsIt.Text)] = true;
            }
            this.Close();
        }

        private void lvAvailable_ItemDrag(object sender, ItemDragEventArgs e)
        {
            lvAvailable.DoDragDrop(lvAvailable.SelectedItems, DragDropEffects.Move);
        }

        private void lvAvailable_DragEnter(object sender, DragEventArgs e)
        {
            int len = e.Data.GetFormats().Length - 1;
            int i;
            for (i = 0; i <= len; i++)
            {
                if (e.Data.GetFormats()[i].Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection"))
                {
                    //The data from the drag source is moved to the target.	
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void lvPlays_DragDrop(object sender, DragEventArgs e)
        {
            //Return if the items are not selected in the ListView control.
            if (lvAvailable.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem[] sel = new ListViewItem[lvAvailable.SelectedItems.Count];
            for (int i = 0; i <= lvAvailable.SelectedItems.Count - 1; i++)
            {
                sel[i] = lvAvailable.SelectedItems[i];
            }
            for (int i = 0; i < sel.GetLength(0); i++)
            {
                //Obtain the ListViewItem to be dragged to the target location.
                ListViewItem dragItem = sel[i];
                ListViewItem insertItem = (ListViewItem)dragItem.Clone();
                lvPlays.Items.Add(insertItem);
                //Removes the item from the initial location while 
                //the item is moved to the new location.
                lvAvailable.Items.Remove(dragItem);
            }
        }

        private void lvAvailable_DragDrop(object sender, DragEventArgs e)
        {
            //Return if the items are not selected in the ListView control.
            if (lvPlays.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem[] sel = new ListViewItem[lvPlays.SelectedItems.Count];
            for (int i = 0; i <= lvPlays.SelectedItems.Count - 1; i++)
            {
                sel[i] = lvPlays.SelectedItems[i];
            }
            for (int i = 0; i < sel.GetLength(0); i++)
            {
                //Obtain the ListViewItem to be dragged to the target location.
                ListViewItem dragItem = sel[i];
                if (Member.stringToInstrument(dragItem.Text) != clsStorage.currentClub.members[index].curInstrument)
                {
                    ListViewItem insertItem = (ListViewItem)dragItem.Clone();
                    lvAvailable.Items.Add(insertItem);
                    //Removes the item from the initial location while 
                    //the item is moved to the new location.
                    lvPlays.Items.Remove(dragItem);
                }
            }
        }

        private void lvPlays_DragEnter(object sender, DragEventArgs e)
        {
            int len = e.Data.GetFormats().Length - 1;
            int i;
            for (i = 0; i <= len; i++)
            {
                if (e.Data.GetFormats()[i].Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection"))
                {
                    //The data from the drag source is moved to the target.	
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void lvPlays_ItemDrag(object sender, ItemDragEventArgs e)
        {
            lvPlays.DoDragDrop(lvAvailable.SelectedItems, DragDropEffects.Move);
        }
    }
}
