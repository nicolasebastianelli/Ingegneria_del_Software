using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace Lab3.Presentation
{
    //  Permette di modificare i valori delle proprietà di un qualsiasi tipo di oggetto.
    //  Le proprietà prese in considerazione sono esclusivamente quelle marcate con l'attributo Editable.
    public class EditingControlPresenter
    {
        private readonly EditingControl _target;
        private EditingDocument _editingDocument;

        public EditingControlPresenter(EditingControl target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            _target = target;
        }

        public EditingControl Target
        {
            get { return _target; }
        }

        private EditingDocument EditingDocument
        {
            get { return _editingDocument; }
            set { _editingDocument = value; }
        }

        //  Restituisce true se ci sono errori di validazione.
        public bool HasError
        {
            get
            {
                return EditingDocument.HasError;
            }
        }

        //  Associa all'EditingControlPresenter il nuovo oggetto da modificare.
        //  Può essere invocato più volte
        public void SetEditableObject(object editingObject)
        {
            EditingDocument = new EditingDocument(editingObject);
            InitializeTarget();
        }

        //  Reimposta i valori originali di tutte le proprietà non readonly dell'editingObject.
        public void ResetEditingObject()
        {
            EditingDocument.ResetEditingObject();
            RefreshTextBoxes(null);
        }

        //  Inizializza l’EditingControl aggiungendo una coppia di controlli (Label, TextBox) per ogni proprietà 'editable' di EditingDocument
        //  Viene invocato ogni volta che cambia l'editingObject (e quindi l'EditingDocument).
        private void InitializeTarget()
        {
            //	Eliminare da Target.TableLayoutPanel eventuali controlli inseriti in precedenza.
            //	Per ogni proprietà Editable:
            //    invocare il metodo AddRow passando come argomento l'EditingProperty.
            //	Invocare il metodo RefreshTextBoxes in modo che vengano aggiornati i valori di tutte le TextBox.
            Target.SuspendLayout();
            Target.TableLayoutPanel.Controls.Clear();
            foreach (EditingProperty editingProperty in EditingDocument.EditingProperties)
            {
                AddRow(editingProperty);
            }
            RefreshTextBoxes(null);
            Target.ResumeLayout(false);
        }

        //  Crea, inizializza e aggiunge al Target.TableLayoutPanel nell’ordine: una Label e una TextBox.
        private void AddRow(EditingProperty editingProperty)
        {
            //  Per inizializzare la Label:
            // 	  assegnare alla proprietà Text il valore della proprietà Label di editableAttribute; 
            // 	  assegnare alla proprietà AutoSize il valore true, in modo che il testo venga visualizzato correttamente.
            //  Per inizializzare la TextBox:
            // 	  dimensionare il controllo in modo che
            //      la larghezza sia pari al valore della proprietà Width di editableAttribute e
            //      l’altezza sia pari al valore della proprietà PreferredHeight del controllo stesso;
            // 	  se la editingProperty è read-only,
            //      disabilitare la TextBox (utilizzare la proprietà Enabled);
            // 	  assegnare alla proprietà Tag il valore di editingProperty;
            // 	  infine, collegare all’evento Validating della TextBox il gestore ValidatingHandler.
            Label label = new Label();
            label.AutoSize = true;
            label.Text = editingProperty.Label;
            TextBox textbox = new TextBox();
            textbox.Width = editingProperty.Width;
            textbox.Height = textbox.PreferredHeight;
            if (!editingProperty.PropertyInfo.CanWrite)
                textbox.Enabled = false;
            textbox.Tag = editingProperty;
            textbox.Validating += ValidatingHandler;
            Target.TableLayoutPanel.Controls.Add(label);
            Target.TableLayoutPanel.Controls.Add(textbox);
         }

        //  Inserisce nelle TextBox i valori delle corrispondenti proprietà dell'editingObject.
        private void RefreshTextBoxes(TextBox excludedTextBox)
        {
            //  Per ogni textBox contenuta in Target.TableLayoutPanel, ad esclusione di excludedTextBox:
            //	  recuperare la editingProperty precedentemente salvata nella proprietà Tag di textBox;
            //    assegnare alla proprietà Text di textBox il valore corrente dell'editingProperty come stringa di caratteri;
            //	  infine, se la editingProperty è writable, invocare il metodo Validate passando come argomento textBox.

            foreach (Control control in Target.TableLayoutPanel.Controls)
            {
                if (control is TextBox && !((TextBox) control).Equals(excludedTextBox))
                {
                    EditingProperty property = (EditingProperty)control.Tag;
                    control.Text = property.ConvertToString();
                    if (property.CanWrite)
                        Validate((TextBox)control);
                }
            }
        }

        //  Viene invocato automaticamente quando una TextBox perde il focus
        //  Coordina la validazione del dato inserito nella TextBox (il sender).
        private void ValidatingHandler(object sender, CancelEventArgs args)
        {
            //  Invocare il metodo Validate passando come argomento il sender.
            //  Se non ci sono errori di validazione, invocare il metodo RefreshTextBoxes passando come argomento il sender.
            //  Si noti che l’invocazione finale del metodo RefreshTextBoxes permette di visualizzare correttamente
            //  i valori di eventuali proprietà calcolabili (cioè che si basano sui valori di altre proprietà).
            Validate((TextBox)sender);
            if (HasError)
            {
                RefreshTextBoxes((TextBox)sender);
            }
        }

        //  Esegue la validazione vera e propria del dato contenuto nella textBox passata come argomento.
        private void Validate(TextBox textBox)
        {
            // 	Recuperare la editingProperty dalla textBox.
            // 	Invocare in modo opportuno i metodi TryConvertFromString e TrySetValue dell'editingProperty.
            //  Infine, aggiornare l'ErrorProvider per segnalare all'utente che la textBox è con o senza errori.
            Object value;
            EditingProperty property = (EditingProperty)textBox.Tag;
            if(property.TryConvertFromString(textBox.Text,out value)){
                property.TrySetValue(value);
            }
            Target.ErrorProvider.SetError(textBox, property.Message);
        }
    }
}
