using Lab2.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            new EntitiesPresenter(_treeView1, GetPersoneAsEntities);
            new EntitiesPresenter(_treeView2, GetLibriAsEntities);
            _timer.Tick += Documento.GetInstance().EventsGenerator.OnTimedEvent;
        }

        private IEnumerable<IEntity> GetPersoneAsEntities()
        {
            List<IEntity> entities = new List<IEntity>();
            foreach (Persona persona in Documento.GetInstance().Biblioteca.Persone)
            {
                entities.Add(new PersonaAdapter(persona));
            }
            return entities;
        }

        private IEnumerable<IEntity> GetLibriAsEntities()
        {
            List<IEntity> entities = new List<IEntity>();
            foreach (Libro libro in Documento.GetInstance().Biblioteca.Libri)
            {
                entities.Add(new LibroAdapter(libro));
            }
            return entities;
        }

        private void _startButton_Click(object sender, EventArgs e)
        {
            _timer.Enabled = true;
            _startButton.Enabled = false;
            _stopButton.Enabled = true;
            
        }

        private void _stopButton_Click(object sender, EventArgs e)
        {
            _timer.Enabled = false;
            _startButton.Enabled = true;
            _stopButton.Enabled = false;
        }
    }
}
