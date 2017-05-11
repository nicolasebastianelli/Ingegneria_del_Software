using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Lab2
{
    partial class GestorePrestiti
    {
        private class Prestito
        {
            //  Invariante di classe: _libro deve essere != null
            //  Garantito dal costruttore
            private readonly Libro _libro;
            //  Invariante di classe: _possessore deve essere sempre != null
            //  Garantito dal costruttore e dalla set della proprietà Possessore
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