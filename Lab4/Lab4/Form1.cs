using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LogForm logForm = new LogForm();
            logForm.Show();
            FormGenerator formGenerator = new FormGenerator(this, logForm);
            FormGenerator formGenerator2 = new FormGenerator(this, new LogForDebug());
            formGenerator.Load("../../Controls.xml");
            formGenerator.LinkHandler();
        }
    }
}
