namespace Lab3.Presentation
{
    partial class EditingDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._buttonsPanel = new System.Windows.Forms.Panel();
            this._resetButton = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._editingControl = new Lab3.Presentation.EditingControl();
            this._buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._buttonsPanel.Controls.Add(this._resetButton);
            this._buttonsPanel.Controls.Add(this._okButton);
            this._buttonsPanel.Controls.Add(this._cancelButton);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttonsPanel.Location = new System.Drawing.Point(0, 347);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Size = new System.Drawing.Size(587, 49);
            this._buttonsPanel.TabIndex = 2;
            // 
            // _resetButton
            // 
            this._resetButton.Location = new System.Drawing.Point(12, 11);
            this._resetButton.Name = "_resetButton";
            this._resetButton.Size = new System.Drawing.Size(75, 23);
            this._resetButton.TabIndex = 4;
            this._resetButton.Text = "Reset";
            this._resetButton.UseVisualStyleBackColor = true;
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._okButton.Location = new System.Drawing.Point(410, 11);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 2;
            this._okButton.Text = "OK";
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.CausesValidation = false;
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(497, 11);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 3;
            this._cancelButton.Text = "Annulla";
            // 
            // _editingControl
            // 
            this._editingControl.BackColor = System.Drawing.SystemColors.Control;
            this._editingControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._editingControl.Location = new System.Drawing.Point(0, 0);
            this._editingControl.Name = "_editingControl";
            this._editingControl.Padding = new System.Windows.Forms.Padding(10);
            this._editingControl.Size = new System.Drawing.Size(587, 347);
            this._editingControl.TabIndex = 4;
            // 
            // EditingDialog
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(587, 396);
            this.Controls.Add(this._editingControl);
            this.Controls.Add(this._buttonsPanel);
            this.Name = "EditingDialog";
            this.Text = "EditingDialog";
            this._buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _buttonsPanel;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Button _cancelButton;
        private EditingControl _editingControl;
        private System.Windows.Forms.Button _resetButton;
    }
}