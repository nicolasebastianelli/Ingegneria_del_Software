using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Lab2
{
    delegate void Azione(Libro libro, Persona persona);

    class EventsGenerator
    {
        public event Azione Richiesta;
        public event Azione Consegna;

        private enum TipoEvento
        {
            Nop,
            Richiesta,
            Consegna
        }

        private readonly Random _random;
        private readonly Biblioteca _biblioteca;
        private readonly GestorePrestiti _gestorePrestiti;

        public EventsGenerator(Biblioteca biblioteca, GestorePrestiti gestorePrestiti)
        {
            _biblioteca = biblioteca;
            _gestorePrestiti = gestorePrestiti;
            _random = new Random();
        }

        public void OnTimedEvent(object source, EventArgs e)
        {
            switch ((TipoEvento) GetRandomElementFrom((TipoEvento[]) Enum.GetValues(typeof(TipoEvento))))
            {
                case TipoEvento.Richiesta:
                    OnRichiesta();
                    break;
                case TipoEvento.Consegna:
                    OnConsegna();
                    break;
                default:
                    break;
            }
        }

        private void OnRichiesta()
        {
            if (Richiesta != null)
            {
                Libro libro = (Libro) GetRandomElementFrom(_biblioteca.Libri);
                Persona persona = (Persona) GetRandomElementFrom(_biblioteca.Persone);
                if (libro != null && persona != null)
                    Richiesta(libro, persona);
            }
        }

        private void OnConsegna()
        {
            if (Consegna != null)
            {
                Libro libro = (Libro) GetRandomElementFrom(_gestorePrestiti.LibriInPrestito);
                if (libro != null)
                    Consegna(libro, _gestorePrestiti.PossessoreLibro(libro));
            }
        }

        private object GetRandomElementFrom(IList list)
        {
            int count = list.Count;
            if (count == 0)
                return null;
            else
                return list[_random.Next(count)];
        }

        //private T GetRandomElementFrom<T>(IEnumerable<T> list)
        //{
        //    return list.ElementAtOrDefault(_random.Next(list.Count()));
        //}
    }
}
