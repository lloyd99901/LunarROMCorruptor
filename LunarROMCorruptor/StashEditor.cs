﻿using System;
using System.IO;
using System.Windows.Forms;

namespace LunarROMCorruptor
{
    public partial class StashEditor : Form
    {
        public StashEditor()
        {
            InitializeComponent();
        }

        public void AttemptStashLoad(string fileloc)
        {
            try
            {
                stashListbox.Items.Clear();
                foreach (var line in File.ReadLines(fileloc))
                {
                    stashListbox.Items.Add(line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Openfilebtn_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                AttemptStashLoad(OpenFileDialog1.FileName);
            }
        }

        private void removeselbtn_Click(object sender, EventArgs e)
        {
            foreach (var item in stashListbox.SelectedItems)
            {
                RemovedItemslstbx.Items.Add(item);
            }
            while (stashListbox.SelectedItems.Count > 0)
            {
                stashListbox.Items.Remove(stashListbox.SelectedItems[0]);
            }
        }

        private void remove50btn_Click(object sender, EventArgs e)
        {
            try
            {
                int half = stashListbox.Items.Count / 2;
                for (int i = 0; i <= half; i++)
                {
                    RemovedItemslstbx.Items.Add(stashListbox.Items[i]);
                    stashListbox.Items.Remove(stashListbox.Items[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            stashListbox.Items.Add("L: FILE(" + LocationStash.Value + ")." + stashitemcmbx.Text + "(" + valueStashnum.Value + ")");
        }

        private void additemsbtn_Click(object sender, EventArgs e)
        {
            foreach (var item in RemovedItemslstbx.SelectedItems)
                stashListbox.Items.Add(item);
            for (int i = 0; i <= RemovedItemslstbx.SelectedItems.Count - 1; i++)
                RemovedItemslstbx.Items.RemoveAt(RemovedItemslstbx.SelectedIndex);
        }

        private void removeitembtn_Click(object sender, EventArgs e)
        {
            while (RemovedItemslstbx.SelectedItems.Count > 0)
            {
                RemovedItemslstbx.Items.Remove(RemovedItemslstbx.SelectedItems[0]);
            }
        }

        private void newfilestashbtn_Click(object sender, EventArgs e)
        {
        }

        private void StashEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}