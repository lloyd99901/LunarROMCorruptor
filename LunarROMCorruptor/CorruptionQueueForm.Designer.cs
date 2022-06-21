namespace LunarROMCorruptor
{
    partial class CorruptionQueueForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.restorebtn = new System.Windows.Forms.Button();
            this.newfilestashbtn = new System.Windows.Forms.Button();
            this.Openfilebtn = new System.Windows.Forms.Button();
            this.remove50btn = new System.Windows.Forms.Button();
            this.removeselbtn = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.stashListbox = new System.Windows.Forms.ListBox();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Filter = "All Files *.*|*.*";
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.Panel1.Controls.Add(this.restorebtn);
            this.Panel1.Controls.Add(this.newfilestashbtn);
            this.Panel1.Controls.Add(this.Openfilebtn);
            this.Panel1.Controls.Add(this.remove50btn);
            this.Panel1.Controls.Add(this.removeselbtn);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 245);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(411, 75);
            this.Panel1.TabIndex = 162;
            // 
            // restorebtn
            // 
            this.restorebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.restorebtn.FlatAppearance.BorderSize = 0;
            this.restorebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restorebtn.ForeColor = System.Drawing.Color.Aqua;
            this.restorebtn.Location = new System.Drawing.Point(109, 12);
            this.restorebtn.Name = "restorebtn";
            this.restorebtn.Size = new System.Drawing.Size(100, 23);
            this.restorebtn.TabIndex = 2;
            this.restorebtn.Text = "Add Folder";
            this.restorebtn.UseVisualStyleBackColor = false;
            // 
            // newfilestashbtn
            // 
            this.newfilestashbtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newfilestashbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.newfilestashbtn.FlatAppearance.BorderSize = 0;
            this.newfilestashbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newfilestashbtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newfilestashbtn.ForeColor = System.Drawing.Color.MediumSpringGreen;
            this.newfilestashbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newfilestashbtn.Location = new System.Drawing.Point(12, 41);
            this.newfilestashbtn.Name = "newfilestashbtn";
            this.newfilestashbtn.Size = new System.Drawing.Size(387, 23);
            this.newfilestashbtn.TabIndex = 2;
            this.newfilestashbtn.Text = "Send files to corruptor";
            this.newfilestashbtn.UseVisualStyleBackColor = false;
            // 
            // Openfilebtn
            // 
            this.Openfilebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.Openfilebtn.FlatAppearance.BorderSize = 0;
            this.Openfilebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Openfilebtn.ForeColor = System.Drawing.Color.Aqua;
            this.Openfilebtn.Location = new System.Drawing.Point(12, 12);
            this.Openfilebtn.Name = "Openfilebtn";
            this.Openfilebtn.Size = new System.Drawing.Size(91, 23);
            this.Openfilebtn.TabIndex = 2;
            this.Openfilebtn.Text = "Add File(s)";
            this.Openfilebtn.UseVisualStyleBackColor = false;
            this.Openfilebtn.Click += new System.EventHandler(this.Openfilebtn_Click);
            // 
            // remove50btn
            // 
            this.remove50btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.remove50btn.FlatAppearance.BorderSize = 0;
            this.remove50btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.remove50btn.ForeColor = System.Drawing.Color.Aqua;
            this.remove50btn.Location = new System.Drawing.Point(322, 12);
            this.remove50btn.Name = "remove50btn";
            this.remove50btn.Size = new System.Drawing.Size(77, 23);
            this.remove50btn.TabIndex = 2;
            this.remove50btn.Text = "Clear All";
            this.remove50btn.UseVisualStyleBackColor = false;
            // 
            // removeselbtn
            // 
            this.removeselbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.removeselbtn.FlatAppearance.BorderSize = 0;
            this.removeselbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeselbtn.ForeColor = System.Drawing.Color.Aqua;
            this.removeselbtn.Location = new System.Drawing.Point(215, 12);
            this.removeselbtn.Name = "removeselbtn";
            this.removeselbtn.Size = new System.Drawing.Size(101, 23);
            this.removeselbtn.TabIndex = 2;
            this.removeselbtn.Text = "Remove Selected";
            this.removeselbtn.UseVisualStyleBackColor = false;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Filter = "All files (*.*)|*.*";
            // 
            // stashListbox
            // 
            this.stashListbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stashListbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.stashListbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stashListbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.stashListbox.FormattingEnabled = true;
            this.stashListbox.HorizontalScrollbar = true;
            this.stashListbox.IntegralHeight = false;
            this.stashListbox.Location = new System.Drawing.Point(12, 12);
            this.stashListbox.Name = "stashListbox";
            this.stashListbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.stashListbox.Size = new System.Drawing.Size(387, 227);
            this.stashListbox.TabIndex = 163;
            // 
            // CorruptionQueueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(411, 320);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.stashListbox);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(427, 359);
            this.Name = "CorruptionQueueForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Corruption Queue - Edit";
            this.Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.SaveFileDialog SaveFileDialog;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button restorebtn;
        internal System.Windows.Forms.Button newfilestashbtn;
        internal System.Windows.Forms.Button Openfilebtn;
        internal System.Windows.Forms.Button remove50btn;
        internal System.Windows.Forms.Button removeselbtn;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog;
        internal System.Windows.Forms.ListBox stashListbox;
    }
}