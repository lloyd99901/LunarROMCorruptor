using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using LunarROMCorruptor.CorruptionEngines;

namespace LunarROMCorruptor.CorruptionInternals
{
    public class CorruptionCore
    {
        private static readonly Random rnd = new Random();
        public static byte ClampByte(int x) //This is to prevent the byte from going over 255 or going under 0
        {
            if (x < 0)
                return 0;
            if (x > 255)
                return 255;
            return (byte)x;
        }
        public static byte[] StartCorruption(byte[] ROM, int StartByte, int EndByte, bool CorruptNthByte, int Intensity, string CorruptionEngine)
        {
            switch (CorruptionEngine)
            {
                case "Nightmare Engine":
                    if (CorruptNthByte)
                    {
                        //CorruptNTH selected
                        int i1 = StartByte;
                        while (i1 <= EndByte)
                        {
                            Enum.TryParse(Program.Form.objForm2.NightmareComboBox.Text, out CorruptionOptions corruptiontype);
                            NightmareEngine.CorruptByte(ROM, corruptiontype, i1);
                            i1 += Intensity;
                        }
                    }
                    else //Intensity Mode
                    {
                        for (int i1 = 0; i1 <= Intensity - 1; i1++)
                        {
                            Enum.TryParse(Program.Form.objForm2.NightmareComboBox.Text, out CorruptionOptions corruptiontype);
                            NightmareEngine.CorruptByte(ROM, corruptiontype, rnd.Next(StartByte, EndByte));
                        }
                    }
                    break;

                case "Lerp Engine":
                    if (CorruptNthByte)
                    {
                        //CorruptNTH selected
                        int i1 = StartByte;
                        while (i1 <= EndByte)
                        {
                            LerpEngine.CorruptByte(ROM, i1);
                            i1 += Intensity;
                        }
                    }
                    else //Intensity Mode
                    {
                        for (int i1 = 0; i1 <= Intensity - 1; i1++)
                        {
                            LerpEngine.CorruptByte(ROM, rnd.Next(StartByte, EndByte));
                        }
                    }
                    break;

                case "Merge Engine":
                    //Check if the merge file exists, if not throw an error
                    if (string.IsNullOrEmpty(Program.Form.objForm2.MergeFileLocationTxt.Text))
                    {
                        MessageBox.Show("Merge file location is empty. Please select a file.", "Error - LunarROMCorruptor ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    byte[] ROMmerge = File.ReadAllBytes(Program.Form.objForm2.MergeFileLocationTxt.Text);
                    if (CorruptNthByte)
                    {
                        //CorruptNTH selected
                        int i1 = StartByte;
                        while (i1 <= EndByte)
                        {
                            Enum.TryParse(Program.Form.objForm2.CorrTypeMerge.Text, out CorruptionOptions corruptiontype);
                            MergeEngine.CorruptByte(ROM, ROMmerge, corruptiontype, i1);
                            i1 += Intensity;
                        }
                    }
                    else //Intensity Mode
                    {
                        for (int i1 = 0; i1 <= Intensity - 1; i1++)
                        {
                            Enum.TryParse(Program.Form.objForm2.CorrTypeMerge.Text, out CorruptionOptions corruptiontype);
                            MergeEngine.CorruptByte(ROM, ROMmerge, corruptiontype, rnd.Next(StartByte, EndByte));
                        }
                    }
                    break;

                case "Logic Engine":
                    if (CorruptNthByte)
                    {
                        //CorruptNTH selected
                        int i1 = StartByte;
                        while (i1 <= EndByte)
                        {
                            if (Program.Form.objForm2.LogicRandomizeTypeCheckbox.Checked) //Randomize selection
                            {
                                int randomIndex = rnd.Next(1, Program.Form.objForm2.BitwiseComboBox.Items.Count - 1);
                                Program.Form.objForm2.BitwiseComboBox.SelectedIndex = randomIndex;
                            }
                            if (Program.Form.objForm2.LogicRandomizeValueCheckBox.Checked) //Randomize Value
                            {
                                int randomValue = rnd.Next((int)Program.Form.objForm2.ValueBitwise.Minimum, (int)Program.Form.objForm2.ValueBitwise.Maximum);
                                Program.Form.objForm2.ValueBitwise.Value = randomValue;
                            }
                            Enum.TryParse(Program.Form.objForm2.BitwiseComboBox.Text, out CorruptionOptions corruptiontype);
                            LogicEngine.CorruptByte(ROM, corruptiontype, i1, (int)Program.Form.objForm2.ValueBitwise.Value);
                            i1 += Intensity;
                        }
                    }
                    else //Intensity Mode
                    {
                        for (int i1 = 0; i1 <= Intensity - 1; i1++)
                        {
                            if (Program.Form.objForm2.LogicRandomizeTypeCheckbox.Checked) //Randomize selection
                            {
                                int randomIndex = rnd.Next(1, Program.Form.objForm2.BitwiseComboBox.Items.Count - 1);
                                Program.Form.objForm2.BitwiseComboBox.SelectedIndex = randomIndex;
                            }
                            if (Program.Form.objForm2.LogicRandomizeValueCheckBox.Checked) //Randomize Value
                            {
                                int randomValue = rnd.Next((int)Program.Form.objForm2.ValueBitwise.Minimum, (int)Program.Form.objForm2.ValueBitwise.Maximum);
                                Program.Form.objForm2.ValueBitwise.Value = randomValue;
                            }
                            Enum.TryParse(Program.Form.objForm2.BitwiseComboBox.Text, out CorruptionOptions corruptiontype);
                            //MessageBox.Show(StartByte.ToString() + EndByte.ToString());
                            LogicEngine.CorruptByte(ROM, corruptiontype, rnd.Next(StartByte, EndByte), (int)Program.Form.objForm2.ValueBitwise.Value);
                        }
                    }
                    break;
                case "Manual":
                    if (CorruptNthByte) //CorruptNTH mode
                    {
                        int i = StartByte;
                        while (i <= EndByte)
                        {
                            ManualEngine.CorruptByte(ROM, i, StartByte, EndByte);
                            i += Intensity;
                        }
                    }
                    else //Intense mode
                    {
                        for (int i1 = 0; i1 <= Intensity - 1; i1++)
                        {
                            long i = rnd.Next(StartByte, EndByte);
                            ManualEngine.CorruptByte(ROM, i, StartByte, EndByte);
                        }
                    }
                    break;

                default:
                    if (MessageBox.Show("The Start Corruption function returned a result that wasn't expected! Click yes to close this program or no to continue anyway.", "ERROR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                    break;
            }
            return ROM;
        }
        public static void StartEmulator(bool ReopenProgram, string EmulatorLocation, bool OverrideArgumentsChk, string OverrideArguments)
        {
            try
            {
                if (ReopenProgram)
                {
                    var processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(EmulatorLocation));
                    if (processes.Length > 0)
                        processes[0].Kill();
                }
                System.Threading.Thread.Sleep(300);
                Process p = new Process();
                p.StartInfo.FileName = EmulatorLocation;
                if (OverrideArgumentsChk == false)
                    p.StartInfo.Arguments = "\"" + Program.Form.SaveasTxt.Text + "\"";
                else
                    p.StartInfo.Arguments = OverrideArguments;
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when starting process/emulator: " + ex.ToString());
            }
        }
    }
}
