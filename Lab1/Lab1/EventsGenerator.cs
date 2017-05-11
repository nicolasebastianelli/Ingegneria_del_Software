using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Collections;

namespace Lab1
{
    delegate void Azione(Libro libro, Persona persona);

    class EventsGenerator
    {
        public event Azione Richiesta;
        public event Azione Consegna;
        private Timer _timer;
        private readonly Random _random;
        private readonly Biblioteca _biblioteca;
        private readonly GestorePrestiti _gestorePrestiti;

        private enum TipoEvento
        {
            Nop,
            Richiesta,
            Consegna
        }

        public EventsGenerator(Biblioteca biblioteca ,GestorePrestiti gestoreprestiti)
        {
            _biblioteca = biblioteca;
            _gestorePrestiti = gestoreprestiti;
            _random=new Random();
            _timer= new Timer(1000);
            _timer.Elapsed += OnTimedEvent;
            _timer.Enabled = true;
        }

         private void OnTimedEvent(object source, ElapsedEventArgs e)
         {
              switch ((TipoEvento) GetRandomElementFrom(Enum.GetValues(typeof(TipoEvento))))
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
                Libro libro = (Libro)GetRandomElementFrom(_biblioteca.Libri);
                Persona persona = (Persona)GetRandomElementFrom(_biblioteca.Persone);
                if (libro != null && persona != null)
                    Richiesta(libro, persona);
            }
        }

        private void OnConsegna()
        {
            if (Consegna != null)
            {
                Libro libro = (Libro)GetRandomElementFrom(_gestorePrestiti.LibriInPrestito);
                if (libro != null)
                    Consegna(libro, _gestorePrestiti.PossessoreLibro(libro));
            }
        }

        private object GetRandomElementFrom(IList list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            else
            {
                return list[_random.Next(list.Count)];
            }
        }
    }
}
