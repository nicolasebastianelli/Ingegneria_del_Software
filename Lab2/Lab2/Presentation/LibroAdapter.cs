using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Presentation
{
    class LibroAdapter : IEntity
    {
        private readonly Libro _libro;
        private readonly Color _backColor;
        private readonly Color _foreColor;
        public string FullName
        {
            get
            {
                Persona persona = GetGestorePrestiti().PossessoreLibro(_libro);
                if (persona != null)
                    return String.Format("{0} - {1}", _libro.Titolo, persona.Nome);
                else
                    return _libro.Titolo;
            }
        }
        private GestorePrestiti GetGestorePrestiti()
        {
            return Documento.GetInstance().GestorePrestiti;
        }
        public Color BackColor
        {
            get { return _backColor; }
        }

        public Color ForeColor
        {
            get { return _foreColor; }
        }

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

        IEnumerable<IEntity> IEntity.SubEntities
        {
            get
            {
                List<IEntity> subEntities = new List<IEntity>();
                foreach(Persona persona in GetGestorePrestiti().RichiedentiLibro(_libro))
                {
                    subEntities.Add(new PersonaAdapter(persona));
                }
                return subEntities;
            }
        }
    }
}
