using System;
using System.Windows.Forms;

namespace LunarROMCorruptor
{
    internal static class Program
    {
        public static Form1 Form;

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form = new Form1();
            Application.Run(Form);
        }
    }
}