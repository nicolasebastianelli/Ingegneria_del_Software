using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    class Libro
    {
        private readonly string _titolo;
    
        public Libro(string titolo)
        {
            if (String.IsNullOrEmpty(titolo))
                throw new ArgumentException("String.IsNullOrEmpty(titolo)");
            _titolo = titolo;
        }

        public string Titolo
        {
            get { return _titolo; }
        } 
    }
}
