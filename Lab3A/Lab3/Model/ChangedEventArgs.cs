using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Lab3.Model
{
    class ChangedEventArgs : EventArgs
    {
        private readonly TipoEvento _tipoEvento;
        private readonly Libro _libro;
        private readonly Persona _persona;

        private ChangedEventArgs(TipoEvento tipoEvento, Libro libro, Persona persona)
        {
            _tipoEvento = tipoEvento;
            _libro = libro;
            _persona = persona;
        }

        public static ChangedEventArgs InserimentoNuovoLibro(Libro libro)
        {
            if (libro == null)
                throw new ArgumentNullException("libro");
            return new ChangedEventArgs(TipoEvento.InserimentoNuovoLibro, libro, null);
        }

        public static ChangedEventArgs InserimentoNuovaPersona(Persona persona)
        {
            if (persona == null)
                throw new ArgumentNullException("persona");
            return new ChangedEventArgs(TipoEvento.InserimentoNuovaPersona, null, persona);
        }

        public static ChangedEventArgs ModificaLibro(Libro libro)
        {
            if (libro == null)
                throw new ArgumentNullException("libro");
            return new ChangedEventArgs(TipoEvento.ModificaLibro, libro, null);
        }

        public static ChangedEventArgs ModificaPersona(Persona persona)
        {
            if (persona == null)
                throw new ArgumentNullException("persona");
            return new ChangedEventArgs(TipoEvento.ModificaPersona, null, persona);
        }

        public static ChangedEventArgs Richiesta(Libro libro, Persona persona)
        {
            if (libro == null)
                throw new ArgumentNullException("libro");
            if (persona == null)
                throw new ArgumentNullException("persona");
            return new ChangedEventArgs(TipoEvento.Richiesta, libro, persona);
        }

        public static ChangedEventArgs Consegna(Libro libro, Persona persona)
        {
            if (libro == null)
                throw new ArgumentNullException("libro");
            if (persona == null)
                throw new ArgumentNullException("persona");
            return new ChangedEventArgs(TipoEvento.Consegna, libro, persona);
        }

        public TipoEvento TipoEvento
        {
            get { return _tipoEvento; }
        }

        public Libro Libro
        {
            get { return _libro; }
        }

        public Persona Persona
        {
            get { return _persona; }
        }
    }
}
