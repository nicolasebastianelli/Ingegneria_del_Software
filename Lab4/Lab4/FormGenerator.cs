using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;

namespace Lab4
{
    class FormGenerator
    {
        private readonly Form _form;
        private readonly ILogger _logger;
        private Type _controlType;
        private Control _control;

        public FormGenerator(Form form)
        {
            _form = form;
        }

        public FormGenerator(Form form, ILogger logger)
        {
            _form = form;
            _logger = logger;
        }

        public void Load(string fileName)
        {
            XmlDocument xmlDocument =new XmlDocument();
            xmlDocument.Load(fileName);
            XmlNodeList nodeList = xmlDocument.SelectNodes("/Form/Control");
            foreach (XmlElement node in nodeList)
            {
                CreateControl(node.GetAttribute("type"),node.GetAttribute("assembly"));
                if (_control == null)
                {
                    continue;
                }
                foreach (XmlElement subElement in node.OfType<XmlElement>())
                {
                    SetControlProperty(subElement.Name, subElement.InnerText);
                }
                AddControlToForm();
            }
        }

        private void CreateControl(string controlType , string controlAssembly )
        {
            WriteLine("");
            WriteLine("Creazione del controllo:");
            WriteLine("  Tipo = "+controlType);
            WriteLine("  Assembly = " + controlAssembly);
            try
            {
                Assembly assembly = Assembly.Load(controlAssembly);
                _controlType = assembly.GetType(controlType, true);
                _control = (Control)Activator.CreateInstance(_controlType);
            }
            catch
            {
                WriteLine("-- Creazione fallita");
                _controlType = null;
                _control = null;
            }

        }

        private void SetControlProperty(string propertyName,string propertyValue )
        {
            try
            {
                PropertyInfo propertyInfo = _controlType.GetProperty(propertyName);
                object value = Convert.ChangeType(propertyValue,propertyInfo.PropertyType);
                propertyInfo.SetValue(_control, value, null);
                WriteLine("Proprieta: " + propertyName + ", valore: " + propertyValue);
            }
            catch { WriteLine("-- Proprieta errata o non esistente"); }
        }

        private void AddControlToForm()
        {
            WriteLine("Creazione ok!");
            _form.Controls.Add(_control);
        }

        private void WriteLine(string line)
        {
            if (_logger != null)
                _logger.WriteLine(line);
        }

        public void LinkHandler()
        {
            foreach (Button button in _form.Controls.OfType<Button>())
            {
                switch (button.Text)
                {
                    case "Start":
                        button.Click += StartHandler;
                        break;
                    case "Stop":
                        button.Click += StopHandler;
                        break;
                }
            }

        }

        private void StartHandler(object sender,EventArgs e) 
        {
            InvokeHandler("Start");
        }

        

        private void StopHandler(object sender,EventArgs e )
        {
            InvokeHandler("Stop");
        }

        private void InvokeHandler(string method)
        {
            foreach (Control control in _form.Controls)
            {
                MethodInfo methodInfo = control.GetType().GetMethod(method, Type.EmptyTypes);
                if(methodInfo==null)
                    continue;
                if (methodInfo.ReturnType != typeof(void))
                    continue;
                control.GetType().GetMethod(method).Invoke(control,null);
            }
        }
    }
}
