using System;
using System.Collections.Generic;

namespace DigitalCircuit.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var theAgenda = new Agenda();

            var input = new Wire();

            Console.WriteLine(
                String.Format(
                "Initial input signal value = {0}", input._Signal));

            var output = new Wire();
            var inverter = new Inverter(input, output, theAgenda);

            inverter._Delay = 5;

            inverter.InvertInput(); 


            Console.WriteLine(
                String.Format(
                "input signal value = {0}", input._Signal, "after Inverting input."));
        }
    }

    public class Agenda
    {
        public int CurrentTime { get; set; }

        public Agenda()
        {
            CurrentTime = 0;
        }

        internal void AddToAgenda(int v, Action action)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeSegment 
    {
        public int _SegmentTime;
        public Queue<Action> _SegmentQueue; 
    }

    public class Wire
    {
        public int _Signal;

        public List<Action> _Actions { get; }

        public Wire()
        {
            _Signal = InitialSignal();
            _Actions = new List<Action>();
        }

        private int InitialSignal()
        {
            return 0; 
        }

        public void SetSignal(int newSignal)
        {
            if (_Signal != newSignal)
            {
                _Signal = newSignal;
                CallEachAction();
            }
        }
        public void AddAction(Action action)
        {
            _Actions.Insert(0, action); // Ensures LIFO
        }

        private void CallEachAction()
        {
            foreach (var action in _Actions)
            {
                action.Invoke();
            }
        }
    }

    public class Inverter
    {
        public Wire _Input { get; }
        public Wire _Output { get; }
        public int _Delay { get; internal set; }
        private Agenda _Agenda;

        public Inverter(Wire input, Wire output, Agenda agenda)
        {
            _Input = input;
            _Output = output;
            _Input.AddAction(InvertInput);
            _Agenda = agenda; 
        }

        public void InvertInput()
        {
            var newSignal = LogicalNot(_Input._Signal);
            AfterDelay(_Delay, () => _Output.SetSignal(newSignal));
        }

        private void AfterDelay(int delay, Action action)
        {
            _Agenda.AddToAgenda(_Agenda.CurrentTime + delay, action);
        }

        private int LogicalNot(int signal)
        {
            if (signal == 0)
            {
                return 1;
            }
            else if (signal == 1)
            {
                return 0;
            }
            else
            {
                throw new ArgumentException("signal must be either a 1 or a 0");
            }
        }
    }
}
