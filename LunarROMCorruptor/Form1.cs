using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;

//MIT License

//Copyright (c) 2020 LunarHunter

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

namespace LunarROMCorruptor
{
    public partial class Form1 : Form
    {
        byte[] ROM;
        byte[] backupROM;
        byte[] ROMmerge;
        int MaxByte;
        int StartByte;
        int EndByte;
        readonly Random rnd;
        public Form1()
        {
            InitializeComponent();
        }

        static public double LinearInterpolationCalculation(double v0, double v1, double t)
        {
            return Math.Round(Math.Abs(v0 + (t * (v1 - v0))));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void LoadFile()
        {
            if (MainOpenFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                if(MultipleFilesChbx.Checked)
                {
                    FileSelectiontxt.Text = MainOpenFileDialog.FileName;
                    SaveasTxt.Text = MainOpenFileDialog.FileName;
                    MultipleFileList.Items.Add(FileSelectiontxt.Text);
                }
                else
                {
                    ROM = File.ReadAllBytes(MainOpenFileDialog.FileName);
                    FileSelectiontxt.Text = MainOpenFileDialog.FileName;
                    SaveasTxt.Text = MainOpenFileDialog.FileName;
                    MainSaveFileDialog.FileName = Path.GetDirectoryName(SaveasTxt.Text);
                    string exc = Path.GetExtension(MainOpenFileDialog.FileName);
                    SaveasTxt.Text = SaveasTxt.Text.Replace(Path.GetFileName(MainOpenFileDialog.FileName), "CorruptedFile" + exc);;
                    MaxByte = ROM.Length - 1;
                    StartByteTrackBar.Maximum = MaxByte;
                    EndByteTrackbar.Maximum = MaxByte;
                    EndByteNumb.Maximum = MaxByte;
                    EndByteNumb.Value = MaxByte;
                    StartByteNumb.Maximum = MaxByte;
                    backupROM = ROM;
                }
            }
        }

        private void Openfilebtn_Click(object sender, EventArgs e)
        {
            LoadFile();
        }
    }
}
