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

//Advisories:
//This program can irreversibly corrupt personal or critical system data if you're not careful.
//This program comes with no warranty of ANY kind and is provided 'AS IS'.
//You're responsible for backing up your data before use and for any damage that comes with the use or misuse of this program.
//Anti-cheat software may be triggered if corruption is used on multiplayer games.
//You may get banned if this corruptor is used in online games or games with anti-cheat software.
//Don't use this on system32 or any other system files...

namespace LunarROMCorruptor
{
    public partial class Form1 : Form
    {
        private byte[] ROM;
        private int MaxByte;
        private int StartByte;
        private int EndByte;
        private readonly Random rnd = new Random();
        private readonly string vernumber = "v1.0.3 - Build Number: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public List<string> InternalStashItems = new List<string>(); //Adding to this list will make corruptions faster as it's not in the GUI so it doesn't have to render every item update.
        readonly CorruptionQueueForm CorruptionQueueFormSettings = new CorruptionQueueForm();
        public readonly CorruptionEngineOptions CorruptionEngineFrame = new CorruptionEngineOptions() //This is the form that will be used to set the options for the corruption engine. It will be embedded in the main form.
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
            if (!Directory.Exists(Application.StartupPath + "\\Saves\\")) //If file doesn't exist, assume it's the first time the program has been run and create the directory.
            {
                MessageBox.Show("Welcome to LunarROMCorruptor!\n\nDisclaimer:\nLunarROMCorruptor is distributed under an MIT license.\n\nBy clicking OK, you agree to that license and also understand the risks and disclaimers provided.\n\nThis program can irreversibly corrupt personal or critical system data if you're not careful.\nThis program comes with no warranty of ANY kind and is provided 'AS IS'.\nYou're responsible for backing up your data before use and for any damage that comes with the use or misuse of this program.\n\nThis message will not show up again but you can read the license again by going to the 'About' tab.\n\nEnjoy!", "LunarROMCorruptor - INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            CorruptionEngineFrame.TopLevel = false;
            CorruptionEngineTab.Controls.Add(CorruptionEngineFrame); //Adds the CorruptionEngineFrame to the CorruptionEngineTab.
            CorruptionEngineFrame.Dock = DockStyle.Fill;
            CorruptionEngineFrame.Show();
            //Fixes a .net bug where the trackbar takes a huge amount of memory
            EndByteTrackbar.TickStyle = TickStyle.None;
            StartByteTrackBar.TickStyle = TickStyle.None;
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
            //Enumurates through the CorruptionStashList directory and adds all files to the Stash list.
            StashFileList.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\CorruptionStashList\");
            FileInfo[] diar1 = di.GetFiles();
            foreach (var dra in diar1)
            {
                StashFileList.Items.Add(dra);
            }
        }

        public void RefreshFileSaves()
        {
            //Enumurates through the Saves directory and adds all files to the File Saves list.
            FilesaveList.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\Saves\");
            FileInfo[] diar1 = di.GetFiles();

            // list the names of all files in the specified directory
            foreach (FileInfo dra in diar1)
            {
                FilesaveList.Items.Add(dra);
            }
        }

        public void SaveCorruptedFileCopy()
        {
            //File Saves Tab - Save the corrupted file to the Saves directory.
            if (SaveasTxt.Text == "No save location set.") 
            {
                return;
            }
            //Check if the file exists, if not DONT run
            if (!File.Exists(SaveasTxt.Text))
            {
                MessageBox.Show("The file you're trying to save doesn't exist. Please load a file first.", "LunarROMCorruptor - ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //Main Function - Preps the file for corruption.
            if (new FileInfo(FileLocation).Length < 2147483648) //Check if file is less than 2GB
            {
                //Check if the file size is 0
                if (new FileInfo(FileLocation).Length == 0)
                {
                    MessageBox.Show("The file you're trying to load is empty. Please load a valid file.", "LunarROMCorruptor - ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //Discard the previous ROM loaded into memory
                ROM = new byte[0];
                //GC collection force -Forces garbage collection
                GC.Collect();
                //GC wait for pending finalizers
                GC.WaitForPendingFinalizers();
                //Load ROM into memory.
                ROM = File.ReadAllBytes(FileLocation);
                MaxByte = ROM.Length - 1;
                StartByteTrackBar.Value = 0;
                StartByteTrackBar.Maximum = MaxByte;
                EndByteTrackbar.Maximum = MaxByte;
                EndByteTrackbar.Value = MaxByte;
                EndByteNumb.Maximum = MaxByte;
                EndByteNumb.Value = MaxByte;
                StartByteNumb.Maximum = MaxByte;
                StartByteNumb.Value = 0;
                FileSelectiontxt.Text = FileLocation;
                SaveasTxt.Text = FileLocation;
                MainSaveFileDialog.FileName = Path.GetDirectoryName(SaveasTxt.Text);
                string exc = Path.GetExtension(FileLocation);
                SaveasTxt.Text = SaveasTxt.Text.Replace(Path.GetFileName(FileLocation), "CorruptedFile" + exc);

                //Check if the file is bigger than 1 gig or less than 1 gig, on both conditions just return
                if (ROM.Length > 1073741824)
                {
                    //Change the CorruptButton to say "Corrupt File " and in brackets put the file size in Gigabytes
                    CorruptButton.Text = "Corrupt File (" + (Math.Round((double)new FileInfo(FileLocation).Length / 1073741824, 2)) + " GB)";
                }
                //else if the rom is less than 1mb, show the size of the file in KB
                else if (ROM.Length < 1000000)
                {
                    //Change the CorruptButton to say "Corrupt File " and in brackets put the file size in kilobytes
                    CorruptButton.Text = "Corrupt File (" + (Math.Round((double)new FileInfo(FileLocation).Length / 1000, 2)) + " KB)";
                }
                //else if the rom is less than 1gb, show the size of the file in MB
                else if (ROM.Length < 1073741824)
                {
                    //Change the CorruptButton to say "Corrupt File " and in bracket put the files size in megabytes
                    CorruptButton.Text = "Corrupt File (" + (Math.Round((double)new FileInfo(FileLocation).Length / 1000000, 2)) + "MB)";
                }
            }
            else
            {
                MessageBox.Show("File is too large to load.\n\nFile must be less than 2GB in size.", "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Openfilebtn_Click(object sender, EventArgs e)
        {
            //Main Function - If the user didn't cancel, load file.
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
            //Main Form - Change what the CorruptionEngineFrame displays.
            //Hide all corruption engine forms
            CorruptionEngineFrame.MergeEnginePanel.Visible = false;
            CorruptionEngineFrame.LogicEnginePanel.Visible = false;
            CorruptionEngineFrame.NightmareEnginePanel.Visible = false;
            CorruptionEngineFrame.LerpEnginePanel.Visible = false;
            CorruptionEngineFrame.Vector2EnginePanel.Visible = false;
            ManualEnginePanel.Hide();
            CorruptionEngineFrame.Show();
            switch (CorruptionEngineComboBox.Text)
            {
                case "Nightmare Engine":
                    CorruptionEngineFrame.NightmareEnginePanel.Visible = true;
                    break;
                case "Merge Engine":
                    CorruptionEngineFrame.MergeEnginePanel.Visible = true;
                    break;
                case "Logic Engine":
                    CorruptionEngineFrame.LogicEnginePanel.Visible = true;
                    break;
                case "Lerp Engine":
                    CorruptionEngineFrame.LerpEnginePanel.Visible = true;
                    break;
                case "Vector2 Engine":
                    CorruptionEngineFrame.Vector2EnginePanel.Visible = true;
                    break;
                default:
                    //If none matched the text, assume manual is selected.
                    CorruptionEngineFrame.Hide();
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
            //Main Function - Starts the corruption process.
            StashBytesList.Items.Clear();
            InternalStashItems.Clear();
            InternalStashItems.TrimExcess(); //This probably isn't required, it resizes the internal array to free up more memory.

            //---Check if the corruption queue has been enabled--- If so, use this multi-corruption code.
            if (CorruptionQueueChkbox.Checked)
            {
                //Check if the queue is empty
                if (CorruptionQueueFormSettings.CorruptionQueueList.Items.Count == 0)
                {
                    MessageBox.Show("The corruption queue is empty.\n\nPlease add files to the queue.", "Empty Queue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    //Check if the queue is full
                    if (CorruptionQueueFormSettings.CorruptionQueueList.Items.Count >= 100)
                    {
                        MessageBox.Show("The corruption queue is rather full of files.\n\nBe aware that this will impact corruption times.", "Full Queue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                //For each file on the corruptionqueuelist, make sure the file is valid for use
                for (int i = 0; i < CorruptionQueueFormSettings.CorruptionQueueList.Items.Count; i++)
                {
                    if (!File.Exists(CorruptionQueueFormSettings.CorruptionQueueList.Items[i].ToString()))
                    {
                        MessageBox.Show("The file " + CorruptionQueueFormSettings.CorruptionQueueList.Items[i].ToString() + " does not exist.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                //Standard Checks of the variables that are used in the corruption process.
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

                int tmpintensity; //See which corruption intensity type was selected and give the correct intensity value. Used for the FinROm = CorruptionCore.CorruptROM function
                if (CorruptnthbyteCheckbox.Checked)
                {
                    tmpintensity = (int)EveryNthByte.Value;
                }
                else
                {
                    tmpintensity = (int)Intensity.Value;
                }

                //For each file that is on the corruptionqueuelist, corrupt it.
                for (int i = 0; i < CorruptionQueueFormSettings.CorruptionQueueList.Items.Count; i++)
                {
                    //Same as the LoadFile code but without the updates on the CorruptButton text -- Discard the previous ROM loaded into memory
                    ROM = new byte[0];
                    //GC collection force -Forces garbage collection
                    GC.Collect();
                    //GC wait for pending finalizers
                    GC.WaitForPendingFinalizers();
                    //Load the file into ROM byte
                    ROM = File.ReadAllBytes(CorruptionQueueFormSettings.CorruptionQueueList.Items[i].ToString());
                    MaxByte = ROM.Length - 1;
                    StartByteTrackBar.Value = 0;
                    StartByteTrackBar.Maximum = MaxByte;
                    EndByteTrackbar.Maximum = MaxByte;
                    EndByteTrackbar.Value = MaxByte;
                    EndByteNumb.Maximum = MaxByte;
                    EndByteNumb.Value = MaxByte;
                    StartByteNumb.Maximum = MaxByte;
                    StartByteNumb.Value = 0;
                    FileSelectiontxt.Text = CorruptionQueueFormSettings.CorruptionQueueList.Items[i].ToString();
                    SaveasTxt.Text = CorruptionQueueFormSettings.CorruptionQueueList.Items[i].ToString();

                    //Start Corruption in CorruptionCore
                    ROM = CorruptionCore.StartCorruption(ROM, StartByte, EndByte, CorruptnthbyteCheckbox.Checked, tmpintensity, CorruptionEngineComboBox.Text);
                    //Check if the corruption returned anything
                    if (ROM != null)
                    {
                        //If it did, try to save the file
                        try
                        {
                            File.WriteAllBytes(CorruptionQueueFormSettings.CorruptionQueueList.Items[i].ToString(), ROM);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("The file " + CorruptionQueueFormSettings.CorruptionQueueList.Items[i].ToString() + " could not be saved.\n\n" + ex.ToString(), "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        //If it didn't, show an error message
                        MessageBox.Show("The file " + CorruptionQueueFormSettings.CorruptionQueueList.Items[i].ToString() + " could not be corrupted. (Corruption returned nothing)", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //Resets the text boxes to show this again.
                    FileSelectiontxt.Text = "---Multiple files selected---";
                    SaveasTxt.Text = "---Corruption will be applied on the files selected---";
                }
                if (Runemulatorchbox.Checked && File.Exists(EmulatorLocationtxt.Text)) // Check if the emulator works before running
                {
                    CorruptionCore.StartEmulator(ReopenChbox.Checked, EmulatorLocationtxt.Text, CorruptionQueueFormSettings.SelectedRunFileTXT.Text, OverrideArgumentschbox.Checked, OverrideArguments.Text); //Run emulator with the new ROM.
                }
                else if (Runemulatorchbox.Checked && !File.Exists(EmulatorLocationtxt.Text))
                {
                    MessageBox.Show("Emulator doesn't exist.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else //Use normal, one file corruption code.
            {
                //Check if the variables are valid
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

                int tmpintensity; //See which corruption intensity type was selected and give the correct intensity value. Used for the FinROm = CorruptionCore.CorruptROM function
                if (CorruptnthbyteCheckbox.Checked)
                {
                    tmpintensity = (int)EveryNthByte.Value;
                }
                else
                {
                    tmpintensity = (int)Intensity.Value;
                }

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

                byte[] FinROM = ROM.Clone() as byte[]; //Set FinROM to be the same as ROM. FinROM will contain the corrupted file when its run through the corruption engine.

                FinROM = CorruptionCore.StartCorruption(FinROM, StartByte, EndByte, CorruptnthbyteCheckbox.Checked, tmpintensity, CorruptionEngineComboBox.Text); //Corrupts the ROM and returns the new ROM to the FinROM variable.

                if (FinROM == null) //check if the corruption returned anything.
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
                    MessageBox.Show("Unable to save file.\n" + ex.ToString(), "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (FilesaveEnableAutoSaves.Checked) // If file saves are enabled, save the full file to the file saves folder.
                {
                    SaveCorruptedFileCopy();
                }

                if (Runemulatorchbox.Checked && File.Exists(EmulatorLocationtxt.Text)) // Check if the emulator works before running
                {
                    CorruptionCore.StartEmulator(ReopenChbox.Checked, EmulatorLocationtxt.Text, SaveasTxt.Text, OverrideArgumentschbox.Checked, OverrideArguments.Text); //Run emulator with the new ROM.
                }
                else if (Runemulatorchbox.Checked && !File.Exists(EmulatorLocationtxt.Text))
                {
                    MessageBox.Show("Emulator doesn't exist.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (EnableStashSavesChkbox.Checked)
                {
                    if (InternalStashItems.Count > 50000) //This check is for preventing too many GUI rendering updates on the listbox. The items are contained in a variable but are not shown.
                    {
                        StashBytesList.Items.Add("LargeStash");
                    }
                    else
                    {
                        foreach (var item in InternalStashItems)
                        {
                            StashBytesList.Items.Add(item); // Add the items to the listbox.
                        }
                    }
                }
                //Clear FINROM and clean memory
                FinROM = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            CorruptButton.BackColor = Color.Green; //Change colour of the corrupt button
            if (!SilentCorruptionchbox.Checked) //check if silent corruption is on. if it is, don't play the sound.
            {
                using (var soundPlayer = new SoundPlayer(Properties.Resources.success3))
                {
                    soundPlayer.Play();
                }
            }

            CorruptButtonColorChanger.Start(); //Change the corruption button colour back to normal after the timer ticks.
            Console.WriteLine("Corruption Complete: Number of Stash Items: " + InternalStashItems.Count); //Debugging
        }

        private void CorruptButtonColorChanger_Tick(object sender, EventArgs e)
        {
            CorruptButton.BackColor = Color.FromArgb(255, 32, 32, 32);
            CorruptButtonColorChanger.Stop(); //On tick, change the corrupt button colour back to normal and stop the timer.
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            //Get the information from the file dragged.
            DragandDropICON.Hide();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            //If there are mulitple files, open the corruption queue instead
            if (files.Length > 1)
            {
                //Enable Corruption Queue
                CorruptionQueueChkbox.Checked = true;
                //Add files to the listbox in the CorruptionQueue
                foreach (var item in files)
                {
                    CorruptionQueueFormSettings.CorruptionQueueList.Items.Add(item);
                }
                //Set TopMost
                CorruptionQueueFormSettings.TopMost = true;
                CorruptionQueueFormSettings.ShowDialog();
                //Revert Topmost
                CorruptionQueueFormSettings.TopMost = false;
                return;
            }
            foreach (var path1 in files)
            {
                LoadFile(path1); //Loads the ROM.
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
                File.WriteAllBytes(SaveasTxt.Text, ROM);// Try to write the ROM that is in memory into the save as file.
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
            if (StashBytesList.Items.Count == 0)
            {
                return;
            }

            StringBuilder builder = new StringBuilder();
            StashFileList.Items.Clear();

            if (StashBytesList.Items[0].ToString() == "LargeStash")
            {
                if (MessageBox.Show("This is a large stash which may take awhile to load in the future. Are you sure you want to save anyway?", "",
 MessageBoxButtons.YesNo,
 MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (var listitem in InternalStashItems)
                    {
                        builder.Append(listitem);
                        builder.AppendLine();
                    }
                }
            }
            else
            {
                foreach (var listitem in InternalStashItems)
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
                StashFileList.Items.Add(dra);
        }

        private void StashEditorbtn_Click(object sender, EventArgs e)
        {
            StashEditor frm1 = new StashEditor();
            try
            {
                frm1.AttemptStashLoad(Application.StartupPath + "\\CorruptionStashList\\" + StashFileList.SelectedItems[0].ToString());
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
            var exc = Path.GetExtension(Application.StartupPath + @"\CorruptionStashList\" + StashFileList.GetItemText(StashFileList.SelectedItem));
            //Check if file doesnt exist
            if (!File.Exists(Application.StartupPath + @"\CorruptionStashList\" + StashFileList.GetItemText(StashFileList.SelectedItem)))
            {
                MessageBox.Show("File doesn't exist!");
                return;
            }
            //Check if the file rename is vaild, e.g do not allow CON
            if (input.InputBoxTxtBox.Text.Contains("CON") || input.InputBoxTxtBox.Text.Contains("PRN") || input.InputBoxTxtBox.Text.Contains("AUX") || input.InputBoxTxtBox.Text.Contains("NUL") || input.InputBoxTxtBox.Text.Contains("COM1") || input.InputBoxTxtBox.Text.Contains("COM2") || input.InputBoxTxtBox.Text.Contains("COM3") || input.InputBoxTxtBox.Text.Contains("COM4") || input.InputBoxTxtBox.Text.Contains("COM5") || input.InputBoxTxtBox.Text.Contains("COM6") || input.InputBoxTxtBox.Text.Contains("COM7") || input.InputBoxTxtBox.Text.Contains("COM8") || input.InputBoxTxtBox.Text.Contains("COM9") || input.InputBoxTxtBox.Text.Contains("LPT1") || input.InputBoxTxtBox.Text.Contains("LPT2") || input.InputBoxTxtBox.Text.Contains("LPT3") || input.InputBoxTxtBox.Text.Contains("LPT4") || input.InputBoxTxtBox.Text.Contains("LPT5") || input.InputBoxTxtBox.Text.Contains("LPT6") || input.InputBoxTxtBox.Text.Contains("LPT7") || input.InputBoxTxtBox.Text.Contains("LPT8") || input.InputBoxTxtBox.Text.Contains("LPT9"))
            {
                MessageBox.Show("Handle Error: Invalid File Name!");
                return;
            }
            //Check if the file rename is vaild, check normal things.
            if (input.InputBoxTxtBox.Text.Contains("\\") || input.InputBoxTxtBox.Text.Contains("/") || input.InputBoxTxtBox.Text.Contains(":") || input.InputBoxTxtBox.Text.Contains("*") || input.InputBoxTxtBox.Text.Contains("?") || input.InputBoxTxtBox.Text.Contains("\"") || input.InputBoxTxtBox.Text.Contains("<") || input.InputBoxTxtBox.Text.Contains(">") || input.InputBoxTxtBox.Text.Contains("|"))
            {
                MessageBox.Show("Invalid File Name!");
                return;
            }
            File.Move(Application.StartupPath + @"\CorruptionStashList\" + StashFileList.GetItemText(StashFileList.SelectedItem), Application.StartupPath + "\\CorruptionStashList\\" + input.InputBoxTxtBox.Text + exc);
            StashFileList.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\CorruptionStashList\");
            FileInfo[] diar1 = di.GetFiles();

            // list the names of all files in the specified directory
            foreach (var dra in diar1)
                StashFileList.Items.Add(dra);
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
                    foreach (var item in StashFileList.SelectedItems)
                    {
                        File.Delete(Application.StartupPath + @"\CorruptionStashList\" + item.ToString());
                    }
                    StashFileList.Items.Clear();
                    DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\CorruptionStashList\");
                    FileInfo[] diar1 = di.GetFiles();

                    // list the names of all files in the specified directory
                    foreach (var dra in diar1)
                        StashFileList.Items.Add(dra);
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
            catch (DirectoryNotFoundException)
            {
                //Show error message
                MessageBox.Show("File not found (FileSave). Did you select an item?", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilesaveReloadbtn_Click(object sender, EventArgs e)
        {
            RefreshFileSaves();
        }

        private void FilesaveSavebtn_Click(object sender, EventArgs e)
        {
            SaveCorruptedFileCopy();
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
            //Check if the file rename is vaild, e.g do not allow CON
            if (input.InputBoxTxtBox.Text.Contains("CON") || input.InputBoxTxtBox.Text.Contains("PRN") || input.InputBoxTxtBox.Text.Contains("AUX") || input.InputBoxTxtBox.Text.Contains("NUL") || input.InputBoxTxtBox.Text.Contains("COM1") || input.InputBoxTxtBox.Text.Contains("COM2") || input.InputBoxTxtBox.Text.Contains("COM3") || input.InputBoxTxtBox.Text.Contains("COM4") || input.InputBoxTxtBox.Text.Contains("COM5") || input.InputBoxTxtBox.Text.Contains("COM6") || input.InputBoxTxtBox.Text.Contains("COM7") || input.InputBoxTxtBox.Text.Contains("COM8") || input.InputBoxTxtBox.Text.Contains("COM9") || input.InputBoxTxtBox.Text.Contains("LPT1") || input.InputBoxTxtBox.Text.Contains("LPT2") || input.InputBoxTxtBox.Text.Contains("LPT3") || input.InputBoxTxtBox.Text.Contains("LPT4") || input.InputBoxTxtBox.Text.Contains("LPT5") || input.InputBoxTxtBox.Text.Contains("LPT6") || input.InputBoxTxtBox.Text.Contains("LPT7") || input.InputBoxTxtBox.Text.Contains("LPT8") || input.InputBoxTxtBox.Text.Contains("LPT9"))
            {
                MessageBox.Show("Handle Error: Invalid File Name!");
                return;
            }
            //Check if the file rename is vaild, check normal things.
            if (input.InputBoxTxtBox.Text.Contains("\\") || input.InputBoxTxtBox.Text.Contains("/") || input.InputBoxTxtBox.Text.Contains(":") || input.InputBoxTxtBox.Text.Contains("*") || input.InputBoxTxtBox.Text.Contains("?") || input.InputBoxTxtBox.Text.Contains("\"") || input.InputBoxTxtBox.Text.Contains("<") || input.InputBoxTxtBox.Text.Contains(">") || input.InputBoxTxtBox.Text.Contains("|"))
            {
                MessageBox.Show("Invalid File Name!");
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
            //Check if a StashFile is selected before running code
            if (StashFileList.SelectedItems.Count == 0)
            {
                MessageBox.Show("No StashFile selected!", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CorruptUsingStashFile(Application.StartupPath + "\\CorruptionStashList\\" + StashFileList.GetItemText(StashFileList.SelectedItem));
        }

        public void CorruptUsingStashFile(string StashFileLocation)
        {
            //Check if ROM isn't null before running code
            if (ROM == null)
            {
                MessageBox.Show("No ROM loaded!" + Environment.NewLine + "Please load a ROM first.", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            byte[] FinROM = ROM.Clone() as byte[];
            foreach (var line in File.ReadAllLines(StashFileLocation))
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
                    MessageBox.Show($"Split error in line: {line} {Environment.NewLine} Error: {ex.Message}", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show($"Location error in line: {line} {Environment.NewLine} Error: {ex.Message}", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    result = splitStrings[3];
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Corruption error in line: {line} {Environment.NewLine} Error: {ex.Message}", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //Check if the byte within the correct margins. If not, throw an error.
                if (Convert.ToInt32(i) > FinROM.Length)
                {
                    MessageBox.Show($"Byte location is out of bounds! {Environment.NewLine}Byte location: {i}", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    FinROM[int.Parse(i.ToString())] = (byte)int.Parse(result.ToString());
                }
                catch (IndexOutOfRangeException ex1)
                {
                    MessageBox.Show($"Stash item location is invalid! {Environment.NewLine} Error: {ex1}", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ROM byte stash corruption failed! Corruption cannot continue. {Environment.NewLine} Error: {ex}", "Error - LunarROMCorruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            File.WriteAllBytes(SaveasTxt.Text, FinROM);
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
                CorruptionCore.StartEmulator(ReopenChbox.Checked, EmulatorLocationtxt.Text, SaveasTxt.Text, OverrideArgumentschbox.Checked, OverrideArguments.Text);
            }
            //Set StashByteList to the files contents, each line is each item on the list
            StashBytesList.Items.Clear();
            foreach (var item in File.ReadAllLines(StashFileLocation))
            {
                StashBytesList.Items.Add(item);
            }
        }
        
        private void EnableStashSavesChkbox_CheckedChanged(object sender, EventArgs e)
        {
            StashBytesList.Enabled = EnableStashSavesChkbox.Checked;
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
                StashFileList.SelectedIndex = StashFileList.IndexFromPoint(e.X, e.Y);
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
            //Disable Stash Saves if queue is enabled as well as file saves
            if (CorruptionQueueChkbox.Checked)
            {
                EnableStashSavesChkbox.Checked = false;
                FilesaveEnableAutoSaves.Checked = false;
            }
            //Disable the ability to change the checkboxes
            EnableStashSavesChkbox.Enabled = !CorruptionQueueChkbox.Checked;
            FilesaveEnableAutoSaves.Enabled = !CorruptionQueueChkbox.Checked;
            //Disable restore button
            Restorefilebtn.Enabled = !CorruptionQueueChkbox.Checked;
            //Disable corrupt using stash file
            Corruptusingstashbtn.Enabled = !CorruptionQueueChkbox.Checked;
            //Disable the save as button
            Changesaveasbtn.Enabled = !CorruptionQueueChkbox.Checked;
        }

        private void CorruptionQueueBtn_Click(object sender, EventArgs e)
        {
            CorruptionQueueFormSettings.ShowDialog();
        }

        private void FileSaveOpenLocationBtn_Click(object sender, EventArgs e)
        {
            //Open the folder location for file saves
            System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + @"\Saves\");
        }

        private void StartAutomationBtn_Click(object sender, EventArgs e)
        {
            //If the buttons text is "Start Automation", then start automation and change the text and color to stop and OrangeRed
            if (StartAutomationBtn.Text == "Start Automation")
            {
                StartAutomationBtn.Text = "Stop Automation";
                StartAutomationBtn.ForeColor = Color.OrangeRed;
                AutomationTimer.Start();
            }
            else
            {
                StartAutomationBtn.Text = "Start Automation";
                StartAutomationBtn.ForeColor = Color.SpringGreen;
                AutomationTimer.Stop();
            }
        }

        private void RemoveTaskBtn_Click(object sender, EventArgs e)
        {
            //Removes selected item from the automation list
            AutomationList.Items.Remove(AutomationList.SelectedItem);
        }

        private void AddTaskBtn_Click(object sender, EventArgs e)
        {
            //Adds automation task from the combo box into the automation list
            AutomationList.Items.Add(AutomationTaskComboBox.SelectedItem);
        }

        private void DelayInAutomationNUD_ValueChanged(object sender, EventArgs e)
        {
            //Converts the NumericUpDown number from seconds to miliseconds to use on the timer
            AutomationTimer.Interval = (int)DelayInAutomationNUD.Value * 1000;
        }

        private void AutomationTimer_Tick(object sender, EventArgs e)
        {
            //For each task on the automation list, execute.
            foreach (var item in AutomationList.Items)
            {
                //Switch statement: Corrupt, Randomize Intensity, Randomize Start Byte, Randomize End Byte, Randomize both Start byte and End byte
                switch (item)
                {
                    case "Run Corruption":
                        //Click Corrupt Button
                        CorruptButton.PerformClick();
                        break;
                    case "Randomize Intensity":
                        //Check if nth Intensity is enabled
                        if (CorruptnthbyteCheckbox.Checked)
                        {
                            if (AllowLargeIntensity.Checked)
                            {
                                CorrupteverynthbyteTrackbar.Value = rnd.Next(1, 99999);
                                EveryNthByte.Value = rnd.Next(1, 99999);
                            }
                            else
                            {
                                CorrupteverynthbyteTrackbar.Value = rnd.Next(1, 1000);
                                EveryNthByte.Value = rnd.Next(1, 1000);
                            }
                        }
                        else
                        {
                            //If allow large intensity is checked, make the max random larger
                            if (AllowLargeIntensity.Checked)
                            {
                                IntensityTrackbar.Value = rnd.Next(1, 99999);
                                Intensity.Value = rnd.Next(1, 99999);
                            }
                            else
                            {
                                IntensityTrackbar.Value = rnd.Next(1, 1000);
                                Intensity.Value = rnd.Next(1, 1000);
                            }
                        }

                        break;
                    case "Randomize Start Byte":
                        //Randomizes Start byte by the maximum value of the trackbar
                        StartByteTrackBar.Value = rnd.Next(1, StartByteTrackBar.Maximum);
                        StartByteNumb.Value = rnd.Next(1, StartByteTrackBar.Maximum);
                        break;
                    case "Randomize End Byte":
                        EndByteTrackbar.Value = rnd.Next(1, EndByteTrackbar.Maximum);
                        EndByteNumb.Value = rnd.Next(1, EndByteTrackbar.Maximum);
                        break;
                    case "Randomize both Start byte and End byte":
                        //Randomizes both start byte and end byte, but making sure that the start byte is not higher than the end byte

                        StartByteTrackBar.Value = rnd.Next(1, StartByteTrackBar.Maximum);
                        EndByteTrackbar.Value = rnd.Next(StartByteTrackBar.Value, EndByteTrackbar.Maximum);

                        StartByteNumb.Value = rnd.Next(1, StartByteTrackBar.Maximum);
                        EndByteNumb.Value = rnd.Next(StartByteTrackBar.Value, EndByteTrackbar.Maximum);

                        break;
                }
            }
        }

        private void MoveTaskUpBtn_Click(object sender, EventArgs e)
        {
            //Moves selected item on automation list up
            if (AutomationList.SelectedIndex > 0)
            {
                int index = AutomationList.SelectedIndex;
                object item = AutomationList.SelectedItem;
                AutomationList.Items.Remove(item);
                AutomationList.Items.Insert(index - 1, item);
                AutomationList.SelectedIndex = index - 1;
            }
        }

        private void MoveTaskDownBtn_Click(object sender, EventArgs e)
        {
            //Moves selected item on automation list down
            if (AutomationList.SelectedIndex < AutomationList.Items.Count - 1)
            {
                int index = AutomationList.SelectedIndex;
                object item = AutomationList.SelectedItem;
                AutomationList.Items.Remove(item);
                AutomationList.Items.Insert(index + 1, item);
                AutomationList.SelectedIndex = index + 1;
            }
        }
    }
}