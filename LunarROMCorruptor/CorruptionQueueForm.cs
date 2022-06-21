using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LunarROMCorruptor
{
    public partial class CorruptionQueueForm : Form
    {
        public CorruptionQueueForm()
        {
            InitializeComponent();
        }

        private void Openfilebtn_Click(object sender, EventArgs e)
        {

        }
        public void LoadFile(string FileLocation)
        {
            //Check if file is less than 2GB
            if (new FileInfo(FileLocation).Length >= 2147483648)
            {
                MessageBox.Show("File is too large to load.\n\nFile must be less than 2GB in size.", "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

            }
        }
    }
}
