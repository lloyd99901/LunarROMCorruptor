using System;
using System.Windows.Forms;

namespace LunarROMCorruptor.CorruptionEngines
{
    internal class LogicEngine
    {
        public static byte[] CorruptByte(byte[] ROM, CorruptionOptions CorruptOption, long i, int bitwiseval)
        {
            byte BitVal = Convert.ToByte(bitwiseval);
            switch (CorruptOption)
            {
                case CorruptionOptions.AND:
                    ROM[i] = (byte)(ROM[i] & BitVal);
                    Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                    //Console.WriteLine("[x] AND trigger");
                    break;
                case CorruptionOptions.OR:
                    ROM[i] = (byte)(ROM[i] | BitVal);
                    Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                    //Console.WriteLine("[x] OR trigger");
                    break;
                case CorruptionOptions.XOR:
                    ROM[i] = (byte)(ROM[i] ^ BitVal);
                    Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                    //Console.WriteLine("[x] XOR trigger");
                    break;
                case CorruptionOptions.NOT:
                    ROM[i] = (byte)~(ROM[i]);
                    Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                    //Console.WriteLine("[x] NOT trigger");
                    break;
                case CorruptionOptions.NAND:
                    ROM[i] = (byte)~(ROM[i] & BitVal);
                    Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                    //Console.WriteLine("[x] NAND trigger");
                    break;
                case CorruptionOptions.NOR:
                    ROM[i] = (byte)~(ROM[i] | BitVal);
                    Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                    //Console.WriteLine("[x] NOR trigger");
                    break;
                case CorruptionOptions.SWAP:
                    byte swap1 = ROM[i];
                    byte swap2;
                    //Console.WriteLine("[x] SWAP trigger");
                    try
                    {
                        swap2 = ROM[i + 1];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //Console.WriteLine("[x] Error: Index out of range.");
                        break;
                    }
                    //Console.WriteLine("[x] Passed check");
                    ROM[i] = swap2;
                    ROM[i + 1] = swap1;
                    int i1 = (int)(i + BitVal);
                    Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + swap2 + ")");
                    Program.Form.StashItems.Add("[x] File(" + i1 + ").SET(" + swap1 + ")");
                    break;
                case CorruptionOptions.SHIFT:
                    ROM[i] = ROM[i + BitVal];
                    Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                    //Console.WriteLine("[x] SHIFT trigger");
                    break;
                default:
                    if (MessageBox.Show("The Logic Engine returned a result that wasn't expected! Click yes to close this program or no to continue anyway.", "ERROR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                    break;
            }
            return ROM;
        }
    }
}
