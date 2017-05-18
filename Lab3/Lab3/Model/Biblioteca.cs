using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Lab3.Model
{
    partial class Documento
    {
        class Biblioteca
        {
            private Dictionary<int, Libro> _libri = new Dictionary<int, Libro>();
            private Dictionary<int, Persona> _persone = new Dictionary<int, Persona>();

            public Biblioteca()
            {
                for (int k = 1; k <= 5; k++)
                    Aggiungi(CreaLibroRandom());
                for (int k = 1; k <= 5; k++)
                    Aggiungi(CreaPersonaRandom());
            }

            public IEnumerable<Libro> Libri
            {
                get { return _libri.Values; }
            }

            public IEnumerable<Persona> Persone
            {
                get { return _persone.Values; }
            }

            public Libro NuovoLibro()
            {
                int nextId = _libri.Count == 0 ? 1 : _libri.Keys.Max() + 1;
                return new Libro(nextId);
            }

            public Persona NuovaPersona()
            {
                int nextId = _persone.Count == 0 ? 1 : _persone.Keys.Max() + 1;
                return new Persona(nextId);
            }

            public void Aggiungi(Libro libro)
            {
                if (libro == null)
                    throw new ArgumentNullException("libro");
                _libri.Add(libro.Id, libro);
            }

            public void Aggiungi(Persona persona)
            {
                if (persona == null)
                    throw new ArgumentNullException("persona");
                _persone.Add(persona.Id, persona);
            }

            private Libro CreaLibroRandom()
            {
                Libro libro = NuovoLibro();
                libro.Titolo = "TitoloLibro_" + libro.Id;
                libro.Autore = "Autore_" + libro.Id;
                libro.Editore = "Editore_" + libro.Id;
                libro.AnnoDiPubblicazione = 1950 + libro.Id;
                return libro;
            }

            private Persona CreaPersonaRandom()
            {
                Persona persona = NuovaPersona();
                persona.Nome = "NomePersona_" + persona.Id;
                return persona;
            }
        }
    }
}