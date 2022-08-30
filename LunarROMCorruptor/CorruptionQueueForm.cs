﻿using System;
using System.IO;
using System.Windows.Forms;

namespace LunarROMCorruptor
{
    public partial class CorruptionQueueForm : Form
    {
        public CorruptionQueueForm()
        {
            InitializeComponent();
        }

        private void Openfilebtn_Click(object sender, EventArgs e)
        {
            //Main Function - If the user didn't cancel, load file.
            if (OpenFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                //add file to list
                CorruptionQueueList.Items.Add(OpenFileDialog.FileName);
            }
        }
        private void SendFilestoCorruptorBTN_Click(object sender, EventArgs e)
        {
            //Check if there are no items in the list
            if (CorruptionQueueList.Items.Count == 0)
            {
                MessageBox.Show("No files selected.", "No Files Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //for each item on the queue, check if the file size isn't bigger than 2gb
                foreach (string file in CorruptionQueueList.Items)
                {
                    if (new FileInfo(file).Length > 2147483648)
                    {
                        MessageBox.Show("File " + file + " is too big to be corrupted.", "File Too Big", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                Program.Form.FileSelectiontxt.Text = "---Multiple files selected---";
                Program.Form.SaveasTxt.Text = "---Corruption will be applied on the files selected---";
                Hide();
            }
        }

        private void AddFolderBTN_Click(object sender, EventArgs e)
        {
            //Open the folder dialog box and once a folder has been selected, get each file and add them as a item on the corruptionqueuelist
            //Change root folder of folder dialog to be the the application startup folder
            folderBrowserDialog1.SelectedPath = Application.StartupPath;
            if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
            {
                foreach (string file in Directory.GetFiles(folderBrowserDialog1.SelectedPath))
                {
                    CorruptionQueueList.Items.Add(file);
                }
            }
        }

        private void removeselbtn_Click(object sender, EventArgs e)
        {
            //Remove each selected item
            while (CorruptionQueueList.SelectedItems.Count > 0)
            {
                CorruptionQueueList.Items.Remove(CorruptionQueueList.SelectedItems[0]);
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            CorruptionQueueList.Items.Clear();
        }

        private void CorruptionQueueForm_DragDrop(object sender, DragEventArgs e)
        {
            //Get the information from the file dragged.
            DragandDropICON.Hide();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var path1 in files)
            {
                //add files to the queue list
                CorruptionQueueList.Items.Add(path1);
            }
        }

        private void CorruptionQueueForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                DragandDropICON.BringToFront();
                DragandDropICON.Show();
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void CorruptionQueueForm_DragLeave(object sender, EventArgs e)
        {
            DragandDropICON.Hide();
        }

        private void RunSelectedFileInEmuBTN_Click(object sender, EventArgs e)
        {
            //Get the selected item text and put it in the run textbox
            if (CorruptionQueueList.SelectedItems.Count > 0)
            {
                SelectedRunFileTXT.Text = CorruptionQueueList.GetItemText(CorruptionQueueList.SelectedItem);
            }
        }

        private void ClearSelectedRunFileBTN_Click(object sender, EventArgs e)
        {
            //Clear Textbox
            SelectedRunFileTXT.Text = "";
        }
    }
}
