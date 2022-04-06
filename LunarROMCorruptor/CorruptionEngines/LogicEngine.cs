using System;
using System.Windows.Forms;

//AND
//OR
//XOR
//NOT
//NAND
//NOR
//XNOR
//SWAP
//SHIFT

namespace LunarROMCorruptor.CorruptionEngines
{
    internal class LogicEngine
    {
        public static byte[] CorruptByte(byte[] ROM, CorruptionOptions CorruptOption, long i, int bitwiseval)
        {
            byte bitwiseval2 = Convert.ToByte(bitwiseval);
            switch (CorruptOption)
            {
                case CorruptionOptions.AND:
                    ROM[i] = (byte)(ROM[i] & bitwiseval2);
                    break;
                case CorruptionOptions.OR:

                    break;
                case CorruptionOptions.XOR:

                    break;
                case CorruptionOptions.NOT:

                    break;
                case CorruptionOptions.NAND:

                    break;
                case CorruptionOptions.NOR:
                    
                    break;
                case CorruptionOptions.XNOR:

                    break;
                case CorruptionOptions.SWAP:

                    break;
                case CorruptionOptions.SHIFT:

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
