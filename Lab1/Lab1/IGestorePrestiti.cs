using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    interface IGestorePrestiti
    {
        Libro[] LibriInPrestito { get; }
        bool IsLibroDisponibile(Libro libro);
        Persona PossessoreLibro(Libro libro);
        void GestisciRichiesta(Libro libro, Persona persona);
        void GestisciConsegna(Libro libro, Persona persona);
    }
}
