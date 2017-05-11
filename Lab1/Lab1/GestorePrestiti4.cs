using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    partial class GestorePrestiti4 : IGestorePrestiti
    {
	private readonly Dictionary<Libro, Prestito> _prestiti = new Dictionary<Libro, Prestito>();
 
        public Libro[] LibriInPrestito
        {
            get { return Prestiti.Keys.ToArray(); }
        }
 
        private Dictionary<Libro, Prestito> Prestiti
        {
            get { return _prestiti; }
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
                Prestiti.Add(libro, new Prestito(persona));
            }
            else
            {
                //  La richiesta non può essere soddisfatta: il richiedente viene aggiunto alla coda dei richiedenti
                Console.WriteLine(persona.Nome + " viene aggiunto alla coda dei richiedenti di \"" + libro.Titolo + "\"");
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

        private class Prestito
        {
            private readonly Libro _libro;
            private Persona _possessore;
            private Queue<Persona> _richiedenti;

            private static readonly Queue<Persona> EmptyQueue = new Queue<Persona>();

            public Prestito(Libro libro, Persona possessore)
            {
                #region Precondizioni
                Debug.Assert(libro != null, "libro != null");
                Debug.Assert(possessore != null, "possessore != null");
                #endregion
                _libro = libro;
                _possessore = possessore;
                _richiedenti = EmptyQueue;
            }

            public Libro Libro
            {
                get { return _libro; }
            }
            public Persona Possessore
            {
                get { return _possessore; }
                set
                {
                    #region Precondizioni
                    Debug.Assert(value != null, "value != null");
                    #endregion
                    _possessore = value;
                }
            }

            public IEnumerable<Persona> Richiedenti
            {
                get { return _richiedenti; }
            }

            public void AggiungiRichiedente(Persona richiedente)
            {
                #region Precondizioni
                Debug.Assert(richiedente != null, "richiedente != null");
                #endregion
                if (_richiedenti == EmptyQueue)
                    _richiedenti = new Queue<Persona>();
                //  il nuovo richiedente viene aggiunto solo se non esiste già
                if (!_richiedenti.Contains(richiedente))
                    _richiedenti.Enqueue(richiedente);
                #region Postcondizioni
                Debug.Assert(_richiedenti.Contains(richiedente));
                #endregion
            }

            public Persona PrendiPrimoRichiedente()
            {
                Persona richiedente;
                if (_richiedenti.Count == 0)
                {
                    richiedente = null;
                }
                else
                {
                    if (_richiedenti.Count > 1 && _richiedenti.Peek() == Possessore)
                        _richiedenti.Enqueue(_richiedenti.Dequeue());
                    richiedente = _richiedenti.Dequeue();
                }
                return richiedente;
            }
        }
    }
}
