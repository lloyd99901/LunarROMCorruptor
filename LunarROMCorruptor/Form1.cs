using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

//MIT License

//Copyright (c) 2020 LunarHunter

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

namespace LunarROMCorruptor
{
    public partial class Form1 : Form
    {
        private byte[] ROM;
        private byte[] backupROM;

        //private byte[] ROMmerge; This probably should be in the Merge Engine Code (when it's implemented that is...)
        private int MaxByte;

        private int StartByte;
        private int EndByte;
        Random rnd = new Random();
        private Programlog LoggingForm = new Programlog();

        private CorruptionEngineOptions objForm2 = new CorruptionEngineOptions()
        {
            TopLevel = false
        };

        public Form1()
        {
            InitializeComponent();
        }

        static public double LinearInterpolationCalculation(double v0, double v1, double t)
        {
            //LinearInterpolationCalculation(0, 100, 0.5).ToString() returns 50.
            return Math.Round(Math.Abs(v0 + (t * (v1 - v0))));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Application.StartupPath + "\\Saves\\"))
            {
                MessageBox.Show("Welcome to LunarROMCorruptorV3! Make sure you read the agreement before using this corruptor. Other than that enjoy corrupting whatever you like!");
            }
            Directory.CreateDirectory(Application.StartupPath + "\\Saves\\");
            Directory.CreateDirectory(Application.StartupPath + "\\CorruptionStashList\\");
            //Directory.CreateDirectory(Application.StartupPath + "\\RestoreMultipleFiles\\");
            AllowDrop = true;
            BrowseEmulatorbutton.Enabled = false;
            EmulatorLocationtxt.BackColor = Color.Gray;
            try
            {
                EmulatorLocationtxt.Text = File.ReadAllText(Application.StartupPath + "\\executablepath.ini");
                if (string.IsNullOrEmpty(EmulatorLocationtxt.Text))
                {
                    Runemulatorchbox.Checked = true;
                    EmulatorLocationtxt.BackColor = Color.White;
                    BrowseEmulatorbutton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ReportException(ex);
            }
            finally
            {
                DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\Saves\");
                FileInfo[] diar1 = di.GetFiles();

                // list the names of all files in the specified directory
                foreach (FileInfo dra in diar1)
                {
                    FilesaveList.Items.Add(dra);
                }
                di = new DirectoryInfo(Application.StartupPath + @"\CorruptionStashList\");
                diar1 = di.GetFiles();
                foreach (var dra in diar1)
                {
                    StashList.Items.Add(dra);
                }
                objForm2.TopLevel = false;
                TabPage4.Controls.Add(objForm2);
                objForm2.Dock = DockStyle.Fill;
                objForm2.Show();
            }
        }

        public void LoadFile()
        {
            if (MainOpenFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                if (MultipleFilesChbx.Checked)
                {
                    FileSelectiontxt.Text = MainOpenFileDialog.FileName;
                    SaveasTxt.Text = MainOpenFileDialog.FileName;
                    //MultipleFileList.Items.Add(FileSelectiontxt.Text);
                }
                else
                {
                    ROM = File.ReadAllBytes(MainOpenFileDialog.FileName);
                    FileSelectiontxt.Text = MainOpenFileDialog.FileName;
                    SaveasTxt.Text = MainOpenFileDialog.FileName;
                    MainSaveFileDialog.FileName = Path.GetDirectoryName(SaveasTxt.Text);
                    string exc = Path.GetExtension(MainOpenFileDialog.FileName);
                    SaveasTxt.Text = SaveasTxt.Text.Replace(Path.GetFileName(MainOpenFileDialog.FileName), "CorruptedFile" + exc);
                    MaxByte = ROM.Length - 1;
                    StartByteTrackBar.Maximum = MaxByte;
                    EndByteTrackbar.Maximum = MaxByte;
                    EndByteTrackbar.Value = MaxByte;
                    EndByteNumb.Maximum = MaxByte;
                    EndByteNumb.Value = MaxByte;
                    StartByteNumb.Maximum = MaxByte;
                    backupROM = ROM;
                }
            }
        }

        private void Openfilebtn_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        private void StartByteTrackBar_Scroll(object sender, EventArgs e)
        {
            StartByteNumb.Value = StartByteTrackBar.Value;
        }

        private void EndByteTrackbar_Scroll(object sender, EventArgs e)
        {
            EndByteNumb.Value = EndByteTrackbar.Value;
        }

        private void CorrupteverynthbyteTrackbar_Scroll(object sender, EventArgs e)
        {
            EveryNthByte.Value = CorrupteverynthbyteTrackbar.Value;
        }

        private void CorruptnthbyteCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            EverynthbyteGroupbox.Visible = CorruptnthbyteCheckbox.Checked;
        }

