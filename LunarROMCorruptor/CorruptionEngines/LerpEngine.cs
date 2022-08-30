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
            double interpolateVal = Convert.ToDouble(Program.Form.CorruptionEngineFrame.LerpValueTxt.Text);
            //Check if the Bytes selected in i are in range.
            try
            {
                byteminus = ROM[i + 1];
            }
            catch (IndexOutOfRangeException)
            {
                byteminus = ROM[i];
            }
            try
            {
                byteplus = ROM[i - 1];
            }
            catch (IndexOutOfRangeException)
            {
                byteplus = ROM[i];
            }
            //Check if the interpolateVal is in range, cannot be higher than 1.0 and cannot be lower than 0.0
            if (interpolateVal > 1.0)
            {
                interpolateVal = 1.0;
            }
            if (interpolateVal < 0.0)
            {
                interpolateVal = 0.0;
            }
            //Calculate the new value
            ROM[i] = (byte)LinearInterpolationCalculation(byteminus, byteplus, interpolateVal);
            Program.Form.InternalStashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
            return ROM;
        }
    }
}