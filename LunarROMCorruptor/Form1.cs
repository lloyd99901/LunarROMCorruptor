using LunarROMCorruptor.CorruptionEngines;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
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
        private int MaxByte;
        private int StartByte;
        private int EndByte;
        private readonly Random rnd = new Random();
        private readonly string vernumber = "v0.1";
        public List<string> StashItems = new List<string>(); //Adding to this list will make corruptions faster as it's not in the GUI so it doesn't have to render every item update.

        public readonly CorruptionEngineOptions objForm2 = new CorruptionEngineOptions()
        {
            TopLevel = false
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "LunarROMCorruptor - " + vernumber + " - UNSTABLE BUILD";
            AboutVerLabel.Text = vernumber;
            if (!Directory.Exists(Application.StartupPath + "\\Saves\\"))
            {
                MessageBox.Show("Welcome to LunarROMCorruptor! Make sure you read the agreement before using this corruptor. Other than that enjoy corrupting whatever you like!");
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
                if (!string.IsNullOrEmpty(EmulatorLocationtxt.Text))
                {
                    Runemulatorchbox.Checked = true;
                    EmulatorLocationtxt.BackColor = Color.White;
                    BrowseEmulatorbutton.Enabled = true;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("executablepath.ini not found. Ignoring.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Loading executablepath.ini: " + ex.Message);
            }
            finally
            {
                RefreshCorruptionStashList();
                RefreshFileSaves();

                objForm2.TopLevel = false;
                TabPage4.Controls.Add(objForm2);
                objForm2.Dock = DockStyle.Fill;
                objForm2.Show();
            }
        }

        public void RefreshCorruptionStashList()
        {
            StashList.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\CorruptionStashList\");
            FileInfo[] diar1 = di.GetFiles();
            foreach (var dra in diar1)
            {
                StashList.Items.Add(dra);
            }
        }

        public void RefreshFileSaves()
        {
            FilesaveList.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\Saves\");
            FileInfo[] diar1 = di.GetFiles();

            // list the names of all files in the specified directory
            foreach (FileInfo dra in diar1)
            {
                FilesaveList.Items.Add(dra);
            }
        }

        public void SaveCorruptedFile()
        {
            try
            {
                int fileCount = Directory.GetFiles(Application.StartupPath + "\\Saves\\", " *.* ", SearchOption.TopDirectoryOnly).Length;
                string exc = Path.GetExtension(SaveasTxt.Text);
                File.Copy(SaveasTxt.Text, Application.StartupPath + "\\Saves\\" + Path.GetFileNameWithoutExtension(SaveasTxt.Text) + fileCount + 1 + exc);
                FilesaveList.Items.Add(Path.GetFileNameWithoutExtension(SaveasTxt.Text) + fileCount + 1 + exc);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("SaveCorruptedFile Argument Exception: Is there a file loaded?");
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

        private void CorruptionEngineComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            objForm2.MergeEnginePanel.Visible = false;
            objForm2.LogicEnginePanel.Visible = false;
            objForm2.HellEnginePanel.Visible = false;
            objForm2.NightmareEnginePanel.Visible = false;
            objForm2.LerpEnginePanel.Visible = false;
            objForm2.Vector2EnginePanel.Visible = false;
            ManualEnginePanel.Hide();
            objForm2.Show();
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

        private void CorruptButton_Click(object sender, EventArgs e)
        {
            StashItemList.Items.Clear();
            StashItems.Clear();
            StashItems.TrimExcess(); //This probably isn't required, it resizes the internal array to free up more memory.
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
                MessageBox.Show("Start byte is incorrect or invaild.", "Error - LunarROMCorruptor ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                EndByte = (int)EndByteNumb.Value;
            }
            catch
            {
                MessageBox.Show("End byte is incorrect or invaild.", "Error - LunarROMCorruptor ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ROM = File.ReadAllBytes(MainOpenFileDialog.FileName);
            //Hell engine goes here.
            byte[] FinROM = null;

            if (CorruptionEngineComboBox.Text == "Hell Engine")
            {
                for (int i1 = 0; i1 <= Intensity.Value - 1; i1++)
                {
                    int intes = (int)Intensity.Value;
                    Intensity.Value = 1;
                    int randomIndex = rnd.Next(1, CorruptionEngineComboBox.Items.Count - 1);
                    CorruptionEngineComboBox.SelectedIndex = randomIndex;
                    if (CorruptionEngineComboBox.Text == "Merge Engine" && string.IsNullOrEmpty(objForm2.TextBox5.Text))
                    {
                        CorruptionEngineComboBox.Text = "Nightmare Engine";
                    }
                    FinROM = StartCorruption(ROM);
                    Intensity.Value = intes;
                }
                CorruptionEngineComboBox.Text = "Hell Engine";
            }
            else
            {
                FinROM = StartCorruption(ROM);
            }

            if (FinROM == null)
            {
                MessageBox.Show("FinROM returned NULL! Corruption Failed!");
                return;
            }

            File.WriteAllBytes(SaveasTxt.Text, FinROM);

            if (FilesaveEnableAutoSaves.Checked)
            {
                SaveCorruptedFile();
            }

            CorruptButton.BackColor = Color.Green;
            using (var soundPlayer = new SoundPlayer(Properties.Resources.success2))
            {
                soundPlayer.Play();
            }
            CorruptButtonColorChanger.Start();
            if (Runemulatorchbox.Checked && string.IsNullOrEmpty(EmulatorLocationtxt.Text))
            {
                StartEmulator();
            }

            if (EnableStashSavesChkbox.Checked)
            {
                if (StashItems.Count > 50000)
                {
                    StashItemList.Items.Add("LargeStash");
                }
                else
                {
                    foreach (var item in StashItems)
                    {
                        StashItemList.Items.Add(item);
                    }
                }
            }
        }

        private void CorruptButtonColorChanger_Tick(object sender, EventArgs e)
        {
            CorruptButton.BackColor = Color.FromArgb(255, 32, 32, 32);
            CorruptButtonColorChanger.Stop();
        }

        private void StartEmulator()
        {
            try
            {
                if (ReopenChbox.Checked == true)
                {
                    var processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(EmulatorLocationtxt.Text));
                    if (processes.Length > 0)
                        processes[0].CloseMainWindow();
                }
                System.Threading.Thread.Sleep(300);
                Process p = new Process();
                p.StartInfo.FileName = EmulatorLocationtxt.Text;
                if (OverrideArgumentschbox.Checked == false)
                    p.StartInfo.Arguments = "\"" + SaveasTxt.Text + "\"";
                else
                    p.StartInfo.Arguments = OverrideArguments.Text;
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when starting process/emulator: " + ex.ToString());
            }
        }

        public byte[] StartCorruption(byte[] ROM)
        {
            switch (CorruptionEngineComboBox.Text)
            {
                case "Nightmare Engine":
                    if (CorruptnthbyteCheckbox.Checked)
                    {
                        //CorruptNTH selected
                        int i1 = StartByte;
                        while (i1 <= EndByte)
                        {
                            Enum.TryParse(objForm2.ComboBox1.Text, out CorruptionOptions corruptiontype);
                            NightmareEngine.CorruptByte(ROM, corruptiontype, i1);
                            i1 += (int)EveryNthByte.Value;
                        }
                    }
                    else //Intensity Mode
                    {
                        for (int i1 = 0; i1 <= Intensity.Value - 1; i1++)
                        {
                            Enum.TryParse(objForm2.ComboBox1.Text, out CorruptionOptions corruptiontype);
                            //MessageBox.Show(StartByte.ToString() + EndByte.ToString());
                            NightmareEngine.CorruptByte(ROM, corruptiontype, rnd.Next(StartByte, EndByte));
                        }
                    }
                    break;

                case "Lerp Engine":
                    if (CorruptnthbyteCheckbox.Checked)
                    {
                        //CorruptNTH selected
                        int i1 = StartByte;
                        while (i1 <= EndByte)
                        {
                            LerpEngine.CorruptByte(ROM, i1);
                            i1 += (int)EveryNthByte.Value;
                        }
                    }
                    else //Intensity Mode
                    {
                        for (int i1 = 0; i1 <= Intensity.Value - 1; i1++)
                        {
                            LerpEngine.CorruptByte(ROM, rnd.Next(StartByte, EndByte));
                        }
                    }
                    break;

                case "Merge Engine":
                    if (CorruptnthbyteCheckbox.Checked)
                    {
                        //CorruptNTH selected
                        int i1 = StartByte;
                        while (i1 <= EndByte)
                        {
                            byte[] ROMmerge = File.ReadAllBytes(objForm2.TextBox5.Text);
                            Enum.TryParse(objForm2.CorrTypeMerge.Text, out CorruptionOptions corruptiontype);
                            MergeEngine.CorruptByte(ROM, ROMmerge, corruptiontype, i1);
                        }
                    }
                    else //Intensity Mode
                    {
                        for (int i1 = 0; i1 <= Intensity.Value - 1; i1++)
                        {
                            byte[] ROMmerge = File.ReadAllBytes(objForm2.TextBox5.Text);
                            Enum.TryParse(objForm2.CorrTypeMerge.Text, out CorruptionOptions corruptiontype);
                            MergeEngine.CorruptByte(ROM, ROMmerge, corruptiontype, rnd.Next(StartByte, EndByte));
                        }
                    }
                    break;

                case "Manual":
                    if (CorruptnthbyteCheckbox.Checked) //CorruptNTH mode
                    {
                        int i = StartByte;
                        while (i <= EndByte)
                        {
                            ManualEngine.CorruptByte(ROM, i, StartByte, EndByte);
                            i += (int)EveryNthByte.Value;
                        }
                    }
                    else //Intense mode
                    {
                        for (int i1 = 0; i1 <= Intensity.Value - 1; i1++)
                        {
                            long i = rnd.Next(StartByte, EndByte);
                            ManualEngine.CorruptByte(ROM, i, StartByte, EndByte);
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

        private void Restorefilebtn_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllBytes(SaveasTxt.Text, backupROM);
                CorruptButton.BackColor = Color.Green;
                using (var soundPlayer = new SoundPlayer(Properties.Resources.success2))
                {
                    soundPlayer.Play();
                }
                CorruptButtonColorChanger.Start();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("You don't have a file open!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TransferStash_Click(object sender, EventArgs e)
        {
            if (StashItemList.Items.Count == 0)
            {
                return;
            }

            StringBuilder builder = new StringBuilder();
            StashList.Items.Clear();

            if (StashItemList.Items[0].ToString() == "LargeStash")
            {
                if (MessageBox.Show("This is a large stash which may take awhile to load in the future. Are you sure you want to save anyway?", "",
 MessageBoxButtons.YesNo,
 MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (var listitem in StashItems)
                    {
                        builder.Append(listitem);
                        builder.AppendLine();
                    }
                }
            }
            else
            {
                foreach (var listitem in StashItems)
                {
                    builder.Append(listitem);
                    builder.AppendLine();
                }
            }

            File.WriteAllText(Application.StartupPath + @"\CorruptionStashList\" + rnd.Next(1000, 999999999) + ".lunarstash", builder.ToString());
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\CorruptionStashList\");
            FileInfo[] diar1 = di.GetFiles();

            // list the names of all files in the specified directory
            foreach (var dra in diar1)
                StashList.Items.Add(dra);
        }

        private void StashEditorbtn_Click(object sender, EventArgs e)
        {
            StashEditor frm1 = new StashEditor();
            try
            {
                frm1.AttemptStashLoad(Application.StartupPath + "\\CorruptionStashList\\" + StashList.SelectedItems[0].ToString());
            }
            catch { }
            frm1.ShowDialog();
        }

        private void RenameStash_Click(object sender, EventArgs e)
        {
            InputBox input = new InputBox
            {
                Text = "Rename to?"
            };
            input.ShowDialog();
            if (string.IsNullOrEmpty(input.textBox1.Text)) { return; }
            var exc = Path.GetExtension(Application.StartupPath + @"\CorruptionStashList\" + StashList.GetItemText(StashList.SelectedItem));
            File.Move(Application.StartupPath + @"\CorruptionStashList\" + StashList.GetItemText(StashList.SelectedItem), Application.StartupPath + "\\CorruptionStashList\\" + input.textBox1.Text + exc);
            StashList.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\CorruptionStashList\");
            FileInfo[] diar1 = di.GetFiles();

            // list the names of all files in the specified directory
            foreach (var dra in diar1)
                StashList.Items.Add(dra);
            input.Dispose();
        }

        private void RefreshStash_Click(object sender, EventArgs e)
        {
            RefreshCorruptionStashList();
        }

        private void DeleteStash_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to deleted the selected item?", "",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    File.Delete(Application.StartupPath + @"\CorruptionStashList\" + StashList.GetItemText(StashList.SelectedItem));
                    StashList.Items.Clear();
                    DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\CorruptionStashList\");
                    FileInfo[] diar1 = di.GetFiles();

                    // list the names of all files in the specified directory
                    foreach (var dra in diar1)
                        StashList.Items.Add(dra);
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access Denied or nothing was selected for deletion. Check if you can write to that directory.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FilesaveCopysavetobtn_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy(Application.StartupPath + @"\Saves\" + FilesaveList.GetItemText(FilesaveList.SelectedItem), SaveasTxt.Text, true);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Argument Exception (FileSave). Did you select an item?", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilesaveReloadbtn_Click(object sender, EventArgs e)
        {
            RefreshFileSaves();
        }

        private void FilesaveSavebtn_Click(object sender, EventArgs e)
        {
            SaveCorruptedFile();
        }

        private void FilesaveRenameBtn_Click(object sender, EventArgs e)
        {
            InputBox input = new InputBox
            {
                Text = "Rename to?"
            };
            input.ShowDialog();
            if (string.IsNullOrEmpty(input.textBox1.Text)) { return; }
            var exc = Path.GetExtension(Application.StartupPath + @"\Saves\" + FilesaveList.GetItemText(FilesaveList.SelectedItem));
            File.Move(Application.StartupPath + @"\Saves\" + FilesaveList.GetItemText(FilesaveList.SelectedItem), Application.StartupPath + "\\Saves\\" + input.textBox1.Text + exc);
            FilesaveList.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\Saves\");
            FileInfo[] diar1 = di.GetFiles();

            // list the names of all files in the specified directory
            foreach (var dra in diar1)
                FilesaveList.Items.Add(dra);
            input.Dispose();
        }

        private void FilesaveDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to deleted the selected item?", "",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    File.Delete(Application.StartupPath + @"\Saves\" + FilesaveList.GetItemText(FilesaveList.SelectedItem));
                    FilesaveList.Items.Clear();
                    DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\Saves\");
                    FileInfo[] diar1 = di.GetFiles();

                    // list the names of all files in the specified directory
                    foreach (var dra in diar1)
                        FilesaveList.Items.Add(dra);
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access Denied or nothing was selected for deletion. Check if you can write to that directory.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Corruptusingstash_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    ROM = File.ReadAllBytes(MainOpenFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                foreach (var line in File.ReadAllLines(Application.StartupPath + "\\CorruptionStashList\\" + StashList.GetItemText(StashList.SelectedItem)))
                {
                    Object[] splitStrings;
                    Object[] Instructions;
                    try
                    {
                        splitStrings = line.Split(new string[] { "(", ")" }, StringSplitOptions.None);
                        Instructions = line.Split(new string[] { ".", "(" }, StringSplitOptions.None);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while splitting important variables: " + ex.Message);
                        return;
                    }
                    Object i;
                    Object result;
                    try
                    {
                        i = splitStrings[1];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invaild Corruption Location: " + ex.ToString());
                        return;
                    }

                    try
                    {
                        result = splitStrings[3];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invaild Result Location: " + ex.ToString());
                        return;
                    }

                    //if (Char.IsDigit((char)i) == false | Char.IsDigit((char)result) == false)
                    //{
                    //    MessageBox.Show("IsNumeric Location/Result Check Failed, File is most likely corrupted or is blank.");
                    //    return;
                    //}
                    //MessageBox.Show(i.ToString());
                    //MessageBox.Show(result.ToString());
                    //ROM[(int)i] = (byte)result;
                    ROM[int.Parse(i.ToString())] = (byte)int.Parse(result.ToString());
                }
                File.WriteAllBytes(SaveasTxt.Text, ROM);
                CorruptButton.BackColor = Color.Green;
                using (var soundPlayer = new SoundPlayer(Properties.Resources.success2))
                {
                    soundPlayer.Play();
                }
                CorruptButtonColorChanger.Start();
                if (Runemulatorchbox.Checked && string.IsNullOrEmpty(EmulatorLocationtxt.Text))
                {
                    StartEmulator();
                }
            }
        }

        private void EnableStashSavesChkbox_CheckedChanged(object sender, EventArgs e)
        {
            StashItemList.Enabled = EnableStashSavesChkbox.Checked;
            TransferStash.Enabled = EnableStashSavesChkbox.Checked;
        }
    }
}