        private void IntensityTrackbar_Scroll(object sender, EventArgs e)
        {
            Intensity.Value = IntensityTrackbar.Value;
        }

        private void Intensity_ValueChanged(object sender, EventArgs e)
        {
            IntensityTrackbar.Value = (int)Intensity.Value;
        }

        private void EveryNthByte_ValueChanged(object sender, EventArgs e)
        {
            CorrupteverynthbyteTrackbar.Value = (int)EveryNthByte.Value;
        }

        private void AllowLargeIntensity_CheckedChanged(object sender, EventArgs e)
        {
            if (AllowLargeIntensity.Checked)
            {
                Intensity.Maximum = 99999;
                IntensityTrackbar.Maximum = 99999;
                CorrupteverynthbyteTrackbar.Maximum = 99999;
                EveryNthByte.Maximum = 99999;
            }
            else
            {
                Intensity.Maximum = 1000;
                IntensityTrackbar.Maximum = 1000;
                CorrupteverynthbyteTrackbar.Maximum = 1000;
                EveryNthByte.Maximum = 1000;
            }
        }

        private void UseHexchbox_CheckedChanged(object sender, EventArgs e)
        {
            StartByteNumb.Hexadecimal = UseHexchbox.Checked;
            EndByteNumb.Hexadecimal = UseHexchbox.Checked;
        }

        private void Changesaveasbtn_Click(object sender, EventArgs e)
        {
            string exc1 = Path.GetExtension(FileSelectiontxt.Text);
            MainSaveFileDialog.FileName = "CorruptedFile" + exc1;
            if (MainSaveFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                SaveasTxt.Text = MainSaveFileDialog.FileName;
            }
        }

        private void ConsoleButton_Click(object sender, EventArgs e)
        {
            LoggingForm.Show();
        }

