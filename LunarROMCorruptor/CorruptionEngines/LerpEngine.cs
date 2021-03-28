using System;

namespace LunarROMCorruptor.CorruptionEngines
{
    internal class LerpEngine
    {
        static public double LinearInterpolationCalculation(double v0, double v1, double t)
        {
            return Math.Round(Math.Abs(v0 + (t * (v1 - v0))));
        }

        public static byte[] CorruptByte(byte[] ROM, long i)
        {
            ROM[i] = (byte)LinearInterpolationCalculation(ROM[i - 1], ROM[i + 1], Convert.ToDouble(Program.Form.objForm2.TextBox1.Text));
            Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
            return ROM;
        }
    }
}