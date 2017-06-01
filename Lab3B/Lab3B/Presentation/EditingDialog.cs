using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lab3.Presentation
{
    public partial class EditingDialog : Form
    {
        public EditingDialog()
        {
            InitializeComponent();
        }

        public EditingControl EditingControl
        {
            get { return _editingControl; }
        }

        public Button ResetButton
        {
            get { return _resetButton; }
        }

        public Button OkButton
        {
            get { return _okButton; }
        }

        public Button CancelButton_
        {
            get { return _cancelButton; }
        }
    }
}
