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
            byte byteminus;
            byte byteplus;
            //Check if the Bytes selected in i are in range.

            try
            {
                byteminus = ROM[i+1];
            }
            catch (IndexOutOfRangeException)
            {
                byteminus = ROM[i];
            }
            try
            {
                byteplus = ROM[i-1];
            }
            catch (IndexOutOfRangeException)
            {
                byteplus = ROM[i];
            }
            ROM[i] = (byte)LinearInterpolationCalculation(byteminus, byteplus, Convert.ToDouble(Program.Form.objForm2.LerpValueTxt.Text));
            Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
            return ROM;
        }
    }
}