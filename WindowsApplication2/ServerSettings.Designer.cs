namespace SeaBattle
{
    partial class ServerSettings
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
            this.serverRadio = new System.Windows.Forms.RadioButton();
            this.clientRadio = new System.Windows.Forms.RadioButton();
            this.startServer = new System.Windows.Forms.Button();
            this.ipServer = new System.Windows.Forms.TextBox();
            this.serverIp = new IPAddressControlLib.IPAddressControl();
            this.clientConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverRadio
            // 
            this.serverRadio.AutoSize = true;
            this.serverRadio.Checked = true;
            this.serverRadio.Location = new System.Drawing.Point(22, 12);
            this.serverRadio.Name = "serverRadio";
            this.serverRadio.Size = new System.Drawing.Size(71, 17);
            this.serverRadio.TabIndex = 0;
            this.serverRadio.TabStop = true;
            this.serverRadio.Text = "Я сервер";
            this.serverRadio.UseVisualStyleBackColor = true;
            this.serverRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // clientRadio
            // 
            this.clientRadio.AutoSize = true;
            this.clientRadio.Location = new System.Drawing.Point(161, 12);
            this.clientRadio.Name = "clientRadio";
            this.clientRadio.Size = new System.Drawing.Size(70, 17);
            this.clientRadio.TabIndex = 1;
            this.clientRadio.Text = "Я клиент";
            this.clientRadio.UseVisualStyleBackColor = true;
            this.clientRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // startServer
            // 
            this.startServer.Location = new System.Drawing.Point(22, 55);
            this.startServer.Name = "startServer";
            this.startServer.Size = new System.Drawing.Size(75, 23);
            this.startServer.TabIndex = 2;
            this.startServer.Text = "Запустить";
            this.startServer.UseVisualStyleBackColor = true;
            this.startServer.Click += new System.EventHandler(this.startServer_Click);
            // 
            // ipServer
            // 
            this.ipServer.Location = new System.Drawing.Point(12, 101);
            this.ipServer.Name = "ipServer";
            this.ipServer.ReadOnly = true;
            this.ipServer.Size = new System.Drawing.Size(97, 20);
            this.ipServer.TabIndex = 3;
            // 
            // serverIp
            // 
            this.serverIp.AllowInternalTab = false;
            this.serverIp.AutoHeight = true;
            this.serverIp.BackColor = System.Drawing.SystemColors.Window;
            this.serverIp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.serverIp.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.serverIp.Enabled = false;
            this.serverIp.Location = new System.Drawing.Point(149, 58);
            this.serverIp.Name = "serverIp";
            this.serverIp.ReadOnly = false;
            this.serverIp.Size = new System.Drawing.Size(108, 20);
            this.serverIp.TabIndex = 9;
            this.serverIp.Text = "...";
            // 
            // clientConnect
            // 
            this.clientConnect.Enabled = false;
            this.clientConnect.Location = new System.Drawing.Point(149, 101);
            this.clientConnect.Name = "clientConnect";
            this.clientConnect.Size = new System.Drawing.Size(108, 23);
            this.clientConnect.TabIndex = 10;
            this.clientConnect.Text = "Подключиться";
            this.clientConnect.UseVisualStyleBackColor = true;
            this.clientConnect.Click += new System.EventHandler(this.clientConnect_Click);
            // 
            // ServerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 262);
            this.Controls.Add(this.clientConnect);
            this.Controls.Add(this.serverIp);
            this.Controls.Add(this.ipServer);
            this.Controls.Add(this.startServer);
            this.Controls.Add(this.clientRadio);
            this.Controls.Add(this.serverRadio);
            this.Name = "ServerSettings";
            this.Text = "ServerSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton serverRadio;
        private System.Windows.Forms.RadioButton clientRadio;
        private System.Windows.Forms.Button startServer;
        private System.Windows.Forms.TextBox ipServer;
        private IPAddressControlLib.IPAddressControl serverIp;
        private System.Windows.Forms.Button clientConnect;
    }
}