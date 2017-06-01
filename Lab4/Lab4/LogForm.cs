using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Lab4
{
    public partial class LogForm : Form , ILogger
    {
        public LogForm()
        {
            InitializeComponent();
        }

        public void WriteLine(string line)
        {
            _textBox.AppendText(line+Environment.NewLine);
        }

        private void saveAsToolStripMenuItem_Click_1(object sender ,EventArgs e){
            SaveLog(false);
        }

        private void appendToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLog(true);
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SaveLog(bool append){
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "LogFile.txt";
            saveFile.Filter = "Text files (*.txt)|*.txt";
            saveFile.RestoreDirectory = true;
            saveFile.AddExtension = true;
            saveFile.OverwritePrompt = !append;
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFile.FileName,append))
                    sw.WriteLine(_textBox.Text);
            }
        }
     }
}
