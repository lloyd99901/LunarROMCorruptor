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

        private void LerpSplitValueTrackBar_Scroll(object sender, EventArgs e)
        {
            //Set the value of the trackbar to the textbox and move the decimal point down one, e.g 10 becomes 1.0
            LerpValueTxt.Text = (LerpSplitValueTrackBar.Value / 10.0).ToString();
        }

        private void LerpValueTxt_TextChanged(object sender, EventArgs e)
        {
            try //catch any errors and if there is an error, set the trackbar value to 0
            {
                float value;
                if (float.TryParse(LerpValueTxt.Text, out value))
                {
                    LerpSplitValueTrackBar.Value = (int)(value * 10); //Convert the textbox text into a float and set the trackbar value to the float value
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occured: {Environment.NewLine + ex.Message}", "Error parsing text - LunarROMCorruptor");
                LerpSplitValueTrackBar.Value = 0;
                LerpValueTxt.Text = "0";
            }
        }
    }
}