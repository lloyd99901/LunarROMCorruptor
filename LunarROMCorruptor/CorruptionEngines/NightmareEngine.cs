﻿using LunarROMCorruptor.CorruptionInternals;
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
                    Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                    break;

                case CorruptionOptions.RANDOMTILT:
                    switch (rnd.Next(0, 3))
                    {
                        case 0:
                            ROM[i] = (byte)((byte)rnd.Next(0, 256) % (Byte.MaxValue + 1));
                            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                            break;

                        case 1:
                            ROM[i] = CorruptionCore.ClampByte(ROM[i] + (int)Program.Form.CorruptionEngineFrame.IncreDecrenumbnightmare.Value);
                            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                            break;

                        case 2:
                            ROM[i] = CorruptionCore.ClampByte(ROM[i] - (int)Program.Form.CorruptionEngineFrame.IncreDecrenumbnightmare.Value);
                            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                            break;

                        default:
                            if (MessageBox.Show("The Nightmare Engine returned a result that wasn't expected! Click yes to close this program or no to continue anyway.", "ERROR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Application.Exit();
                            }
                            break;
                    }
                    break;

                case CorruptionOptions.TILT:
                    switch (rnd.Next(0, 2))
                    {
                        case 0:
                            ROM[i] = CorruptionCore.ClampByte(ROM[i] + (int)Program.Form.CorruptionEngineFrame.IncreDecrenumbnightmare.Value);
                            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                            break;

                        case 1:
                            ROM[i] = CorruptionCore.ClampByte(ROM[i] - (int)Program.Form.CorruptionEngineFrame.IncreDecrenumbnightmare.Value);
                            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                            break;
                    }
                    break;

                default:
                    if (MessageBox.Show("The Nightmare Engine returned a result that wasn't expected! Click yes to close this program or no to continue anyway.", "ERROR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                    break;
            }
            return ROM;
        }
    }
}