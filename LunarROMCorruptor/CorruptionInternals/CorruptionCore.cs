using LunarROMCorruptor.CorruptionEngines;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace LunarROMCorruptor.CorruptionInternals
{
    public class CorruptionCore
    {
        private static readonly Random rnd = new Random();

        //Read 4 bytes from the ROM at the specified offset (32bit) and return them as a 32bit hex value
        public static uint Read32(byte[] rom, uint offset)
        {
            try
            {
                uint value = 0;
                value |= rom[offset + 0];
                value |= (uint)rom[offset + 1] << 8;
                value |= (uint)rom[offset + 2] << 16;
                value |= (uint)rom[offset + 3] << 24;
                return value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return 0;
            }

        }

        //convert value to hex ("0x00000000")
        public static string ToHex(uint value)
        {
            return "0x" + value.ToString("X8");
        }

        //Read all bytes from the ROM and return them as a 32bit hex array

        //public static uint[] ReadAll32(byte[] rom)
        //{
        //    uint[] values = new uint[rom.Length / 4];
        //    for (int i = 0; i < rom.Length; i += 4)
        //    {
        //        //check if we are at the end of the ROM
        //        if (i + 4 > rom.Length)
        //        {
        //            return values;
        //        }
        //        try
        //        {
        //            values[i / 4] = Read32(rom, (uint)i);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //            return null;
        //        }
        //    }
        //    return values;
        //}

        //Code from Form1.cs, this is how you use the corruption to hex function.
        ////Reall all of the ROM data, convert to 32 bit hex and store it in a string array
        //string[] HexData = new string[ROM.Length / 4];
        //for (int i = 0; i < ROM.Length / 4; i++)
        //{
        //    //check if i doens't exceed the array size of the ROM
        //    if (i * 4 < ROM.Length)
        //    {
        //        HexData[i] = CorruptionCore.ToHex(CorruptionCore.Read32(ROM, (uint)i * 4));
        //    }
        //    else
        //    {
        //        break;
        //    }
        //}
        //CorruptionEngineFrame.FloatBitOutput.Text = string.Join(Environment.NewLine, HexData);
        ////print how many lines there are in the textbox in the console
        //Console.WriteLine(CorruptionEngineFrame.FloatBitOutput.Lines.Length);
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
                            Enum.TryParse(Program.Form.CorruptionEngineFrame.NightmareComboBox.Text, out CorruptionOptions corruptiontype);
                            NightmareEngine.CorruptByte(ROM, corruptiontype, i1);
                            i1 += Intensity;
                        }
                    }
                    else //Intensity Mode
                    {
                        for (int i1 = 0; i1 <= Intensity - 1; i1++)
                        {
                            Enum.TryParse(Program.Form.CorruptionEngineFrame.NightmareComboBox.Text, out CorruptionOptions corruptiontype);
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
                    if (string.IsNullOrEmpty(Program.Form.CorruptionEngineFrame.MergeFileLocationTxt.Text))
                    {
                        MessageBox.Show("Merge file location is empty. Please select a file.", "Error - LunarROMCorruptor ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    byte[] ROMmerge = File.ReadAllBytes(Program.Form.CorruptionEngineFrame.MergeFileLocationTxt.Text);
                    if (CorruptNthByte)
                    {
                        //CorruptNTH selected
                        int i1 = StartByte;
                        while (i1 <= EndByte)
                        {
                            Enum.TryParse(Program.Form.CorruptionEngineFrame.CorrTypeMerge.Text, out CorruptionOptions corruptiontype);
                            MergeEngine.CorruptByte(ROM, ROMmerge, corruptiontype, i1);
                            i1 += Intensity;
                        }
                    }
                    else //Intensity Mode
                    {
                        for (int i1 = 0; i1 <= Intensity - 1; i1++)
                        {
                            Enum.TryParse(Program.Form.CorruptionEngineFrame.CorrTypeMerge.Text, out CorruptionOptions corruptiontype);
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
                            if (Program.Form.CorruptionEngineFrame.LogicRandomizeTypeCheckbox.Checked) //Randomize selection
                            {
                                int randomIndex = rnd.Next(1, Program.Form.CorruptionEngineFrame.BitwiseComboBox.Items.Count - 1);
                                Program.Form.CorruptionEngineFrame.BitwiseComboBox.SelectedIndex = randomIndex;
                            }
                            if (Program.Form.CorruptionEngineFrame.LogicRandomizeValueCheckBox.Checked) //Randomize Value
                            {
                                int randomValue = rnd.Next((int)Program.Form.CorruptionEngineFrame.ValueBitwise.Minimum, (int)Program.Form.CorruptionEngineFrame.ValueBitwise.Maximum);
                                Program.Form.CorruptionEngineFrame.ValueBitwise.Value = randomValue;
                            }
                            Enum.TryParse(Program.Form.CorruptionEngineFrame.BitwiseComboBox.Text, out CorruptionOptions corruptiontype);
                            LogicEngine.CorruptByte(ROM, corruptiontype, i1, (int)Program.Form.CorruptionEngineFrame.ValueBitwise.Value);
                            i1 += Intensity;
                        }
                    }
                    else //Intensity Mode
                    {
                        for (int i1 = 0; i1 <= Intensity - 1; i1++)
                        {
                            if (Program.Form.CorruptionEngineFrame.LogicRandomizeTypeCheckbox.Checked) //Randomize selection
                            {
                                int randomIndex = rnd.Next(1, Program.Form.CorruptionEngineFrame.BitwiseComboBox.Items.Count - 1);
                                Program.Form.CorruptionEngineFrame.BitwiseComboBox.SelectedIndex = randomIndex;
                            }
                            if (Program.Form.CorruptionEngineFrame.LogicRandomizeValueCheckBox.Checked) //Randomize Value
                            {
                                int randomValue = rnd.Next((int)Program.Form.CorruptionEngineFrame.ValueBitwise.Minimum, (int)Program.Form.CorruptionEngineFrame.ValueBitwise.Maximum);
                                Program.Form.CorruptionEngineFrame.ValueBitwise.Value = randomValue;
                            }
                            Enum.TryParse(Program.Form.CorruptionEngineFrame.BitwiseComboBox.Text, out CorruptionOptions corruptiontype);
                            //MessageBox.Show(StartByte.ToString() + EndByte.ToString());
                            LogicEngine.CorruptByte(ROM, corruptiontype, rnd.Next(StartByte, EndByte), (int)Program.Form.CorruptionEngineFrame.ValueBitwise.Value);
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
                    if (MessageBox.Show("The main corruption core function returned a result that wasn't expected! Click yes to close this program (Recommended) or no to continue anyway. (The corrupted ROM HASN'T been saved to the file yet)", "ERROR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                    break;
            }
            return ROM;
        }
        public static void StartEmulator(bool ReopenProgram, string EmulatorLocation, string FileLocation, bool OverrideArgumentsChk, string OverrideArguments)
        {
            try
            {
                if (ReopenProgram)
                {
                    var processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(EmulatorLocation));
                    if (processes.Length > 0)
                        processes[0].Kill();
                }
                //System.Threading.Thread.Sleep(300);
                Process p = new Process();
                p.StartInfo.FileName = EmulatorLocation;
                if (OverrideArgumentsChk == false)
                    p.StartInfo.Arguments = "\"" + FileLocation + "\"";
                else
                    p.StartInfo.Arguments = OverrideArguments;
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when starting process/emulator: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
