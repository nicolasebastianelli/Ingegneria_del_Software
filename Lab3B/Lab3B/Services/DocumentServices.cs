using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

using Lab3.Model;
using Lab3.Presentation;

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

            #region ----- TODO -----

            Libro libro = Documento.NuovoLibro();
            if (Modifica(libro))
            {
                Documento.Aggiungi(libro);
            }

            #endregion
        }

        public static void InserisciNuovaPersona()
        {
            //  Creare una nuova persona (servizio di Document)
            //  Invocare il metodo Modifica
            //  In caso di successo, aggiungere la persona alla biblioteca (servizio di Document)

            #region ----- TODO -----

            Persona persona = Documento.NuovaPersona();
            if (Modifica(persona))
            {
                Documento.Aggiungi(persona);
            }

            #endregion
        }

        public static void ModificaLibro()
        {
            //  Selezionare il libro che si vuole modificare (SelezionaDa)
            //  Invocare il metodo Modifica
            //  In caso di successo, Invocare il metodo Modifica di Document

            #region ----- TODO -----

            Libro libro = SelezionaDa(Documento.Libri);
            if (Modifica(libro))
            {
                Documento.Modifica(libro);
            }

            #endregion
        }

        public static void ModificaPersona()
        {
            //  Selezionare la persona che si vuole modificare (SelezionaDa)
            //  Invocare il metodo Modifica
            //  In caso di successo, Invocare il metodo Modifica di Document

            #region ----- TODO -----

            Persona persona = SelezionaDa(Documento.Persone);
            if (Modifica(persona))
            {
                Documento.Modifica(persona);
            }

            #endregion
        }

        public static bool Modifica<T>(T item)
            where T : class
        {
            //  Se item non è null
            //    creare una EditingDialog
            //    creare un corrispondente EditingDialogPresenter
            //    invocare il metodo SetEditableObject dell'EditingDialogPresenter
            //    visualizzare la EditingDialog e, in caso di successo, restituire true

            #region ----- TODO -----

            if (item != null)
            {
                using (EditingDialog editingDialog = new EditingDialog())
                {
                    EditingDialogPresenter editingDialogPresenter = new EditingDialogPresenter(editingDialog);
                    editingDialogPresenter.SetEditableObject(item);
                    if (editingDialog.ShowDialog() == DialogResult.OK)
                    {
                        return true;
                    }
                }
            }

            #endregion

            return false;
        }

        public static T SelezionaDa<T>(IEnumerable<T> items)
            where T : class
        {
            //  Creare una SelectDialog
            //  Caricare sulla SelectDialog gli items
            //  Visualizzare la SelectDialog e, in caso di successo, restituire l'oggetto selezionato dall'utente

            #region ----- TODO -----

            using (SelectDialog selectDialog = new SelectDialog())
            {
                selectDialog.LoadItems(items);
                if (selectDialog.ShowDialog() == DialogResult.OK)
                    return (T) selectDialog.SelectedItem;
            }

            #endregion

            return null;
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
