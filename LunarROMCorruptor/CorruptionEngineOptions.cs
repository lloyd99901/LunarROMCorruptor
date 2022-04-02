using System;
using System.IO;
using System.Windows.Forms;

namespace LunarROMCorruptor
{
    public partial class CorruptionEngineOptions : Form
    {
        public CorruptionEngineOptions()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                try
                {
                    MergeFileLocationTxt.Text = OpenFileDialog1.FileName;
                    FileInfo myFile = new FileInfo(Program.Form.FileSelectiontxt.Text);
                    long sizeInBytes = myFile.Length;
                    FileInfo myFile2 = new FileInfo(MergeFileLocationTxt.Text);
                    long sizeInBytes2 = myFile2.Length;
                    if (sizeInBytes2 < sizeInBytes)
                    {
                        MessageBox.Show("This file must be the same/bigger size in order for this engine to work.");
                        MergeFileLocationTxt.Text = "";
                        return;
                    }
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Please load a file into the corruptor first before loading in a file in the merge engine. (Argument Exception)");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}