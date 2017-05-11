using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    class GestorePrestiti
    {
        private readonly Dictionary<Libro,Persona> _prestiti = new Dictionary<Libro,Persona>();

        public Libro[] LibriInPrestito
        {
            get { return _prestiti.Keys.ToArray(); }
        }
        public bool IsLibroDisponibile(Libro libro){
            return ! _prestiti.ContainsKey(libro);
        }

        public Persona PossessoreLibro(Libro libro){

            if(IsLibroDisponibile(libro)){
                return null;
            }
            else{
                return _prestiti[libro];
            }

        }

        public void GestisciRichiesta(Libro libro,Persona persona){
            if(IsLibroDisponibile(libro)){
                _prestiti.Add(libro,persona);
                Console.WriteLine("Prestito libro '"+libro.Titolo+"' effettuato a "+persona.Nome);
            }
            else{
                Console.WriteLine("Prestito libro '"+libro.Titolo+"' NON effettuato a "+persona.Nome);
            }
        }

        public void GestisciConsegna(Libro libro,Persona persona){
            if(PossessoreLibro(libro)!= persona){
                 throw new ArgumentException("Consegna libro '"+libro.Titolo+"' NON effettuato da parte di "+persona.Nome);
            }
            else{
                 _prestiti.Remove(libro);
                 Console.WriteLine("Consegna libro '"+libro.Titolo+"' effettuato da parte di "+persona.Nome);
            }
        }

        
    }
}
