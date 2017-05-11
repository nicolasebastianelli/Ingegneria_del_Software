using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Presentation
{
    class PersonaAdapter : IEntity
    {
        private readonly Persona _persona;
        private readonly Color _backColor;
        private readonly Color _foreColor;

        public string FullName
        {
            get { return _persona.Nome; }
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

        public IEnumerable<IEntity> SubEntities
        {
            get
            {
                List<IEntity> subEntities = new List<IEntity>();
                foreach (Libro libro in GetGestorePrestiti().LibriPossedutiDa(_persona)){
                    subEntities.Add(new LibroAdapter(libro, Color.Green, Color.White));
                }
                foreach (Libro libro in GetGestorePrestiti().LibriRichiestiDa(_persona)){
                    subEntities.Add(new LibroAdapter(libro, Color.Red, Color.White));
                }
                return subEntities;
            }
        }

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

    }
}
