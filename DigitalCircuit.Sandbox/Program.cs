using System;
using System.Collections.Generic;
using System.Linq;

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
                "Initial input signal value = {0}", input.Signal));

            var output = new Wire();
            var inverter = new Inverter(input, output, theAgenda);

            inverter.Delay = 5;

            inverter.InvertInput(); 


            Console.WriteLine(
                String.Format(
                "input signal value = {0}", input.Signal, "after Inverting input."));
        }
    }

    public class Agenda
    {
        public int CurrentTime { get; set; }
        public List<TimeSegment> Segments { get; set; }

        public Agenda()
        {
            CurrentTime = 0;
            Segments = new List<TimeSegment>();
        }

        public List<TimeSegment> GetTimeSegments()
        {
            return Segments.OrderBy(seg => seg.SegmentTime).ToList();
        }

        public TimeSegment FirstSegment()
        {
            return GetTimeSegments().First();
        }

        public List<TimeSegment> RestOfSegments() 
        {
            return GetTimeSegments().Skip(1).ToList();
        }


        internal void AddToAgenda(int delay, Action action)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeSegment 
    {
        public int SegmentTime { get; set; }
        public Queue<Action> SegmentQueue { get; set; } 
    }

    public class Wire
    {
        public int Signal { get; set; } 

        public List<Action> Actions { get; }

        public Wire()
        {
            Signal = InitialSignal();
            Actions = new List<Action>();
        }

        private int InitialSignal()
        {
            return 0; 
        }

        public void SetSignal(int newSignal)
        {
            if (Signal != newSignal)
            {
                Signal = newSignal;
                CallEachAction();
            }
        }
        public void AddAction(Action action)
        {
            Actions.Insert(0, action); // Ensures LIFO
        }

        private void CallEachAction()
        {
            foreach (var action in Actions)
            {
                action.Invoke();
            }
        }
    }

    public class Inverter
    {
        private Wire _input; 
        private Wire _output; 
        private Agenda _agenda;
        public int Delay { get; set; }

        public Inverter(Wire input, Wire output, Agenda agenda)
        {
            _input = input;
            _output = output;
            _agenda = agenda; 
            _input.AddAction(InvertInput);
        }

        public void InvertInput()
        {
            var newSignal = LogicalNot(_input.Signal);
            AfterDelay(Delay, () => _output.SetSignal(newSignal)); 
        }

        private void AfterDelay(int delay, Action action)
        {
            _agenda.AddToAgenda(_agenda.CurrentTime + delay, action);
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
