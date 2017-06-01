using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Lab3.Model
{
    partial class Documento
    {
        private readonly Biblioteca _biblioteca;
        private readonly GestorePrestiti _gestorePrestiti;

        private static Documento _instance;

        public event EventHandler<ChangedEventArgs> Changed;

        private Documento()
        {
            _biblioteca = new Biblioteca();
            _gestorePrestiti = new GestorePrestiti();
        }

        public static Documento GetInstance()
        {
            if (_instance == null)
                _instance = new Documento();
            return _instance;
        }

        #region Servizi della Biblioteca

        public IEnumerable<Libro> Libri
        {
            get { return _biblioteca.Libri.ToList(); }
        }

        public IEnumerable<Persona> Persone
        {
            get { return _biblioteca.Persone.ToList(); }
        }

        public Libro NuovoLibro()
        {
            return _biblioteca.NuovoLibro();
        }

        public Persona NuovaPersona()
        {
            return _biblioteca.NuovaPersona();
        }

        public void Aggiungi(Libro libro)
        {
            _biblioteca.Aggiungi(libro);
            OnChanged(ChangedEventArgs.InserimentoNuovoLibro(libro));
        }

        public void Aggiungi(Persona persona)
        {
            _biblioteca.Aggiungi(persona);
            OnChanged(ChangedEventArgs.InserimentoNuovaPersona(persona));
        }

        public void Modifica(Libro libro)
        {
            OnChanged(ChangedEventArgs.ModificaLibro(libro));
        }

        public void Modifica(Persona persona)
        {
            OnChanged(ChangedEventArgs.ModificaPersona(persona));
        }

        #endregion

        #region Servizi del GestorePrestiti

        public IEnumerable<Libro> LibriInPrestito
        {
            get { return _gestorePrestiti.LibriInPrestito.ToList(); }
        }

        public bool IsLibroDisponibile(Libro libro)
        {
            return _gestorePrestiti.IsLibroDisponibile(libro);
        }

        public Persona PossessoreLibro(Libro libro)
        {
            return _gestorePrestiti.PossessoreLibro(libro);
        }

        public IEnumerable<Libro> LibriPossedutiDa(Persona persona)
        {
            return _gestorePrestiti.LibriPossedutiDa(persona).ToList();
        }

        public IEnumerable<Libro> LibriRichiestiDa(Persona persona)
        {
            return _gestorePrestiti.LibriRichiestiDa(persona).ToList();
        }

        public IEnumerable<Persona> RichiedentiLibro(Libro libro)
        {
            return _gestorePrestiti.RichiedentiLibro(libro).ToList();
        }

        public void GestisciRichiesta(Libro libro, Persona persona)
        {
            _gestorePrestiti.GestisciRichiesta(libro, persona);
            OnChanged(ChangedEventArgs.Richiesta(libro, persona));
        }

        public void GestisciConsegna(Libro libro, Persona persona)
        {
            _gestorePrestiti.GestisciConsegna(libro, persona);
            OnChanged(ChangedEventArgs.Consegna(libro, persona));
        }

        #endregion

        private void OnChanged(ChangedEventArgs args)
        {
            if (Changed != null)
                Changed(this, args);
        }
    }
}
