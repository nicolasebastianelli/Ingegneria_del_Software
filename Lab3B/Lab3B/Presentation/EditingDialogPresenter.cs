using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lab3.Presentation
{
    public class EditingDialogPresenter
    {
        private readonly EditingDialog _target;
        private readonly EditingControlPresenter _editingControlPresenter;

        public EditingDialogPresenter(EditingDialog target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            _target = target;
            _editingControlPresenter = new EditingControlPresenter(Target.EditingControl);
            Target.ResetButton.Click += ResetButton_Click;
            Target.CancelButton_.Click += CancelButton_Click;
            Application.Idle += Application_Idle;
        }

        public EditingDialog Target
        {
            get { return _target; }
        }

        private EditingControlPresenter EditingControlPresenter
        {
            get { return _editingControlPresenter; }
        }

        public void SetEditableObject(object editingObject)
        {
            EditingControlPresenter.SetEditableObject(editingObject);
            Target.ResetButton.Enabled = !EditingControlPresenter.HasError;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            Target.OkButton.Enabled = !EditingControlPresenter.HasError;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            EditingControlPresenter.ResetEditingObject();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            EditingControlPresenter.ResetEditingObject();
        }
    }
}
