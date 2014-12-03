namespace SeaBattle
{
    partial class StartForm
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
            this.singleGame = new System.Windows.Forms.Button();
            this.multiGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // singleGame
            // 
            this.singleGame.AutoSize = true;
            this.singleGame.Location = new System.Drawing.Point(128, 120);
            this.singleGame.Name = "singleGame";
            this.singleGame.Size = new System.Drawing.Size(126, 23);
            this.singleGame.TabIndex = 0;
            this.singleGame.Text = "Игра с Компьютером";
            this.singleGame.UseVisualStyleBackColor = true;
            this.singleGame.Click += new System.EventHandler(this.singleGame_Click);
            // 
            // multiGame
            // 
            this.multiGame.AutoSize = true;
            this.multiGame.Location = new System.Drawing.Point(111, 166);
            this.multiGame.Name = "multiGame";
            this.multiGame.Size = new System.Drawing.Size(157, 23);
            this.multiGame.TabIndex = 1;
            this.multiGame.Text = "Игра по локальной  сети";
            this.multiGame.UseVisualStyleBackColor = true;
            this.multiGame.Click += new System.EventHandler(this.multiGame_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(382, 347);
            this.Controls.Add(this.multiGame);
            this.Controls.Add(this.singleGame);
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StartForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button singleGame;
        private System.Windows.Forms.Button multiGame;
    }
}