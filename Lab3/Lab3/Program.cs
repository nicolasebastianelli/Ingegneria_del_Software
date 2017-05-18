using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

using Lab3.Services;
using Lab3.Presentation;

namespace Lab3
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainForm = new MainForm();
            new MainFormPresenter(mainForm);
            DocumentServices.RegisterCommands();
            new EventsGenerator();
            Application.Run(mainForm);
        }
    }
}
