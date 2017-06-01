using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lab3.Presentation
{
    public partial class SelectDialog : Form
    {
        public SelectDialog()
        {
            InitializeComponent();
        }

        //  Restituisce l’elemento correntemente selezionato in _comboBox.
        public object SelectedItem
        {
            get
            {
                return _comboBox.SelectedItem;
            }
        }

        //  Carica in _comboBox tutti gli elementi di items.
        //  Al momento della visualizzazione deve risultare selezionato il primo elemento di items.
        public void LoadItems(IEnumerable items)
        {
            _comboBox.DataSource = items;
        }
    }
}
