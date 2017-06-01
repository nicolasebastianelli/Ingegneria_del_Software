using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

using Lab3.Model;

namespace Lab3.Presentation
{
    class PersonaAdapter : IEntity
    {
        private readonly Persona _persona;
        private readonly Color _backColor;
        private readonly Color _foreColor;

        public PersonaAdapter(Persona persona)
            : this(persona, Color.Empty, Color.Empty)
        {
        }

        public PersonaAdapter(Persona persona, Color backColor, Color foreColor)
        {
            if (persona == null)
                throw new ArgumentNullException("persona");
            _persona = persona;
            _backColor = backColor;
            _foreColor = foreColor;
        }

        public Persona Persona
        {
            get { return _persona; }
        }

        #region IEntity Members

        public string FullName
        {
            get { return Persona.Nome; }
        }

        public Color BackColor
        {
            get { return _backColor; }
        }

        public Color ForeColor
        {
            get { return _foreColor; }
        }

        public IEnumerable<IEntity> SubEntities
        {
            get
            {
                List<IEntity> subEntities = new List<IEntity>();
                foreach (Libro libro in Documento.LibriPossedutiDa(Persona))
                {
                    subEntities.Add(new LibroAdapter(libro, Color.Green, Color.White));
                }
                foreach (Libro libro in Documento.LibriRichiestiDa(Persona))
                {
                    subEntities.Add(new LibroAdapter(libro, Color.Red, Color.White));
                }
                return subEntities;
            }
        }

        private Documento Documento
        {
            get { return Documento.GetInstance(); }
        }

        #endregion
    }
}
