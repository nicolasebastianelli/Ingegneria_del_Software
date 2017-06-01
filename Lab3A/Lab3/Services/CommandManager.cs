using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lab3.Services
{
    static class CommandManager
    {
        private static readonly Dictionary<string, Action> _actions = new Dictionary<string, Action>();

        //  Permette di registrare automaticamente tutti i comandi contenuti in voci dei menù e bottoni delle toolstrip di una form.
        public static void RegisterTarget(Form target)
        {
            InitializeCommandHandlers(target.Controls);
        }

        //  Permette di associare a un comando un'azione da eseguire
        public static void RegisterCommand(string command, Action action)
        {
            //  Verificare:
            //    che il comando non sia una stringa nulla o vuota e che non contenga spazi bianchi
            //    che l'azione non sia nulla
            //  Se il dizionario contiene già il comando, 'aggiungere' la nuova azione al comando
            //  altrimenti, inserire il nuovo comando e la corrispondente azione
            //  Visualizzare nella finestra di output del debugger il comando e il metodo da invocare
            //    in particolare, il nome della classe che contiene il metodo e il nome del metodo
            //    ad esempio: "RegisterCommand InserisciNuovoLibro -> DocumentServices.InserisciNuovoLibro"

            if(String.IsNullOrEmpty(command)||command.Contains(" ")){
                throw new ArgumentNullException("Stringa nulla o vuota contenente spazi bianchi");
            }
            if (action == null)
            {
                throw new ArgumentNullException("Azione nulla");
            }
            if (_actions.ContainsKey(command))
            {
                _actions[command] += action;
            }
            else
            {
                _actions.Add(command, action);
            }
            Console.WriteLine("RegisterCommand {0} -> {1}.{2}" ,command , action.Method.DeclaringType.Name, action.Method.Name);
        }

        //  Permette di eseguire le azioni correntemente associate al comando
        public static void DoCommand(string command)
        {
            //  Visualizzare nella finestra di output del debugger il comando da eseguire
            //    ad esempio: "DoCommand InserisciNuovoLibro"
            //  Se il comando esiste, eseguire il comando
            Console.WriteLine("DoCommand {0}", command);
            if (_actions.ContainsKey(command))
            {
                _actions[command].Invoke();
            }
        }

        private static void InitializeCommandHandlers(Control.ControlCollection controls)
        {
            //  Per ogni control contenuto in controls
            //    se control è un ToolStrip
            //      invocare InitializeCommandHandlers passando come argomento la collezione degli item contenuti nel ToolStrip
            //    altrimenti
            //      invocare InitializeCommandHandlers passando come argomento la collezione dei controlli contenuti in control
            foreach (Control control in controls)
            {
                if (control is ToolStrip)
                {
                    InitializeCommandHandlers((control as ToolStrip).Items);
                }
                else
                {
                    InitializeCommandHandlers(control.Controls);
                }
            }
            
        }

        private static void InitializeCommandHandlers(ToolStripItemCollection items)
        {
            //  Per ogni ToolStripItem in items
            //    se il Tag è una stringa (è un comando)
            //      collegare all'evento Click dell'item il gestore Item_Click
            //      visualizzare nella finestra di output del debugger il comando da eseguire
            //        ad esempio: "InitializeCommandHandler for InserisciNuovoLibro"
            //    se è un ToolStripMenuItem
            //      andare in ricorsione passando le sotto voci del menu (proprietà DropDownItems)

            foreach(ToolStripItem item in items){
                if (item.Tag is string)
                {
                    item.Click += Item_Click;
                    Console.WriteLine("InitializeCommandHandler for {0}", item.Tag);
                }
                if (item is ToolStripMenuItem)
                {
                    InitializeCommandHandlers((item as ToolStripMenuItem).DropDownItems);
                }
            }
        }

        private static void Item_Click(object sender, EventArgs e)
        {
            //  Lanciare un'eccezione se il Tag del sender non è una stringa
            //  Eseguire il comando memorizzato nel Tag del sender

            if( !((sender as ToolStripItem).Tag is String))
                throw new ArgumentNullException("Tag not a string");
            DoCommand((sender as ToolStripItem).Tag as String);
        }
    }
}
