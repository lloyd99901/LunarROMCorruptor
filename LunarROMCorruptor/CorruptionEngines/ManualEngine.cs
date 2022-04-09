using System;
using System.Collections.Generic;

namespace LunarROMCorruptor.CorruptionEngines
{
    internal class ManualEngine
    {
        private static readonly Random rnd = new Random();
        private static readonly List<byte> list = new List<byte>();

        static byte ClampByte(int x) //This is to prevent the byte from going over 255 or going under 0
        {
            if (x < 0)
                return 0;
            if (x > 255)
                return 255;
            return (byte)x;
        }

        public static byte[] CorruptByte(byte[] ROM, long i, int StartByte, int EndByte)
        {
            if (Program.Form.IncrementCHECK.Checked)
            {
                ROM[i] = ClampByte(ROM[i] + (int)Program.Form.objForm2.IncreDecrenumbnightmare.Value);
                Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
            }
            if (Program.Form.SHIFTBYTECHECK.Checked)
            {
                long j = (long)(i + Program.Form.ShiftNumericUpDown.Value);
                if (j >= StartByte && j <= EndByte)
                {
                    ROM[j] = ROM[i];
                    Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                }
            }
            if (Program.Form.MakeBitEqualCHECK.Checked)
            {
                ROM[i] = (byte)Program.Form.ByteEqualNumericUpDown.Value;
                Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
            }
            if (Program.Form.ReplaceCHECK.Checked)
            {
                if (ROM[i] == (byte)Program.Form.ReplaceNumericUpDown1.Value)
                {
                    ROM[i] = (byte)Program.Form.ReplaceNumericUpDown2.Value;
                    Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                }
            }
            if (Program.Form.PasterandombitCHECK.Checked)
            {
                byte copy = ROM[rnd.Next(StartByte, EndByte)];
                ROM[i] = copy;
                Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + copy + ")");
            }
            if (Program.Form.RepeatRandomBitCHECK.Checked)
            {
                list.Clear();
                for (int i2 = 0; i2 <= Program.Form.RepeatNumericUpDown.Value - 1; i2++)
                    list.Add(ROM[i + i2]);
                // ListBox3.Items.Add(String.Join(" ", list))
                foreach (var itemcon in list)
                {
                    long final = rnd.Next(StartByte, EndByte);
                    ROM[final] = itemcon;
                    Program.Form.StashItems.Add("[x] File(" + final + ").SET(" + itemcon + ")");
                }
            }
            if (Program.Form.MULTIORDIVIDECHeck.Checked)
            {
                if (Program.Form.MultiRadio.Checked)
                {
                    ROM[i] = ClampByte(ROM[i] * (int)Program.Form.MathOperationNumericUpDown.Value);
                    Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                }
                if (Program.Form.DivideRadio.Checked)
                {
                    ROM[i] = ClampByte(ROM[i] / (int)Program.Form.MathOperationNumericUpDown.Value);
                    Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                }
                if (Program.Form.DoubleCheck.Checked)
                { 
                    ROM[i] = (byte)((byte)Math.Pow(ROM[i], (double)Program.Form.MathOperationNumericUpDown.Value) % (byte.MaxValue + 1));
                    Program.Form.StashItems.Add("[x] File(" + i + ").SET(" + ROM[i] + ")");
                }
            }
            return ROM;
        }
    }
}