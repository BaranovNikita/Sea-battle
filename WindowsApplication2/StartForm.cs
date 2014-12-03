using System;
using System.Windows.Forms;

namespace SeaBattle
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void singleGame_Click(object sender, EventArgs e)
        {

        }

        private void multiGame_Click(object sender, EventArgs e)
        {
            var settingsServer = new ServerSettings();
            settingsServer.ShowDialog();
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(@"Закрыть?", @"Message", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
            else
            {
                e.Cancel = false;
                if (!Server.IsStarted()) return;
                Server.Stop();
                ServerSettings.Server.Join();
            }
        }
    }
}
