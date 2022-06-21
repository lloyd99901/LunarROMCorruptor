using System;
using System.Windows.Forms;

namespace LunarROMCorruptor.CorruptionEngines
{
    internal class MergeEngine
    {
        private static readonly Random rnd = new Random();

        public static byte[] CorruptByte(byte[] ROM, byte[] ROMMerge, CorruptionOptions CorruptionOption, long i)
        {
            if (Program.Form.CorruptionEngineFrame.ReplaceByteWithSamePos.Checked)
            {
                if (Program.Form.CorruptionEngineFrame.Mod256MergeEnginechkbox.Checked)
                {
                    switch (CorruptionOption)
                    {
                        case CorruptionOptions.NONE:
                            ROM[i] = (byte)(ROMMerge[i] % Byte.MaxValue + 1);
                            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                            break;

                        case CorruptionOptions.RANGE:
                            ROM[i] = (byte)Math.Abs(ROM[i] - ROMMerge[i] % byte.MaxValue + 1);
                            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                            break;

                        default:
                            if (MessageBox.Show("The Merge Engine returned a result that wasn't expected! Click yes to close this program or no to continue anyway.", "ERROR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Application.Exit();
                            }
                            break;
                    }
                }
                else
                {
                    switch (CorruptionOption)
                    {
                        case CorruptionOptions.NONE:
                            ROM[i] = ROMMerge[i];
                            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                            break;

                        case CorruptionOptions.RANGE:
                            ROM[i] = (byte)Math.Abs(ROM[i] - ROMMerge[i]);
                            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                            break;

                        default:
                            if (MessageBox.Show("The Merge Engine returned a result that wasn't expected! Click yes to close this program or no to continue anyway.", "ERROR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Application.Exit();
                            }
                            break;
                    }
                }
            }
            else
            {
                if (Program.Form.CorruptionEngineFrame.Mod256MergeEnginechkbox.Checked)
                {
                    switch (CorruptionOption)
                    {
                        case CorruptionOptions.NONE:
                            ROM[i] = (byte)(ROMMerge[rnd.Next(0, ROMMerge.Length)] % Byte.MaxValue + 1);
                            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                            break;

                        case CorruptionOptions.RANGE:
                            ROM[i] = (byte)Math.Abs(ROM[rnd.Next(0, ROMMerge.Length)] - ROMMerge[i] % byte.MaxValue + 1);
                            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                            break;

                        default:
                            if (MessageBox.Show("The Merge Engine returned a result that wasn't expected! Click yes to close this program or no to continue anyway.", "ERROR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Application.Exit();
                            }
                            break;
                    }
                }
                else
                {
                    switch (CorruptionOption)
                    {
                        case CorruptionOptions.NONE:
                            ROM[i] = ROMMerge[rnd.Next(0, ROMMerge.Length)];
                            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                            break;

                        case CorruptionOptions.RANGE:
                            ROM[i] = (byte)Math.Abs(ROM[i] - ROMMerge[rnd.Next(0, ROMMerge.Length)]);
                            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                            break;

                        default:
                            if (MessageBox.Show("The Merge Engine returned a result that wasn't expected! Click yes to close this program or no to continue anyway.", "ERROR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Application.Exit();
                            }
                            break;
                    }
                }
            }
            return ROM;
        }
    }
}