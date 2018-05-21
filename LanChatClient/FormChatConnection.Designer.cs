namespace LanChatClient
{
    partial class FormChatConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChatConnection));
            this.txtServer = new System.Windows.Forms.TextBox();
            this.gbxServer = new System.Windows.Forms.GroupBox();
            this.gbxUsername = new System.Windows.Forms.GroupBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.gbxServer.SuspendLayout();
            this.gbxUsername.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(6, 19);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(248, 20);
            this.txtServer.TabIndex = 0;
            this.txtServer.Text = "127.0.0.1";
            // 
            // gbxServer
            // 
            this.gbxServer.Controls.Add(this.txtServer);
            this.gbxServer.Location = new System.Drawing.Point(12, 12);
            this.gbxServer.Name = "gbxServer";
            this.gbxServer.Size = new System.Drawing.Size(260, 52);
            this.gbxServer.TabIndex = 2;
            this.gbxServer.TabStop = false;
            this.gbxServer.Text = "Server Address";
            // 
            // gbxUsername
            // 
            this.gbxUsername.Controls.Add(this.txtUsername);
            this.gbxUsername.Location = new System.Drawing.Point(12, 70);
            this.gbxUsername.Name = "gbxUsername";
            this.gbxUsername.Size = new System.Drawing.Size(260, 52);
            this.gbxUsername.TabIndex = 3;
            this.gbxUsername.TabStop = false;
            this.gbxUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtUsername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtUsername.Location = new System.Drawing.Point(6, 19);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(248, 20);
            this.txtUsername.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(18, 130);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(248, 30);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connexion";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // FormChatConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 172);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.gbxUsername);
            this.Controls.Add(this.gbxServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormChatConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CRM Chat Connection";
            this.Load += new System.EventHandler(this.FormChatConnection_Load);
            this.gbxServer.ResumeLayout(false);
            this.gbxServer.PerformLayout();
            this.gbxUsername.ResumeLayout(false);
            this.gbxUsername.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbxServer;
        private System.Windows.Forms.GroupBox gbxUsername;
        internal System.Windows.Forms.TextBox txtServer;
        internal System.Windows.Forms.TextBox txtUsername;
        internal System.Windows.Forms.Button btnConnect;
    }
}