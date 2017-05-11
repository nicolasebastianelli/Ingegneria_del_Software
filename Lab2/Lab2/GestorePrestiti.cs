using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Lab2
{
    partial class GestorePrestiti
    {
        private readonly Dictionary<Libro, Prestito> _prestiti = new Dictionary<Libro, Prestito>();

        public Libro[] LibriInPrestito
        {
            get { return Prestiti.Keys.ToArray(); }
        }

        public bool IsLibroDisponibile(Libro libro)
        {
            #region Precondizioni
            Debug.Assert(libro != null, "libro != null");
            #endregion
            return !Prestiti.ContainsKey(libro);
        }

        public Persona PossessoreLibro(Libro libro)
        {
            #region Precondizioni
            Debug.Assert(libro != null, "libro != null");
            #endregion
            if (IsLibroDisponibile(libro))
            {
                return null;
            }
            else
            {
                return Prestiti[libro].Possessore;
            }
        }

        #region Nuovi servizi

        //
        //  Restituisce l’elenco (eventualmente vuoto) dei libri correntemente posseduti
        //  dalla persona passata come argomento
        //
        public IEnumerable<Libro> LibriPossedutiDa(Persona persona)
        {
            List<Libro> libriPosseduti = new List<Libro>();
            foreach (KeyValuePair<Libro, Prestito> entry in Prestiti)
            {
                if (entry.Value.Possessore == persona)
                {
                    libriPosseduti.Add(entry.Key);
                }
            }
            return libriPosseduti;

            //return from entry in Prestiti
            //       where entry.Value.Possessore == persona
            //       select entry.Key;
        }

        //
        //  Restituisce l’elenco (eventualmente vuoto) dei libri richiesti (ma non ancora posseduti)
        //  dalla persona passata come argomento
        //
        public IEnumerable<Libro> LibriRichiestiDa(Persona persona)
        {
            List<Libro> libriRichiesti = new List<Libro>();
            foreach (KeyValuePair<Libro, Prestito> entry in Prestiti)
            {
                if (entry.Value.Richiedenti.Contains(persona))
                {
                    libriRichiesti.Add(entry.Key);
                }
            }
            return libriRichiesti;

            //return from entry in Prestiti
            //       where entry.Value.Richiedenti.Contains(persona)
            //       select entry.Key;
        }

        //
        //  Restituisce l’elenco (eventualmente vuoto) delle persone che sono in attesa di ottenere in prestito
        //  il libro passato come argomento
        //
        public IEnumerable<Persona> RichiedentiLibro(Libro libro)
        {
            if (IsLibroDisponibile(libro))
            {
                return new List<Persona>();
            }
            else
            {
                return Prestiti[libro].Richiedenti;
            }
        }

        #endregion

        public void GestisciRichiesta(Libro libro, Persona persona)
        {
            #region Precondizioni
            Debug.Assert(libro != null, "libro != null");
            Debug.Assert(persona != null, "persona != null");
#if DEBUG
            int previousPrestitiCount = Prestiti.Count;
#endif
            #endregion
            if (IsLibroDisponibile(libro))
            {
                //  La richiesta può essere soddisfatta: viene creato un nuovo prestito
                Console.WriteLine(persona.Nome + " prende in prestito \"" + libro.Titolo + "\"");
                Prestiti.Add(libro, new Prestito(libro, persona));
            }
            else
            {
                //  La richiesta non può essere soddisfatta: il richiedente viene aggiunto alla coda dei richiedenti
                Console.WriteLine(persona.Nome + " vorrebbe prendere in prestito \"" + libro.Titolo + "\"");
                Prestito prestito = Prestiti[libro];
                prestito.AggiungiRichiedente(persona);
            }
            #region Postcondizioni
            Debug.Assert(Prestiti.Count == previousPrestitiCount || Prestiti.Count == previousPrestitiCount + 1);
            #endregion
        }

        public void GestisciConsegna(Libro libro, Persona persona)
        {
            #region Precondizioni
            Debug.Assert(libro != null);
            Debug.Assert(persona != null);
            Debug.Assert(Prestiti.ContainsKey(libro));
            Debug.Assert(persona == PossessoreLibro(libro));
#if DEBUG
            int previousPrestitiCount = Prestiti.Count;
#endif
            #endregion
            Console.WriteLine(persona.Nome + " consegna \"" + libro.Titolo + "\"");
            Prestito prestito = Prestiti[libro];
            Persona richiedente = prestito.PrendiPrimoRichiedente();
            if (richiedente != null)
            {
                Console.WriteLine(richiedente.Nome + " prende in prestito \"" + libro.Titolo + "\"");
                prestito.Possessore = richiedente;
            }
            else
            {
                Prestiti.Remove(libro);
            }
            #region Postcondizioni
            Debug.Assert(Prestiti.Count == previousPrestitiCount || Prestiti.Count == previousPrestitiCount - 1);
            #endregion
        }

        private Dictionary<Libro, Prestito> Prestiti
        {
            get { return _prestiti; }
        }
    }
}
