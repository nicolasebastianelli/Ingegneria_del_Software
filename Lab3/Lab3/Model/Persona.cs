using System;

namespace Lab3.Model
{
    class Persona
    {
        private readonly int _id;
        private string _nome = String.Empty;
        private string _indirizzo = String.Empty;
        private string _telefono = String.Empty;

        public Persona(int id)
        {
            _id = id;
        }

        [Editable("Identificatore", Width = 30)]
        public int Id
        {
            get { return _id; }
        }

        [Editable("Nome")]
        public string Nome
        {
            get { return _nome; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Il nome della persona è obbligatorio");
                _nome = value;
            }
        }

        [Editable("Indirizzo")]
        public string Indirizzo
        {
            get { return _indirizzo; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Valore nullo non accettabile");
                _indirizzo = value;
            }
        }

        [Editable("Numero di telefono")]
        public string Telefono
        {
            get { return _telefono; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Valore nullo non accettabile");
                _telefono = value;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} [{1}]", Nome, Id);
        }
    }
}
