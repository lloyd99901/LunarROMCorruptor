using LunarROMCorruptor.CorruptionInternals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
using System.Windows.Forms;

//MIT License

//Copyright (c) 2022 LunarHunter

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
        private int MaxByte;
        private int StartByte;
        private int EndByte;
        private readonly Random rnd = new Random();
        private readonly string vernumber = "v1.0 - Build Number: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
            Text = "LunarROMCorruptor - " + vernumber;
            AboutVerLabel.Text = vernumber;
            if (!Directory.Exists(Application.StartupPath + "\\Saves\\"))
            {
                MessageBox.Show("Welcome to LunarROMCorruptor!\n\nDisclaimer:\nLunarROMCorruptor is distributed under an MIT license.\n\nBy clicking OK, you agree to that license and also understand the risks and disclaimers provided.\n\nThis program can irreversibly corrupt personal or critical system data if you're not careful.\nThis program comes with no warranty of ANY kind and is provided 'AS IS'.\nYou're responsible for backing up your data before use and for any damage that comes with the use or misuse of this program.\n\nThis message will not show up again.\n\nEnjoy!", "LunarROMCorruptor - INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Directory.CreateDirectory(Application.StartupPath + "\\Saves\\");
            Directory.CreateDirectory(Application.StartupPath + "\\CorruptionStashList\\");
            CorruptionEngineComboBox.Text = "Nightmare Engine";
            AllowDrop = true;
            BrowseEmulatorbutton.Enabled = false;
            EmulatorLocationtxt.BackColor = Color.Gray;
            RefreshCorruptionStashList();
            RefreshFileSaves();
            LoadSettings();
            objForm2.TopLevel = false;
            TabPage4.Controls.Add(objForm2);
            objForm2.Dock = DockStyle.Fill;
            objForm2.Show();
        }

        public void LoadSettings()
        {
            //Read & check if the emulator path is valid
            if (!string.IsNullOrEmpty(Properties.Settings.Default.EmulatorPath) && File.Exists(Properties.Settings.Default.EmulatorPath))
            {
                EmulatorLocationtxt.Text = Properties.Settings.Default.EmulatorPath;
            }
            
            //Read silent corurption setting
            if (Properties.Settings.Default.SilentCorruption)
            {
                SilentCorruptionchbox.Checked = true;
            }
            else
            {
                SilentCorruptionchbox.Checked = false;
            }
            //Read if stash list is enabled
            if (Properties.Settings.Default.StashSavesEnabled)
            {
                EnableStashSavesChkbox.Checked = true;
            }
            else
            {
                EnableStashSavesChkbox.Checked = false;
            }
            //Read if HEX mode is enabled
            if (Properties.Settings.Default.UseHex)
            {
                UseHexchbox.Checked = true;
            }
            else
            {
                UseHexchbox.Checked = false;
            }
            //Read if RunEmulator is enabled
            if (Properties.Settings.Default.RunEmulator)
            {
                Runemulatorchbox.Checked = true;
            }
            else
            {
                Runemulatorchbox.Checked = false;
            }
            //Read if ReopenEmulator is enabled
            if (Properties.Settings.Default.ReopenEmulator)
            {
                ReopenChbox.Checked = true;
            }
            else
            {
                ReopenChbox.Checked = false;
            }
            //Read if AutoFileSave is enabled
            if (Properties.Settings.Default.AutoFileSaveEnabled)
            {
                FilesaveEnableAutoSaves.Checked = true;
            }
            else
            {
                FilesaveEnableAutoSaves.Checked = false;
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
            if (SaveasTxt.Text == "No save location set.")
            {
                return;
            }
            try
            {
                int fileCount = Directory.GetFiles(Application.StartupPath + "\\Saves\\", " *.* ", SearchOption.TopDirectoryOnly).Length;
                string exc = Path.GetExtension(SaveasTxt.Text);
                File.Copy(SaveasTxt.Text, Application.StartupPath + "\\Saves\\" + Path.GetFileNameWithoutExtension(SaveasTxt.Text) + fileCount + 1 + exc);
                FilesaveList.Items.Add(Path.GetFileNameWithoutExtension(SaveasTxt.Text) + fileCount + 1 + exc);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("SaveCorruptedFile Argument Error: Is there a file loaded?");
            }
        }

        public void LoadFile(string FileLocation)
        {
            //Check if file is less than 2GB
            if (new FileInfo(FileLocation).Length < 2147483648)
            {
                ROM = File.ReadAllBytes(FileLocation);
            }
            else
            {
                MessageBox.Show("File is too large to load.\n\nFile must be less than 2GB in size.", "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FileSelectiontxt.Text = FileLocation;
            SaveasTxt.Text = FileLocation;
            MainSaveFileDialog.FileName = Path.GetDirectoryName(SaveasTxt.Text);
            string exc = Path.GetExtension(FileLocation);
            SaveasTxt.Text = SaveasTxt.Text.Replace(Path.GetFileName(FileLocation), "CorruptedFile" + exc);
            MaxByte = ROM.Length - 1;
            StartByteTrackBar.Maximum = MaxByte;
            EndByteTrackbar.Maximum = MaxByte;
            EndByteTrackbar.Value = MaxByte;
            EndByteNumb.Maximum = MaxByte;
            EndByteNumb.Value = MaxByte;
            StartByteNumb.Maximum = MaxByte;
        }

        private void Openfilebtn_Click(object sender, EventArgs e)
        {
            if (MainOpenFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                LoadFile(MainOpenFileDialog.FileName);
            }
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
            //Save changes to settings
            Properties.Settings.Default.UseHex = UseHexchbox.Checked;
            Properties.Settings.Default.Save();
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
                //Save selected path to settings
                Properties.Settings.Default.EmulatorPath = EmulatorFileDialog.FileName;
                Properties.Settings.Default.Save();
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
            //Save changes to settings
            Properties.Settings.Default.RunEmulator = Runemulatorchbox.Checked;
            Properties.Settings.Default.Save();
        }

        private void CorruptButton_Click(object sender, EventArgs e)
        {
            StashItemList.Items.Clear();
            StashItems.Clear();
            StashItems.TrimExcess(); //This probably isn't required, it resizes the internal array to free up more memory.
            //Checks if the file is fit for corruption
            if (string.IsNullOrEmpty(FileSelectiontxt.Text) || FileSelectiontxt.Text == "No file selected.")
            {
                MessageBox.Show("Please select a file to corrupt.", "No File Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Check if file is a valid ROM
            if (!File.Exists(FileSelectiontxt.Text))
            {
                MessageBox.Show("File doesn't exist.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (StartByteNumb.Value > EndByteNumb.Value)
            {
                MessageBox.Show("Start Byte cannot be greater than End Byte!", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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

            int tmpintensity; //See which corruption type was selected and give the correct intensity value. Used for the CorruptionCore.CorruptROM function
            if (CorruptnthbyteCheckbox.Checked)
            {
                tmpintensity = (int)EveryNthByte.Value;
            }
            else
            {
                tmpintensity = (int)Intensity.Value;
            }

            //ROM = File.ReadAllBytes(MainOpenFileDialog.FileName);
            //Hell engine goes here.
            byte[] FinROM = ROM.Clone() as byte[]; //Set FinROM to be the same as ROM

            FinROM = CorruptionCore.StartCorruption(FinROM, StartByte, EndByte, CorruptnthbyteCheckbox.Checked, tmpintensity, CorruptionEngineComboBox.Text); //Corrupts the ROM and returns the new ROM to the FinROM variable.
            
            if (FinROM == null) //check if the corruption worked.
            {
                MessageBox.Show("Corrupted ROM is null. If you haven't got an error explaining what went wrong, please report this to the developers with details of what you did.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check if the FinROM can be saved
            try
            {
                File.WriteAllBytes(SaveasTxt.Text, FinROM);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            if (FilesaveEnableAutoSaves.Checked)
            {
                SaveCorruptedFile();
            }

            //check if silent corruption is on. if it is, don't play the sound.
            CorruptButton.BackColor = Color.Green;
            if (!SilentCorruptionchbox.Checked)
            {
                using (var soundPlayer = new SoundPlayer(Properties.Resources.success3))
                {
                    soundPlayer.Play();
                }
            }

            CorruptButtonColorChanger.Start();
            if (Runemulatorchbox.Checked && File.Exists(EmulatorLocationtxt.Text))
            {
                CorruptionCore.StartEmulator(ReopenChbox.Checked, EmulatorLocationtxt.Text, OverrideArgumentschbox.Checked, OverrideArguments.Text);
            }
            else if (!File.Exists(EmulatorLocationtxt.Text))
            {
                MessageBox.Show("Emulator doesn't exist.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (EnableStashSavesChkbox.Checked)
            {
                if (StashItems.Count > 50000) //This check is for preventing too many GUI rendering updates on the listbox. The items are contained in a variable but are not shown.
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
            Console.WriteLine("Number of Stash Items: " + StashItems.Count);
        }

        private void CorruptButtonColorChanger_Tick(object sender, EventArgs e)
        {
            CorruptButton.BackColor = Color.FromArgb(255, 32, 32, 32);
            CorruptButtonColorChanger.Stop();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            DragandDropICON.Hide();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var path1 in files)
            {
                MainOpenFileDialog.FileName = path1;
                //Check if file can be read
                try
                {
                    ROM = File.ReadAllBytes(path1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when reading file: " + ex.ToString());
                    return;
                }
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
                MainSaveFileDialog.FileName = Path.GetDirectoryName(SaveasTxt.Text);
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                DragandDropICON.BringToFront();
                DragandDropICON.Show();
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Restorefilebtn_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllBytes(SaveasTxt.Text, ROM);
                CorruptButton.BackColor = Color.Green;
                if (!SilentCorruptionchbox.Checked)
                {
                    using (var soundPlayer = new SoundPlayer(Properties.Resources.success3))
                    {
                        soundPlayer.Play();
                    }
                }
                CorruptButtonColorChanger.Start();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("You don't have a file open!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when restoring file: " + ex.ToString());
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
            //Check if Application.StartupPath + @"\CorruptionStashList\" directory is valid
            if (!Directory.Exists(Application.StartupPath + @"\CorruptionStashList\"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\CorruptionStashList\");
            }

            File.WriteAllText(Application.StartupPath + @"\CorruptionStashList\" + rnd.Next(1000, 999999999) + ".stash", builder.ToString());
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
            if (string.IsNullOrEmpty(input.InputBoxTxtBox.Text)) { return; }
            var exc = Path.GetExtension(Application.StartupPath + @"\CorruptionStashList\" + StashList.GetItemText(StashList.SelectedItem));
            //Check if file doesnt exist
            if (!File.Exists(Application.StartupPath + @"\CorruptionStashList\" + StashList.GetItemText(StashList.SelectedItem)))
            {
                MessageBox.Show("File doesn't exist!");
                return;
            }

            File.Move(Application.StartupPath + @"\CorruptionStashList\" + StashList.GetItemText(StashList.SelectedItem), Application.StartupPath + "\\CorruptionStashList\\" + input.InputBoxTxtBox.Text + exc);
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
                if (MessageBox.Show("Are you sure you want to delete the selected items?", "",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (var item in StashList.SelectedItems)
                    {
                        File.Delete(Application.StartupPath + @"\CorruptionStashList\" + item.ToString());
                    }
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
                MessageBox.Show("Access Denied or nothing was selected for deletion. Check if you can write or you have authorization to that directory.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FilesaveCopysavetobtn_Click(object sender, EventArgs e)
        {
            if (SaveasTxt.Text == "No save location set.")
            {
                return;
            }
            try
            {
                File.Copy(Application.StartupPath + @"\Saves\" + FilesaveList.GetItemText(FilesaveList.SelectedItem), SaveasTxt.Text, true);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Argument error (FileSave). Did you select an item?", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (string.IsNullOrEmpty(input.InputBoxTxtBox.Text)) { return; }
            var exc = Path.GetExtension(Application.StartupPath + @"\Saves\" + FilesaveList.GetItemText(FilesaveList.SelectedItem));
            //Check if file doesnt exist
            if (!File.Exists(Application.StartupPath + @"\Saves\" + FilesaveList.GetItemText(FilesaveList.SelectedItem)))
            {
                MessageBox.Show("File doesn't exist!");
                return;
            }
            File.Move(Application.StartupPath + @"\Saves\" + FilesaveList.GetItemText(FilesaveList.SelectedItem), Application.StartupPath + "\\Saves\\" + input.InputBoxTxtBox.Text + exc);
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
                if (MessageBox.Show("Are you sure you want to delete the selected items?", "",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (var item in FilesaveList.SelectedItems)
                    {
                        File.Delete(Application.StartupPath + @"\Saves\" + FilesaveList.GetItemText(item.ToString()));
                    }
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
                MessageBox.Show("Access Denied or nothing was selected for deletion. Check if you can write or have authorization to that directory.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Corruptusingstash_Click(object sender, EventArgs e)
        {
            {
                //Check if ROM isn't null before running code
                if (ROM == null)
                {
                    MessageBox.Show("No ROM loaded!" + Environment.NewLine + "Please load a ROM first.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("Split error in line: " + line + Environment.NewLine + "Error: " + ex.Message, "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("Location error in line: " + line + Environment.NewLine + "Error: " + ex.Message, "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        result = splitStrings[3];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Corruption error in line: " + line + Environment.NewLine + "Error: " + ex.Message, "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    try
                    {
                        ROM[int.Parse(i.ToString())] = (byte)int.Parse(result.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ROM byte stash corruption failed! Corruption cannot continue. \n More details:" + ex.ToString());
                        return;
                    }
                }
                File.WriteAllBytes(SaveasTxt.Text, ROM);
                CorruptButton.BackColor = Color.Green;
                if (!SilentCorruptionchbox.Checked)
                {
                    using (var soundPlayer = new SoundPlayer(Properties.Resources.success3))
                    {
                        soundPlayer.Play();
                    }
                }
                CorruptButtonColorChanger.Start();
                if (Runemulatorchbox.Checked && string.IsNullOrEmpty(EmulatorLocationtxt.Text))
                {
                    CorruptionCore.StartEmulator(ReopenChbox.Checked, EmulatorLocationtxt.Text, OverrideArgumentschbox.Checked, OverrideArguments.Text);
                }
            }
        }

        private void EnableStashSavesChkbox_CheckedChanged(object sender, EventArgs e)
        {
            StashItemList.Enabled = EnableStashSavesChkbox.Checked;
            TransferStash.Enabled = EnableStashSavesChkbox.Checked;
            //Save changes to settings
            Properties.Settings.Default.StashSavesEnabled = EnableStashSavesChkbox.Checked;
            Properties.Settings.Default.Save();
        }

        private void StartByteNumb_ValueChanged(object sender, EventArgs e)
        {
            StartByteTrackBar.Value = (int)StartByteNumb.Value;
        }

        private void EndByteNumb_ValueChanged(object sender, EventArgs e)
        {
            EndByteTrackbar.Value = (int)EndByteNumb.Value;
        }

        private void Form1_DragLeave(object sender, EventArgs e)
        {
            DragandDropICON.Hide();
        }

        private void StashList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                StashList.SelectedIndex = StashList.IndexFromPoint(e.X, e.Y);
                contextStripStash.Show(Cursor.Position);
            }
        }

        private void SilentCorruptionchbox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SilentCorruption = SilentCorruptionchbox.Checked;
            Properties.Settings.Default.Save();
        }

        private void ReopenChbox_CheckedChanged(object sender, EventArgs e)
        {
            //Save changes to settings
            Properties.Settings.Default.ReopenEmulator = ReopenChbox.Checked;
            Properties.Settings.Default.Save();
        }

        private void FilesaveEnableAutoSaves_CheckedChanged(object sender, EventArgs e)
        {
            //Save changes to settings
            Properties.Settings.Default.AutoFileSaveEnabled = FilesaveEnableAutoSaves.Checked;
            Properties.Settings.Default.Save();
        }

        private void CorruptionQueueChkbox_CheckedChanged(object sender, EventArgs e)
        {
            CorruptionQueueBtn.Visible = CorruptionQueueChkbox.Checked;
        }
    }
}