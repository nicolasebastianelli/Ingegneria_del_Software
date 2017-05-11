using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    class Biblioteca
    {
        private List<Libro> _libri = new List<Libro>();
        private List<Persona> _persone = new List<Persona>();

        public Biblioteca()
        {
            for (int i = 0; i < 5; i++)
            {
                Libri.Add(new Libro("Libro " + i));
                Persone.Add(new Persona("Persona " + i));
            }
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
