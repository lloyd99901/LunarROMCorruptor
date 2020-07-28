namespace LunarROMCorruptor
{
    partial class StashEditor
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
            this.stashListbox = new System.Windows.Forms.ListBox();
            this.RemovedItemslstbx = new System.Windows.Forms.ListBox();
            this.valueStashnum = new System.Windows.Forms.NumericUpDown();
            this.LocationStash = new System.Windows.Forms.NumericUpDown();
            this.stashitemcmbx = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Label1 = new System.Windows.Forms.Label();
            this.restorebtn = new System.Windows.Forms.Button();
            this.newfilestashbtn = new System.Windows.Forms.Button();
            this.removeitembtn = new System.Windows.Forms.Button();
            this.additemsbtn = new System.Windows.Forms.Button();
            this.addbtn = new System.Windows.Forms.Button();
            this.Openfilebtn = new System.Windows.Forms.Button();
            this.remove50btn = new System.Windows.Forms.Button();
            this.removeselbtn = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.valueStashnum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocationStash)).BeginInit();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stashListbox
            // 
            this.stashListbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.stashListbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stashListbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stashListbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.stashListbox.FormattingEnabled = true;
            this.stashListbox.HorizontalScrollbar = true;
            this.stashListbox.IntegralHeight = false;
            this.stashListbox.Location = new System.Drawing.Point(160, 0);
            this.stashListbox.Name = "stashListbox";
            this.stashListbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.stashListbox.Size = new System.Drawing.Size(283, 470);
            this.stashListbox.TabIndex = 161;
            // 
            // RemovedItemslstbx
            // 
            this.RemovedItemslstbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.RemovedItemslstbx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.RemovedItemslstbx.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RemovedItemslstbx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RemovedItemslstbx.FormattingEnabled = true;
            this.RemovedItemslstbx.HorizontalScrollbar = true;
            this.RemovedItemslstbx.IntegralHeight = false;
            this.RemovedItemslstbx.Location = new System.Drawing.Point(12, 270);
            this.RemovedItemslstbx.Name = "RemovedItemslstbx";
            this.RemovedItemslstbx.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.RemovedItemslstbx.Size = new System.Drawing.Size(136, 114);
            this.RemovedItemslstbx.TabIndex = 160;
            // 
            // valueStashnum
            // 
            this.valueStashnum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(81)))));
            this.valueStashnum.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueStashnum.ForeColor = System.Drawing.Color.White;
            this.valueStashnum.Location = new System.Drawing.Point(70, 196);
            this.valueStashnum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.valueStashnum.Name = "valueStashnum";
            this.valueStashnum.Size = new System.Drawing.Size(78, 22);
            this.valueStashnum.TabIndex = 97;
            this.valueStashnum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // LocationStash
            // 
            this.LocationStash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(81)))));
            this.LocationStash.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationStash.ForeColor = System.Drawing.Color.White;
            this.LocationStash.Location = new System.Drawing.Point(70, 170);
            this.LocationStash.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.LocationStash.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LocationStash.Name = "LocationStash";
            this.LocationStash.Size = new System.Drawing.Size(78, 22);
            this.LocationStash.TabIndex = 97;
            this.LocationStash.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // stashitemcmbx
            // 
            this.stashitemcmbx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(64)))));
            this.stashitemcmbx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stashitemcmbx.ForeColor = System.Drawing.Color.White;
            this.stashitemcmbx.FormattingEnabled = true;
            this.stashitemcmbx.Items.AddRange(new object[] {
            "set",
            "add",
            "sub"});
            this.stashitemcmbx.Location = new System.Drawing.Point(12, 145);
            this.stashitemcmbx.Name = "stashitemcmbx";
            this.stashitemcmbx.Size = new System.Drawing.Size(136, 21);
            this.stashitemcmbx.TabIndex = 96;
            this.stashitemcmbx.Text = "set";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Label3.ForeColor = System.Drawing.Color.White;
            this.Label3.Location = new System.Drawing.Point(22, 197);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(42, 17);
            this.Label3.TabIndex = 95;
            this.Label3.Text = "Value:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Label2.ForeColor = System.Drawing.Color.White;
            this.Label2.Location = new System.Drawing.Point(9, 170);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(60, 17);
            this.Label2.TabIndex = 95;
            this.Label2.Text = "Location:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Label4.ForeColor = System.Drawing.Color.White;
            this.Label4.Location = new System.Drawing.Point(9, 250);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(101, 17);
            this.Label4.TabIndex = 95;
            this.Label4.Text = "Removed items:";
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.Filter = "All files (*.*)|*.*";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(9, 125);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(139, 17);
            this.Label1.TabIndex = 95;
            this.Label1.Text = "Create new stash item:";
            // 
            // restorebtn
            // 
            this.restorebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.restorebtn.Enabled = false;
            this.restorebtn.FlatAppearance.BorderSize = 0;
            this.restorebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restorebtn.ForeColor = System.Drawing.Color.Aqua;
            this.restorebtn.Location = new System.Drawing.Point(12, 41);
            this.restorebtn.Name = "restorebtn";
            this.restorebtn.Size = new System.Drawing.Size(136, 23);
            this.restorebtn.TabIndex = 2;
            this.restorebtn.Text = "Restore";
            this.restorebtn.UseVisualStyleBackColor = false;
            // 
            // newfilestashbtn
            // 
            this.newfilestashbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newfilestashbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.newfilestashbtn.FlatAppearance.BorderSize = 0;
            this.newfilestashbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newfilestashbtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newfilestashbtn.ForeColor = System.Drawing.Color.Aqua;
            this.newfilestashbtn.Location = new System.Drawing.Point(12, 434);
            this.newfilestashbtn.Name = "newfilestashbtn";
            this.newfilestashbtn.Size = new System.Drawing.Size(136, 23);
            this.newfilestashbtn.TabIndex = 2;
            this.newfilestashbtn.Text = "Add new file to Stash";
            this.newfilestashbtn.UseVisualStyleBackColor = false;
            this.newfilestashbtn.Click += new System.EventHandler(this.newfilestashbtn_Click);
            // 
            // removeitembtn
            // 
            this.removeitembtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeitembtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.removeitembtn.FlatAppearance.BorderSize = 0;
            this.removeitembtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeitembtn.ForeColor = System.Drawing.Color.Aqua;
            this.removeitembtn.Location = new System.Drawing.Point(87, 390);
            this.removeitembtn.Name = "removeitembtn";
            this.removeitembtn.Size = new System.Drawing.Size(61, 23);
            this.removeitembtn.TabIndex = 2;
            this.removeitembtn.Text = "Remove";
            this.removeitembtn.UseVisualStyleBackColor = false;
            this.removeitembtn.Click += new System.EventHandler(this.removeitembtn_Click);
            // 
            // additemsbtn
            // 
            this.additemsbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.additemsbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.additemsbtn.FlatAppearance.BorderSize = 0;
            this.additemsbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.additemsbtn.ForeColor = System.Drawing.Color.Aqua;
            this.additemsbtn.Location = new System.Drawing.Point(12, 390);
            this.additemsbtn.Name = "additemsbtn";
            this.additemsbtn.Size = new System.Drawing.Size(57, 23);
            this.additemsbtn.TabIndex = 2;
            this.additemsbtn.Text = "Add";
            this.additemsbtn.UseVisualStyleBackColor = false;
            this.additemsbtn.Click += new System.EventHandler(this.additemsbtn_Click);
            // 
            // addbtn
            // 
            this.addbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.addbtn.FlatAppearance.BorderSize = 0;
            this.addbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addbtn.ForeColor = System.Drawing.Color.Aqua;
            this.addbtn.Location = new System.Drawing.Point(12, 224);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(136, 23);
            this.addbtn.TabIndex = 2;
            this.addbtn.Text = "Add";
            this.addbtn.UseVisualStyleBackColor = false;
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // Openfilebtn
            // 
            this.Openfilebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.Openfilebtn.FlatAppearance.BorderSize = 0;
            this.Openfilebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Openfilebtn.ForeColor = System.Drawing.Color.Aqua;
            this.Openfilebtn.Location = new System.Drawing.Point(12, 12);
            this.Openfilebtn.Name = "Openfilebtn";
            this.Openfilebtn.Size = new System.Drawing.Size(136, 23);
            this.Openfilebtn.TabIndex = 2;
            this.Openfilebtn.Text = "Open File";
            this.Openfilebtn.UseVisualStyleBackColor = false;
            this.Openfilebtn.Click += new System.EventHandler(this.Openfilebtn_Click);
            // 
            // remove50btn
            // 
            this.remove50btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.remove50btn.FlatAppearance.BorderSize = 0;
            this.remove50btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.remove50btn.ForeColor = System.Drawing.Color.Aqua;
            this.remove50btn.Location = new System.Drawing.Point(12, 99);
            this.remove50btn.Name = "remove50btn";
            this.remove50btn.Size = new System.Drawing.Size(136, 23);
            this.remove50btn.TabIndex = 2;
            this.remove50btn.Text = "Remove 50%";
            this.remove50btn.UseVisualStyleBackColor = false;
            this.remove50btn.Click += new System.EventHandler(this.remove50btn_Click);
            // 
            // removeselbtn
            // 
            this.removeselbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.removeselbtn.FlatAppearance.BorderSize = 0;
            this.removeselbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeselbtn.ForeColor = System.Drawing.Color.Aqua;
            this.removeselbtn.Location = new System.Drawing.Point(12, 70);
            this.removeselbtn.Name = "removeselbtn";
            this.removeselbtn.Size = new System.Drawing.Size(136, 23);
            this.removeselbtn.TabIndex = 2;
            this.removeselbtn.Text = "Remove Selected";
            this.removeselbtn.UseVisualStyleBackColor = false;
            this.removeselbtn.Click += new System.EventHandler(this.removeselbtn_Click);
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.Panel1.Controls.Add(this.RemovedItemslstbx);
            this.Panel1.Controls.Add(this.valueStashnum);
            this.Panel1.Controls.Add(this.LocationStash);
            this.Panel1.Controls.Add(this.stashitemcmbx);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.restorebtn);
            this.Panel1.Controls.Add(this.newfilestashbtn);
            this.Panel1.Controls.Add(this.removeitembtn);
            this.Panel1.Controls.Add(this.additemsbtn);
            this.Panel1.Controls.Add(this.addbtn);
            this.Panel1.Controls.Add(this.Openfilebtn);
            this.Panel1.Controls.Add(this.remove50btn);
            this.Panel1.Controls.Add(this.removeselbtn);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(160, 470);
            this.Panel1.TabIndex = 160;
            // 
            // SaveFileDialog1
            // 
            this.SaveFileDialog1.Filter = "All Files *.*|*.*";
            // 
            // StashEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(443, 470);
            this.Controls.Add(this.stashListbox);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(368, 509);
            this.Name = "StashEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StashEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StashEditor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.valueStashnum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocationStash)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListBox stashListbox;
        internal System.Windows.Forms.ListBox RemovedItemslstbx;
        internal System.Windows.Forms.NumericUpDown valueStashnum;
        internal System.Windows.Forms.NumericUpDown LocationStash;
        internal System.Windows.Forms.ComboBox stashitemcmbx;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button restorebtn;
        internal System.Windows.Forms.Button newfilestashbtn;
        internal System.Windows.Forms.Button removeitembtn;
        internal System.Windows.Forms.Button additemsbtn;
        internal System.Windows.Forms.Button addbtn;
        internal System.Windows.Forms.Button Openfilebtn;
        internal System.Windows.Forms.Button remove50btn;
        internal System.Windows.Forms.Button removeselbtn;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.SaveFileDialog SaveFileDialog1;
    }
}