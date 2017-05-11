using System;
using System.Collections.Generic;

namespace Lab2
{
    class Biblioteca
    {
        private readonly List<Libro> _libri = new List<Libro>();
        private readonly List<Persona> _persone = new List<Persona>();

        public Biblioteca()
        {
            Libri.Add(new Libro("Guerra e pace"));
            Libri.Add(new Libro("Il signore degli anelli"));
            Libri.Add(new Libro("Iliade"));
            Libri.Add(new Libro("Apologia di Socrate"));
            Libri.Add(new Libro("Le affinità elettive"));

            Persone.Add(new Persona("Pippo"));
            Persone.Add(new Persona("Topolino"));
            Persone.Add(new Persona("Paperino"));
            Persone.Add(new Persona("Gastone"));
            Persone.Add(new Persona("Nonna Papera"));
        }

        public List<Libro> Libri
        {
            get { return _libri; }
        }

        public List<Persona> Persone
        {
            get { return _persone; }
        }
    }
}