        private void CorruptionEngineComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            objForm2.MergeEnginePanel.Visible = false;
            objForm2.LogicEnginePanel.Visible = false;
            objForm2.HellEnginePanel.Visible = false;
            objForm2.NightmareEnginePanel.Visible = false;
            objForm2.LerpEnginePanel.Visible = false;
            objForm2.Vector2EnginePanel.Visible = false;
            ManualEnginePanel.Hide();
            switch (CorruptionEngineComboBox.Text)
            {
                case "Nightmare Engine":
                    objForm2.NightmareEnginePanel.Visible = true;
                    break;

                case "Merge Engine":
                    objForm2.MergeEnginePanel.Visible = true;
                    break;

                case "Logic Engine":
                    objForm2.LogicEnginePanel.Visible = true;
                    break;

                case "Hell Engine":
                    objForm2.HellEnginePanel.Visible = true;
                    break;

                case "Lerp Engine":
                    objForm2.LerpEnginePanel.Visible = true;
                    break;

                case "Vector2 Engine":
                    objForm2.Vector2EnginePanel.Visible = true;
                    break;

                default:
                    objForm2.Hide();
                    ManualEnginePanel.Show();
                    break;
            }
        }

        private void BrowseEmulatorbutton_Click(object sender, EventArgs e)
        {
            if (EmulatorFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                EmulatorLocationtxt.Text = EmulatorFileDialog.FileName;
                File.WriteAllText(Application.StartupPath + "\\executablepath.ini", EmulatorFileDialog.FileName);
            }
        }

        private void OverrideArgumentschbox_CheckedChanged(object sender, EventArgs e)
        {
            OverrideArguments.Enabled = OverrideArgumentschbox.Checked;
        }

        private void Runemulatorchbox_CheckedChanged(object sender, EventArgs e)
        {
            if (Runemulatorchbox.Checked)
            {
                EmulatorLocationtxt.BackColor = Color.White;
                BrowseEmulatorbutton.Enabled = true;
            }
            else
            {
                EmulatorLocationtxt.BackColor = Color.Gray;
                BrowseEmulatorbutton.Enabled = false;
            }
        }

        private void StashEditorButton_Click(object sender, EventArgs e)
        {
            StashEditor frm1 = new StashEditor();
            frm1.ShowDialog();
        }

        public void ReportException(Exception ex)
        {
            LoggingForm.RichTextBox1.AppendText("\n" + DateTime.Now.ToString() + " - [Exception]: " + ex.ToString());
        }

        private void CorruptButton_Click(object sender, EventArgs e)
        {
            StashItemList.Items.Clear();
            ROM = backupROM;
            //Here is where the multiple files check should occurr
            if (string.IsNullOrEmpty(FileSelectiontxt.Text))
            {
                MessageBox.Show("File hasn't been selected.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (StartByteNumb.Value > EndByteNumb.Value)
            {
                MessageBox.Show("File hasn't been selected.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (EndByteNumb.Value < StartByteNumb.Value) //This check may not be necessary
            {
                MessageBox.Show("File hasn't been selected.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                File.WriteAllBytes(SaveasTxt.Text, backupROM);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("You haven't got a file open! Error code: 3x013");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            ROM = File.ReadAllBytes(MainOpenFileDialog.FileName);
            //Hell engine goes here.#

            byte[] FinROM = StartCorruption(ROM);
            if (FinROM == null)
            {
                MessageBox.Show("FinROM returned NULL!");
                return;
            }

            File.WriteAllBytes(SaveasTxt.Text, FinROM);

            CorruptButton.BackColor = Color.Green;
            using (var soundPlayer = new SoundPlayer(Properties.Resources.success))
            {
                soundPlayer.Play();
            }
            CorruptButtonColorChanger.Start();
        }

        private void CorruptButtonColorChanger_Tick(object sender, EventArgs e)
        {
            CorruptButton.BackColor = Color.FromArgb(255, 32, 32, 32);
            CorruptButtonColorChanger.Stop();
        }

        public byte[] StartCorruption(byte[] ROM)
        {
            if (StartByteNumb.Value > MaxByte)
            {
                StartByteNumb.Value = Int16.Parse(MaxByte.ToString("X"));
            }
            try
            {
                StartByte = (int)StartByteNumb.Value;
            }
            catch
            {
                MessageBox.Show("Start byte is incorrect or invaild.", "Error - LunarROMCorruptor v3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            try
            {
                EndByte = (int)EndByteNumb.Value;
            }
            catch
            {
                MessageBox.Show("End byte is incorrect or invaild.", "Error - LunarROMCorruptor v3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            switch (CorruptionEngineComboBox.Text)
            {
                case "Nightmare Engine":
                    if (CorruptnthbyteCheckbox.Checked)
                    {
                        //CorruptNTH selected
                    }
                    else
                    {
                        for (int i1 = 0; i1 <= Intensity.Value - 1; i1++)
                        {
                            if (objForm2.ComboBox1.Text == "RANDOM")
                            {
                                long i = rnd.Next(StartByte, EndByte);
                                ROM[i] = (byte)rnd.Next(0, 256);
                                StashItemList.Items.Add("L: FILE("+ i + ").set("+ ROM[i]+")");
                            }
                        }
                    }
                    break;

                default:
                    MessageBox.Show("Default case was hit in the StartCorruption function!");
                    break;
            }
            return ROM;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var path1 in files)
            {
                MainOpenFileDialog.FileName = path1;
                ROM = File.ReadAllBytes(path1);
                MaxByte = ROM.Length - 1;
                StartByteNumb.Maximum = MaxByte;
                StartByteTrackBar.Maximum = MaxByte;
                EndByteTrackbar.Maximum = MaxByte;
                EndByteTrackbar.Value = MaxByte;
                EndByteNumb.Maximum = MaxByte;
                EndByteNumb.Value = MaxByte;
                FileSelectiontxt.Text = path1;
                SaveasTxt.Text = path1;
                var exc = Path.GetExtension(path1);
                SaveasTxt.Text = SaveasTxt.Text.Replace(Path.GetFileName(path1), "CorruptedFile" + exc);
                backupROM = ROM;
                MainSaveFileDialog.FileName = Path.GetDirectoryName(SaveasTxt.Text);
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
    }
}