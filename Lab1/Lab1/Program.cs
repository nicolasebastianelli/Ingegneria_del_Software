using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            GestorePrestiti gestorePrestiti = new GestorePrestiti();
            Biblioteca biblioteca = new Biblioteca();
            EventsGenerator eventsGenerator = new EventsGenerator(biblioteca,gestorePrestiti);
            eventsGenerator.Richiesta += LogRichiesta;
            eventsGenerator.Consegna += LogConsegna;
            eventsGenerator.Richiesta += gestorePrestiti.GestisciRichiesta;
            eventsGenerator.Consegna += gestorePrestiti.GestisciConsegna;
            Console.WriteLine("Press the Enter key to exit the program.");
            Console.ReadLine();
        }

        static void LogRichiesta(Libro libro, Persona persona)
        {
            Console.WriteLine();
            Console.WriteLine(persona.Nome + " vorrebbe prendere in prestito \"" + libro.Titolo + "\"");
        }

        static void LogConsegna(Libro libro, Persona persona)
        {
            Console.WriteLine();
            Console.WriteLine(persona.Nome + " consegna \"" + libro.Titolo + "\"");
        }
    }
}
