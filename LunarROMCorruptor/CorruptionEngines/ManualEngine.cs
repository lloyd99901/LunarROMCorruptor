using System;
using System.Collections.Generic;

namespace LunarROMCorruptor.CorruptionEngines
{
    internal class ManualEngine
    {
        private static readonly Random rnd = new Random();
        private static readonly List<byte> list = new List<byte>();

        public static byte[] CorruptByte(byte[] ROM, long i, int StartByte, int EndByte)
        {
            if (Program.Form.IncrementCHECK.Checked)
            {
                int NewValue = (int)((ROM[i] + Program.Form.NumericUpDown4.Value) % (byte.MaxValue + 1));
                ROM[i] = ((byte)NewValue);
                Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
            }
            if (Program.Form.SHIFTBYTECHECK.Checked)
            {
                long j = (long)(i + Program.Form.NumericUpDown7.Value);
                if (j >= StartByte && j <= EndByte)
                {
                    ROM[j] = ROM[i];
                    Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                }
            }
            if (Program.Form.MakeBitEqualCHECK.Checked)
            {
                int NewValue = (int)Program.Form.NumericUpDown8.Value;
                ROM[i] = (byte)NewValue;
                Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + NewValue + ")");
            }
            if (Program.Form.ReplaceCHECK.Checked)
            {
                if (ROM[i] == (byte)Program.Form.NumericUpDown9.Value)
                {
                    ROM[i] = (byte)Program.Form.NumericUpDown10.Value;
                    Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                }
            }
            if (Program.Form.PasterandombitCHECK.Checked)
            {
                byte copy = ROM[rnd.Next(StartByte, EndByte)];
                ROM[i] = copy;
                Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + copy + ")");
            }
            if (Program.Form.RepeatRandomBitCHECK.Checked)
            {
                list.Clear();
                for (int i2 = 0; i2 <= Program.Form.NumericUpDown2.Value - 1; i2++)
                    list.Add(ROM[i + i2]);
                // ListBox3.Items.Add(String.Join(" ", list))
                foreach (var itemcon in list)
                {
                    long final = rnd.Next(StartByte, EndByte);
                    ROM[final] = itemcon;
                    Program.Form.StashItems.Add("L: FILE(" + final + ").set(" + itemcon + ")");
                }
            }
            if (Program.Form.MULTIORDIVIDECHeck.Checked)
            {
                if (Program.Form.MultiRadio.Checked)
                {
                    ROM[i] = (byte)((byte)(ROM[i] * Program.Form.MULTIORDIVIDENUMBER.Value) % (byte.MaxValue + 1));
                    Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                }
                if (Program.Form.DivideRadio.Checked)
                {
                    ROM[i] = (byte)((byte)(ROM[i] / Program.Form.MULTIORDIVIDENUMBER.Value) % (byte.MaxValue + 1));
                    Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                }
                if (Program.Form.DoubleCheck.Checked)
                {
                    ROM[i] = (byte)((byte)(Math.Pow(ROM[i], (double)Program.Form.MULTIORDIVIDENUMBER.Value)) % (byte.MaxValue + 1));
                    Program.Form.StashItems.Add("L: FILE(" + i + ").set(" + ROM[i] + ")");
                }
            }
            return ROM;
        }
    }
}