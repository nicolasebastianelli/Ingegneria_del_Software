using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lab3.Services
{
    class EventsGenerator
    {
        private readonly Timer _timer = new Timer();
        private readonly string[] _comandiGenerabili = new string[]
        {
            "RichiestaRandom",
            //"RichiestaRandom",
            //"RichiestaRandom",
            //"RichiestaRandom",
            //"RichiestaRandom",
            "ConsegnaRandom",
            //"ConsegnaRandom",
            //"ConsegnaRandom",
            //"ConsegnaRandom",
            //"ConsegnaRandom",
            //"ModificaLibroRandom",
            //"ModificaPersonaRandom",
        };

        public EventsGenerator()
        {
            _timer.Interval = 1000;
            _timer.Tick += OnTimedEvent;
            CommandManager.RegisterCommand("Start", Start);
            CommandManager.RegisterCommand("Stop", Stop);
        }

        private void Start()
        {
            _timer.Start();
        }

        private void Stop()
        {
            _timer.Stop();
        }

        private void OnTimedEvent(object source, EventArgs e)
        {
            Stop();
            string command = _comandiGenerabili.GetRandomElementFrom();
            CommandManager.DoCommand(command);
            Start();
        }
    }
}
