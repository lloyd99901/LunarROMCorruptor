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
            this.AddFolderBTN = new System.Windows.Forms.Button();
            this.SendFilestoCorruptorBTN = new System.Windows.Forms.Button();
            this.Openfilebtn = new System.Windows.Forms.Button();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.removeselbtn = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.CorruptionQueueList = new System.Windows.Forms.ListBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.DragandDropICON = new System.Windows.Forms.PictureBox();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DragandDropICON)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Filter = "All Files *.*|*.*";
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.Panel1.Controls.Add(this.AddFolderBTN);
            this.Panel1.Controls.Add(this.SendFilestoCorruptorBTN);
            this.Panel1.Controls.Add(this.Openfilebtn);
            this.Panel1.Controls.Add(this.ClearBtn);
            this.Panel1.Controls.Add(this.removeselbtn);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 247);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(475, 75);
            this.Panel1.TabIndex = 162;
            // 
            // AddFolderBTN
            // 
            this.AddFolderBTN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.AddFolderBTN.FlatAppearance.BorderSize = 0;
            this.AddFolderBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddFolderBTN.ForeColor = System.Drawing.Color.Aqua;
            this.AddFolderBTN.Location = new System.Drawing.Point(109, 12);
            this.AddFolderBTN.Name = "AddFolderBTN";
            this.AddFolderBTN.Size = new System.Drawing.Size(100, 23);
            this.AddFolderBTN.TabIndex = 2;
            this.AddFolderBTN.Text = "Add Folder";
            this.AddFolderBTN.UseVisualStyleBackColor = false;
            this.AddFolderBTN.Click += new System.EventHandler(this.AddFolderBTN_Click);
            // 
            // SendFilestoCorruptorBTN
            // 
            this.SendFilestoCorruptorBTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SendFilestoCorruptorBTN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.SendFilestoCorruptorBTN.FlatAppearance.BorderSize = 0;
            this.SendFilestoCorruptorBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendFilestoCorruptorBTN.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendFilestoCorruptorBTN.ForeColor = System.Drawing.Color.MediumSpringGreen;
            this.SendFilestoCorruptorBTN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SendFilestoCorruptorBTN.Location = new System.Drawing.Point(12, 41);
            this.SendFilestoCorruptorBTN.Name = "SendFilestoCorruptorBTN";
            this.SendFilestoCorruptorBTN.Size = new System.Drawing.Size(448, 23);
            this.SendFilestoCorruptorBTN.TabIndex = 2;
            this.SendFilestoCorruptorBTN.Text = "Send files to corruptor";
            this.SendFilestoCorruptorBTN.UseVisualStyleBackColor = false;
            this.SendFilestoCorruptorBTN.Click += new System.EventHandler(this.SendFilestoCorruptorBTN_Click);
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
            // ClearBtn
            // 
            this.ClearBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.ClearBtn.FlatAppearance.BorderSize = 0;
            this.ClearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearBtn.ForeColor = System.Drawing.Color.Aqua;
            this.ClearBtn.Location = new System.Drawing.Point(322, 12);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(77, 23);
            this.ClearBtn.TabIndex = 2;
            this.ClearBtn.Text = "Clear All";
            this.ClearBtn.UseVisualStyleBackColor = false;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
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
            this.removeselbtn.Click += new System.EventHandler(this.removeselbtn_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Filter = "All files (*.*)|*.*";
            // 
            // CorruptionQueueList
            // 
            this.CorruptionQueueList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorruptionQueueList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.CorruptionQueueList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CorruptionQueueList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CorruptionQueueList.FormattingEnabled = true;
            this.CorruptionQueueList.HorizontalScrollbar = true;
            this.CorruptionQueueList.IntegralHeight = false;
            this.CorruptionQueueList.Location = new System.Drawing.Point(12, 12);
            this.CorruptionQueueList.Name = "CorruptionQueueList";
            this.CorruptionQueueList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.CorruptionQueueList.Size = new System.Drawing.Size(451, 229);
            this.CorruptionQueueList.TabIndex = 163;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select a folder to corrupt the files of.";
            // 
            // DragandDropICON
            // 
            this.DragandDropICON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.DragandDropICON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DragandDropICON.Image = global::LunarROMCorruptor.Properties.Resources.dragicon;
            this.DragandDropICON.Location = new System.Drawing.Point(0, 0);
            this.DragandDropICON.Name = "DragandDropICON";
            this.DragandDropICON.Size = new System.Drawing.Size(475, 322);
            this.DragandDropICON.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.DragandDropICON.TabIndex = 165;
            this.DragandDropICON.TabStop = false;
            this.DragandDropICON.Visible = false;
            // 
            // CorruptionQueueForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(475, 322);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.CorruptionQueueList);
            this.Controls.Add(this.DragandDropICON);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(427, 359);
            this.Name = "CorruptionQueueForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Corruption Queue - Edit";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.CorruptionQueueForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.CorruptionQueueForm_DragEnter);
            this.DragLeave += new System.EventHandler(this.CorruptionQueueForm_DragLeave);
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DragandDropICON)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.SaveFileDialog SaveFileDialog;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button AddFolderBTN;
        internal System.Windows.Forms.Button SendFilestoCorruptorBTN;
        internal System.Windows.Forms.Button Openfilebtn;
        internal System.Windows.Forms.Button ClearBtn;
        internal System.Windows.Forms.Button removeselbtn;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        public System.Windows.Forms.ListBox CorruptionQueueList;
        private System.Windows.Forms.PictureBox DragandDropICON;
    }
}