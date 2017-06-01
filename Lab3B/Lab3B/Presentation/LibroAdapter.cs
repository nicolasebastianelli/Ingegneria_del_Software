using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

using Lab3.Model;

namespace Lab3.Presentation
{
    class LibroAdapter : IEntity
    {
        private readonly Libro _libro;
        private readonly Color _backColor;
        private readonly Color _foreColor;

        public LibroAdapter(Libro libro)
            : this(libro, Color.Empty, Color.Empty)
        {
        }

        public LibroAdapter(Libro libro, Color backColor, Color foreColor)
        {
            if (libro == null)
                throw new ArgumentNullException("libro");
            _libro = libro;
            _backColor = backColor;
            _foreColor = foreColor;
        }

        public Libro Libro
        {
            get { return _libro; }
        }

        #region IEntity Members

        public string FullName
        {
            get
            {
                Persona persona = Documento.PossessoreLibro(Libro);
                if (persona != null)
                    return String.Format("{0} - {1}", Libro.Titolo, persona.Nome);
                else
                    return Libro.Titolo;
            }
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
                foreach (Persona persona in Documento.RichiedentiLibro(Libro))
                {
                    subEntities.Add(new PersonaAdapter(persona));
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
