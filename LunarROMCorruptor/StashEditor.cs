﻿using LunarROMCorruptor.Properties;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LunarROMCorruptor
{
    public partial class StashEditor : Form
    {
        private string LoadedStashLocation;
        public StashEditor()
        {
            InitializeComponent();
        }
        public void AttemptStashLoad(string fileloc)
        {
            try
            {
                stashListbox.Items.Clear();
                //Check if the file being loaded contains only text
                if (File.ReadAllBytes(fileloc).All(b => b < 128))
                {
                    //If it does, load it as a text file
                    foreach (var line in File.ReadLines(fileloc))
                    {
                        //If the line doesn't start with "[x] File" it's not a valid line
                        if (!line.StartsWith("[x] File"))
                        {
                            MessageBox.Show("This file is not a valid stash file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else //passed checks
                        {
                            stashListbox.Items.Add(line);
                            LoadedStashLocation = fileloc;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This file is not a valid stash file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Openfilebtn_Click(object sender, EventArgs e)
        {
            //Check if CorruptionStashList folder exists
            if (!Directory.Exists(@"CorruptionStashList"))
            {
                //If it doesn't, create it
                Directory.CreateDirectory(@"CorruptionStashList");
            }
            OpenFileDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath + "/CorruptionStashList/");
            if (OpenFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                AttemptStashLoad(OpenFileDialog.FileName);
            }
        }

        private void Removeselbtn_Click(object sender, EventArgs e)
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

        private void Remove50btn_Click(object sender, EventArgs e)
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

        private void Addbtn_Click(object sender, EventArgs e)
        {
            stashListbox.Items.Add("[x] File(" + LocationStash.Value + ").SET(" + valueStashnum.Value + ")");
        }

        private void Additemsbtn_Click(object sender, EventArgs e)
        {
            //Add each selected item from RemovedItemslstbx to the stashlistbox items, once added, remove the items that are selected from RemovedItemslstbx
            foreach (var item in RemovedItemslstbx.SelectedItems)
            {
                stashListbox.Items.Add(item);
            }
            while (RemovedItemslstbx.SelectedItems.Count > 0)
            {
                RemovedItemslstbx.Items.Remove(RemovedItemslstbx.SelectedItems[0]);
            }
            //foreach (var item in RemovedItemslstbx.SelectedItems)
            //    stashListbox.Items.Add(item);
            //for (int i = 0; i <= RemovedItemslstbx.SelectedItems.Count - 1; i++)
            //    RemovedItemslstbx.Items.RemoveAt(RemovedItemslstbx.SelectedIndex);
        }

        private void Removeitembtn_Click(object sender, EventArgs e)
        {
            while (RemovedItemslstbx.SelectedItems.Count > 0)
            {
                RemovedItemslstbx.Items.Remove(RemovedItemslstbx.SelectedItems[0]);
            }
        }

        private void Newfilestashbtn_Click(object sender, EventArgs e)
        {
            SaveNewStashFile();
        }
        private string SaveNewStashFile()
        {
            Program.Form.StashFileList.Items.Clear();
            StringBuilder builder = new StringBuilder();
            foreach (var listitem in stashListbox.Items)
            {
                builder.Append(listitem);
                builder.AppendLine();
            }

            //var fileCount = Directory.GetFiles(Application.StartupPath + @"\CorruptionStashList\", "*.*", SearchOption.TopDirectoryOnly).Length;
            //fileCount += 1
            var rnd = new Random().Next(1000, 999999999);
            File.WriteAllText(Application.StartupPath + @"\CorruptionStashList\" + rnd + ".stash", builder.ToString());
            Program.Form.RefreshCorruptionStashList();
            Console.WriteLine(Application.StartupPath + @"\CorruptionStashList\" + rnd + ".stash" + " - Saved to a brand new stash file.");
            return Application.StartupPath + @"\CorruptionStashList\" + rnd + ".stash";
        }
        private void SaveToExistingStashFile()
        {
            //Check if a stash file location is loaded first.
            if (LoadedStashLocation == null)
            {
                MessageBox.Show("No stash file is loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Check if corruption queue is enabled on the main form, if it is cancel the operation.
            if (Program.Form.CorruptionQueueChkbox.Checked)
            {
                MessageBox.Show("Corruption queue is enabled. Please disable it and select a single file before corrupting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Program.Form.StashFileList.Items.Clear();
            StringBuilder builder = new StringBuilder();
            foreach (var listitem in stashListbox.Items)
            {
                builder.Append(listitem);
                builder.AppendLine();
            }
            File.WriteAllText(LoadedStashLocation, builder.ToString());
            Program.Form.RefreshCorruptionStashList();
            Console.WriteLine(LoadedStashLocation + " - Saved to existing stash file.");
        }
        private void StashEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Restorebtn_Click(object sender, EventArgs e)
        {
            stashListbox.Items.Clear();
            try
            {
                foreach (var line in File.ReadLines(LoadedStashLocation))
                {
                    stashListbox.Items.Add(line);
                }
            }
            catch
            {
                MessageBox.Show("Can't restore stash items.", $"Info - {nameof(LunarROMCorruptor)} ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RemoveAllbtn_Click(object sender, EventArgs e)
        {
            //Remove ALL items from the stashlistbox
            stashListbox.Items.Clear();
        }

        private void SaveAndCorruptbtn_Click(object sender, EventArgs e)
        {
            //Run a void function on Form1 that is called CorruptUsingStashFile
            SaveToExistingStashFile();
            if (LoadedStashLocation == null || Program.Form.CorruptionQueueChkbox.Checked)
            {
                return;
            }
            Program.Form.CorruptUsingStashFile(LoadedStashLocation);
        }
    }
}