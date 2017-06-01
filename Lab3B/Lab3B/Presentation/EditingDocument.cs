using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using Lab3.Model;

namespace Lab3.Presentation
{
    //  Modella l'insieme di tutte le proprietà 'editable' di un editingObject
    public class EditingDocument
    {
        //  Oggetto da modificare.
        private readonly object _editingObject;
        //  Elenco delle proprietà editabili
        private readonly List<EditingProperty> _editingProperties = new List<EditingProperty>();

        public EditingDocument(object editingObject)
        {
            if (editingObject == null)
                throw new ArgumentNullException("editingObject");
            _editingObject = editingObject;
            InitializeEditingProperties();
        }

        public object EditingObject
        {
            get { return _editingObject; }
        }

        public IEnumerable<EditingProperty> EditingProperties
        {
            get { return _editingProperties; }
        }

        //  Restituisce true se c'è almeno un errore di validazione.
        public bool HasError
        {
            get
            {
                foreach (EditingProperty property in _editingProperties)
                {
                    if (property.HasError)
                        return true;
                }
                return false;
            }
        }

        //  Reimposta i valori originali di tutte le proprietà non readonly dell'editingObject.
        public void ResetEditingObject()
        {
            #region ----- TODO -----

            #endregion
        }

        //  Memorizza in _editingProperties tutte le proprietà editable
        private void InitializeEditingProperties()
        {
            //	Per ogni proprietà pubblica di EditingObject alla quale è stato associato l’attributo Editable:
            //    se la proprietà è write-only, sollevare un’eccezione;
            //    creare una nuova EditingProperty sulla proprietà e aggiungerla alla collezione _editingProperties;
            //    visualizzare nella finestra di output del debugger la descrizione completa dell'EditingProperty
            //      ad esempio: "Add EditingProperty Titolo  {Label = Titolo, Width = 100} OriginalValue = TitoloLibro_1"

            Type type =EditingObject.GetType();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                EditableAttribute[] attributes = (EditableAttribute[]) propertyInfo.GetCustomAttributes(typeof(EditableAttribute),false);
                if (attributes.Length == 0)
                {
                    continue;
                }
                if(!propertyInfo.CanRead){
                    throw new ApplicationException("Attributo editable non applicabile");
                }
                EditingProperty editingProperty =new EditingProperty(propertyInfo,attributes[0], EditingObject);
                _editingProperties.Add(editingProperty);
                Console.WriteLine("Add EditingProperty "+editingProperty.ToString());
            }
            
        }
    }
}
