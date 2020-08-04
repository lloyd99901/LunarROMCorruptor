using System;
using System.Windows.Forms;

namespace LunarROMCorruptor.CorruptionEngines
{
    internal class MergeEngine
    {
        private static readonly Random rnd = new Random();

        public static byte[] CorruptByte(byte[] ROM, byte[] ROMMerge, CorruptionOptions CorruptionOption, long i)
        {
            if (Program.Form.objForm2.ReplaceByteWithSamePos.Checked)
            {
                if (Program.Form.objForm2.Mod256MergeEnginechkbox.Checked)
                {
                    switch (CorruptionOption)
                    {
                        case CorruptionOptions.NONE:
                            ROM[i] = (byte)(ROMMerge[i] % Byte.MaxValue + 1);
                            Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                            break;

                        case CorruptionOptions.RANGE:
                            ROM[i] = (byte)Math.Abs(ROM[i] - ROMMerge[i] % byte.MaxValue + 1);
                            Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                            break;

                        default:
                            MessageBox.Show("Merge Engine returned default. This should not happen.");
                            break;
                    }
                }
                else
                {
                    switch (CorruptionOption)
                    {
                        case CorruptionOptions.NONE:
                            ROM[i] = ROMMerge[i];
                            Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                            break;

                        case CorruptionOptions.RANGE:
                            ROM[i] = (byte)Math.Abs(ROM[i] - ROMMerge[i]);
                            Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                            break;

                        default:
                            MessageBox.Show("Merge Engine returned default. This should not happen.");
                            break;
                    }
                }
            }
            else
            {
                if (Program.Form.objForm2.Mod256MergeEnginechkbox.Checked)
                {
                    switch (CorruptionOption)
                    {
                        case CorruptionOptions.NONE:
                            ROM[i] = (byte)(ROMMerge[rnd.Next(0, ROMMerge.Length)] % Byte.MaxValue + 1);
                            Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                            break;

                        case CorruptionOptions.RANGE:
                            ROM[i] = (byte)Math.Abs(ROM[rnd.Next(0, ROMMerge.Length)] - ROMMerge[i] % byte.MaxValue + 1);
                            Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                            break;

                        default:
                            MessageBox.Show("Merge Engine returned default. This should not happen.");
                            break;
                    }
                }
                else
                {
                    switch (CorruptionOption)
                    {
                        case CorruptionOptions.NONE:
                            ROM[i] = ROMMerge[rnd.Next(0, ROMMerge.Length)];
                            Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                            break;

                        case CorruptionOptions.RANGE:
                            ROM[i] = (byte)Math.Abs(ROM[i] - ROMMerge[rnd.Next(0, ROMMerge.Length)]);
                            Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                            break;

                        default:
                            MessageBox.Show("Merge Engine returned default. This should not happen.");
                            break;
                    }
                }
            }

            return ROM;
        }
    }
}