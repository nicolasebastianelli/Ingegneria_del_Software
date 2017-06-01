using System;

namespace Lab3.Model
{
    class Libro
    {
        private readonly int _id;
        private string _titolo = String.Empty;
        private string _autore = String.Empty;
        private string _editore = String.Empty;
        private int _anno;

        public Libro(int id)
        {
            _id = id;
        }

        [Editable("Identificatore", Width = 30)]
        public int Id
        {
            get { return _id; }
        }

        [Editable("Titolo")]
        public string Titolo
        {
            get { return _titolo; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Il titolo del libro è obbligatorio");
                _titolo = value;
            }
        }

        [Editable("Autore")]
        public string Autore
        {
            get { return _autore; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("L'autore del libro è obbligatorio");
                _autore = value;
            }
        }

        [Editable("Editore")]
        public string Editore
        {
            get { return _editore; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("L'editore del libro è obbligatorio");
                _editore = value;
            }
        }

        [Editable("Anno di pubblicazione", Width = 50)]
        public int AnnoDiPubblicazione
        {
            get { return _anno; }
            set
            {
                if (value < 1800 || value > DateTime.Today.Year)
                    throw new ArgumentException("Valore dell'anno non accettabile");
                _anno = value;
            }
        }

        [Editable("Anni di 'anzianità'", Width = 50)]
        public int AnniDiAnzianità
        {
            get { return DateTime.Today.Year - AnnoDiPubblicazione; }
        }

        [Editable("Disponibile", Width = 40)]
        public bool Disponibile
        {
            get { return Documento.GetInstance().IsLibroDisponibile(this); }
        }

        public override string ToString()
        {
            return String.Format("{0} [{1}]", Titolo, Id);
        }
    }
}
