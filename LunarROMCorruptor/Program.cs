using System;
using System.Windows.Forms;

namespace LunarROMCorruptor
{
    internal static class Program
    {
        public static MainCorruptionForm Form;

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form = new MainCorruptionForm();
            Application.Run(Form);
        }
    }
}