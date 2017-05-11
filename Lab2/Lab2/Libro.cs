using System;

namespace Lab2
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
            get
            {
                return _titolo;
            }
        }
    }
}
