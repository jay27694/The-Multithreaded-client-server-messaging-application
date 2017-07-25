namespace Mul_Con_C
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnconnect = new System.Windows.Forms.Button();
            this.btnsend = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.clientName = new System.Windows.Forms.TextBox();
            this.btnConnectTo = new System.Windows.Forms.Button();
            this.lstchat = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.olusrs = new System.Windows.Forms.ListBox();
            this.status = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnconnect
            // 
            this.btnconnect.Location = new System.Drawing.Point(12, 12);
            this.btnconnect.Name = "btnconnect";
            this.btnconnect.Size = new System.Drawing.Size(303, 23);
            this.btnconnect.TabIndex = 0;
            this.btnconnect.Text = "Connect";
            this.btnconnect.UseVisualStyleBackColor = true;
            this.btnconnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnsend
            // 
            this.btnsend.Location = new System.Drawing.Point(213, 224);
            this.btnsend.Name = "btnsend";
            this.btnsend.Size = new System.Drawing.Size(102, 23);
            this.btnsend.TabIndex = 1;
            this.btnsend.Text = "Send";
            this.btnsend.UseVisualStyleBackColor = true;
            this.btnsend.Click += new System.EventHandler(this.btnsend_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 224);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(194, 20);
            this.textBox1.TabIndex = 3;
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(108, 42);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(99, 20);
            this.name.TabIndex = 4;
            // 
            // btnRegister
            // 
            this.btnRegister.Enabled = false;
            this.btnRegister.Location = new System.Drawing.Point(213, 39);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(102, 23);
            this.btnRegister.TabIndex = 5;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Connect To";
            // 
            // clientName
            // 
            this.clientName.Location = new System.Drawing.Point(107, 68);
            this.clientName.Name = "clientName";
            this.clientName.Size = new System.Drawing.Size(100, 20);
            this.clientName.TabIndex = 7;
            // 
            // btnConnectTo
            // 
            this.btnConnectTo.Enabled = false;
            this.btnConnectTo.Location = new System.Drawing.Point(213, 67);
            this.btnConnectTo.Name = "btnConnectTo";
            this.btnConnectTo.Size = new System.Drawing.Size(102, 23);
            this.btnConnectTo.TabIndex = 8;
            this.btnConnectTo.Text = "Connect To";
            this.btnConnectTo.UseVisualStyleBackColor = true;
            this.btnConnectTo.Click += new System.EventHandler(this.btnConnectTo_Click);
            // 
            // lstchat
            // 
            this.lstchat.FormattingEnabled = true;
            this.lstchat.Location = new System.Drawing.Point(13, 97);
            this.lstchat.Name = "lstchat";
            this.lstchat.Size = new System.Drawing.Size(302, 121);
            this.lstchat.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(334, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Online Users";
            // 
            // olusrs
            // 
            this.olusrs.FormattingEnabled = true;
            this.olusrs.Location = new System.Drawing.Point(326, 32);
            this.olusrs.Name = "olusrs";
            this.olusrs.Size = new System.Drawing.Size(88, 212);
            this.olusrs.TabIndex = 11;
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(13, 44);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(86, 17);
            this.status.TabIndex = 12;
            this.status.Text = "Show Status";
            this.status.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 257);
            this.Controls.Add(this.status);
            this.Controls.Add(this.olusrs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstchat);
            this.Controls.Add(this.btnConnectTo);
            this.Controls.Add(this.clientName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.name);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnsend);
            this.Controls.Add(this.btnconnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnconnect;
        private System.Windows.Forms.Button btnsend;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox clientName;
        private System.Windows.Forms.Button btnConnectTo;
        private System.Windows.Forms.ListBox lstchat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox olusrs;
        private System.Windows.Forms.CheckBox status;
    }
}

