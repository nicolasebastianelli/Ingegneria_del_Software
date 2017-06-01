using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lab3.Presentation
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public TreeView TreeView1
        {
            get { return _treeView1; }
        }

        public TreeView TreeView2
        {
            get { return _treeView2; }
        }

        //  Permette di aggiornare la UI in funzione del valore di started (true == simulazione in corso)
        public void UpdateUI(bool started)
        {
            //  Abilitare o disabilitare in modo opportuno i menu e i bottoni che permettono di eseguire comandi
            //  Se la simulazione è in corso,
            //    i comandi di inserimento e modifica di libri e persone devono essere disabilitati

                _startButton.Enabled = !started;
                _stopButton.Enabled = started;
                _inserisciNuovaPersonaButton.Enabled = !started;
                _inserisciNuovoLibroButton.Enabled = !started;
                _modificaLibroButton.Enabled = !started;
                _modificaPersonaButton.Enabled = !started;
            
        }
    }
}
