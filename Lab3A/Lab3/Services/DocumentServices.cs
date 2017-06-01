using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Lab3.Model;
using Lab3.Presentation;
using System.Windows.Forms;

namespace Lab3.Services
{
    static class DocumentServices
    {
        public static void RegisterCommands()
        {
            CommandManager.RegisterCommand("InserisciNuovoLibro", InserisciNuovoLibro);
            CommandManager.RegisterCommand("InserisciNuovaPersona", InserisciNuovaPersona);
            CommandManager.RegisterCommand("ModificaLibro", ModificaLibro);
            CommandManager.RegisterCommand("ModificaPersona", ModificaPersona);

            CommandManager.RegisterCommand("RichiestaRandom", RichiestaRandom);
            CommandManager.RegisterCommand("ConsegnaRandom", ConsegnaRandom);
            CommandManager.RegisterCommand("ModificaLibroRandom", ModificaLibroRandom);
            CommandManager.RegisterCommand("ModificaPersonaRandom", ModificaPersonaRandom);
        }

        public static void InserisciNuovoLibro()
        {
            //  Creare un nuovo libro (servizio di Document)
            //  Invocare il metodo Modifica
            //  In caso di successo, aggiungere il libro alla biblioteca (servizio di Document)

            Libro libro = Documento.NuovoLibro();
            if (Modifica<Libro>(libro))
                Documento.Aggiungi(libro);
        }

        public static void InserisciNuovaPersona()
        {
            //  Creare una nuova persona (servizio di Document)
            //  Invocare il metodo Modifica
            //  In caso di successo, aggiungere la persona alla biblioteca (servizio di Document)
            Persona persona = Documento.NuovaPersona();
            if (Modifica<Persona>(persona))
                Documento.Aggiungi(persona);
         }

        public static void ModificaLibro()
        {
            //  Selezionare il libro che si vuole modificare (SelezionaDa)
            //  Invocare il metodo Modifica
            //  In caso di successo, Invocare il metodo Modifica di Document
            Libro libro = SelezionaDa<Libro>(Documento.Libri);
            if(Modifica<Libro>(libro))
                Documento.Modifica(libro);

            
        }

        public static void ModificaPersona()
        {
            //  Selezionare la persona che si vuole modificare (SelezionaDa)
            //  Invocare il metodo Modifica
            //  In caso di successo, Invocare il metodo Modifica di Document

            Persona persona = SelezionaDa<Persona>(Documento.Persone);
            if (Modifica<Persona>(persona))
                Documento.Modifica(persona);
        }

        public static bool Modifica<T>(T item)
            where T : class
        {
            //  Se item non è null
            //    creare una EditingDialog
            //    visualizzare la EditingDialog e, in caso di successo, restituire true

            if (item != null)
            {

                using (EditingDialog editingDialog = new EditingDialog())
                {
                    DialogResult dr = editingDialog.ShowDialog();
                    if (dr == DialogResult.OK)
                        return true;
                }
            }
            return false;
        }

        public static T SelezionaDa<T>(IEnumerable<T> items)
            where T : class
        {
            //  Creare una SelectDialog
            //  Caricare sulla SelectDialog gli items
            //  Visualizzare la SelectDialog e, in caso di successo, restituire l'oggetto selezionato dall'utente

            using (SelectDialog selectDialog = new SelectDialog())
            {
                selectDialog.LoadItems(items);
                DialogResult dr = selectDialog.ShowDialog();
                if (dr == DialogResult.OK)
                    return (selectDialog.SelectedItem as T);
                else
                    return null;
            }
        }

        #region Servizi invocati random da EventsGenerator

        public static void RichiestaRandom()
        {
            Libro libro = Documento.Libri.GetRandomElementFrom();
            Persona persona = Documento.Persone.GetRandomElementFrom();
            if (libro != null && persona != null)
                Documento.GestisciRichiesta(libro, persona);
        }

        public static void ConsegnaRandom()
        {
            Libro libro = Documento.LibriInPrestito.GetRandomElementFrom();
            if (libro != null)
                Documento.GestisciConsegna(libro, Documento.PossessoreLibro(libro));
        }

        public static void ModificaLibroRandom()
        {
            Libro libro = Documento.Libri.GetRandomElementFrom();
            if (libro != null)
            {
                if (Modifica(libro))
                {
                    Documento.Modifica(libro);
                }
            }
        }

        public static void ModificaPersonaRandom()
        {
            Persona persona = Documento.Persone.GetRandomElementFrom();
            if (persona != null)
            {
                if (Modifica(persona))
                {
                    Documento.Modifica(persona);
                }
            }
        }

        #endregion

        private static Documento Documento
        {
            get { return Documento.GetInstance(); }
        }
    }
}
