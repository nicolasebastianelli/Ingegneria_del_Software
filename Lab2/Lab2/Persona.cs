using System;

namespace Lab2
{
    class Persona
    {
        private readonly string _nome;

        public Persona(string nome)
        {
            if (String.IsNullOrEmpty(nome))
                throw new ArgumentException("String.IsNullOrEmpty(nome)");
            _nome = nome;
        }

        public string Nome
        {
            get
            {
                return _nome;
            }
        }
    }
}
