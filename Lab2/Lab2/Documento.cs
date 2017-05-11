using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2
{
    class Documento
    {
        private readonly Biblioteca _biblioteca;
        private readonly GestorePrestiti _gestorePrestiti;
        private readonly EventsGenerator _eventsGenerator;

        private static Documento _instance;

        public event EventHandler Changed;

        private Documento()
        {
            _biblioteca = new Biblioteca();
            _gestorePrestiti = new GestorePrestiti();
            _eventsGenerator = new EventsGenerator(_biblioteca, _gestorePrestiti);
            _eventsGenerator.Richiesta += OnRichiesta;
            _eventsGenerator.Consegna += OnConsegna;
        }

        public static Documento GetInstance()
        {
            if (_instance == null)
                _instance = new Documento();
            return _instance;
        }

        public Biblioteca Biblioteca
        {
            get { return _biblioteca; }
        }

        public GestorePrestiti GestorePrestiti
        {
            get { return _gestorePrestiti; }
        }

        public EventsGenerator EventsGenerator
        {
            get { return _eventsGenerator; }
        }

        private void OnRichiesta(Libro libro, Persona persona)
        {
            _gestorePrestiti.GestisciRichiesta(libro, persona);
            OnChanged();
        }

        private void OnConsegna(Libro libro, Persona persona)
        {
            _gestorePrestiti.GestisciConsegna(libro, persona);
            OnChanged();
        }

        private void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }
    }
}
