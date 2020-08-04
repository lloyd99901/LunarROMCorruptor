using System;
using System.Windows.Forms;

namespace LunarROMCorruptor.CorruptionEngines
{
    internal class NightmareEngine
    {
        private static readonly Random rnd = new Random();

        public static byte[] CorruptByte(byte[] ROM, CorruptionOptions CorruptOption, long i)
        {
            switch (CorruptOption)
            {
                case CorruptionOptions.RANDOM:
                    ROM[i] = (byte)rnd.Next(0, 256);
                    Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                    break;

                case CorruptionOptions.RANDOMTILT:
                    switch (rnd.Next(0, 3))
                    {
                        case 0:
                            ROM[i] = (byte)((byte)rnd.Next(0, 256) % (Byte.MaxValue + 1));
                            Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                            break;

                        case 1:
                            int NewValue = (int)((ROM[i] + Program.Form.objForm2.IncreDecrenumbnightmare.Value) % (byte.MaxValue + 1));
                            ROM[i] = (byte)Math.Abs(NewValue);
                            Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + Program.Form.objForm2.IncreDecrenumbnightmare.Value + ")");
                            break;

                        case 2:

                            NewValue = (int)((ROM[i] - Program.Form.objForm2.IncreDecrenumbnightmare.Value) % (byte.MaxValue + 1));
                            ROM[i] = (byte)Math.Abs(NewValue);
                            Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + Program.Form.objForm2.IncreDecrenumbnightmare.Value + ")");
                            break;

                        default:
                            MessageBox.Show("Default case hit on Nightmare Engine!");
                            break;
                    }
                    break;

                case CorruptionOptions.TILT:
                    switch (rnd.Next(0, 2))
                    {
                        case 0:
                            int NewValue = (int)((ROM[i] + Program.Form.objForm2.IncreDecrenumbnightmare.Value) % (byte.MaxValue + 1));
                            ROM[i] = (byte)Math.Abs(NewValue);
                            Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + Program.Form.objForm2.IncreDecrenumbnightmare.Value + ")");
                            break;

                        case 1:
                            NewValue = (int)((ROM[i] + Program.Form.objForm2.IncreDecrenumbnightmare.Value) % (byte.MaxValue + 1));
                            ROM[i] = (byte)Math.Abs(NewValue);
                            Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + Program.Form.objForm2.IncreDecrenumbnightmare.Value + ")");
                            break;
                    }
                    break;

                default:
                    MessageBox.Show("Nightmare Engine returned default. This should not happen.");
                    break;
            }
            return ROM;
        }
    }
}