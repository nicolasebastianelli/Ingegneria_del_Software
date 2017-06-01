using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Lab3.Model;
using Lab3.Services;

namespace Lab3.Presentation
{
    public class MainFormPresenter
    {
        private readonly MainForm _target;

        public MainFormPresenter(MainForm target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            _target = target;
            CommandManager.RegisterTarget(Target);
            CommandManager.RegisterCommand("Start", Start);
            CommandManager.RegisterCommand("Stop", Stop);
            new EntitiesPresenter(Target.TreeView1, Documento, GetPersoneAsEntities);
            new EntitiesPresenter(Target.TreeView2, Documento, GetLibriAsEntities);
        }

        public MainForm Target
        {
            get { return _target; }
        }

        private IEnumerable<IEntity> GetPersoneAsEntities()
        {
            return from persona in Documento.Persone
                   select new PersonaAdapter(persona);

            //List<IEntity> entities = new List<IEntity>();
            //foreach (Persona persona in Documento.Persone)
            //{
            //    entities.Add(new PersonaAdapter(persona));
            //}
            //return entities;
        }

        private IEnumerable<IEntity> GetLibriAsEntities()
        {
            return from libro in Documento.Libri
                   select new LibroAdapter(libro);

            //List<IEntity> entities = new List<IEntity>();
            //foreach (Libro libro in Documento.Libri)
            //{
            //    entities.Add(new LibroAdapter(libro));
            //}
            //return entities;
        }

        private void Start()
        {
            Target.UpdateUI(true);
        }

        private void Stop()
        {
            Target.UpdateUI(false);
        }

        private static Documento Documento
        {
            get { return Documento.GetInstance(); }
        }
    }
}
