using System;
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

        private void Openfilebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    foreach (var line in File.ReadLines(OpenFileDialog1.FileName))
                    {
                        stashListbox.Items.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void StashEditor_Load(object sender, EventArgs e)
        {
            //Until I figure out how to access other parts of forms that are already open (like VB.net), this code DOESN'T work. - L
            //stashListbox.Items.Clear();
            //try
            //{
            //    foreach(var line in File.ReadLines(Application.StartupPath & "\\CorruptionStashList\\" & Form1.StashList.GetItemText(Form1.StashList.SelectedItem)){

            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }
    }
}