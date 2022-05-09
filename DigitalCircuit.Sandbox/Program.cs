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

            //inverter.InvertInput();

            input.SetSignal(1);

            theAgenda.Propogate();

            Console.WriteLine(
                String.Format(
                "output signal value = {0}", output.Signal, "after Inverting input."));
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

        public bool IsAgendaEmpty() 
        {
            if (Segments == null) 
            {
                return true;
            }
            return !Segments.Any();
        }

        public bool BelongsBefore(int time, TimeSegment segment)
        {
            return segment == null || time < segment.SegmentTime;
        }

        public TimeSegment MakeNewTimeSegment(int time, Action action) 
        {
            var q =  new Queue<Action>();
            q.Enqueue(action);

            return new TimeSegment() { 
                SegmentTime = time, 
                SegmentQueue = q
            };
        }

        public void AddToSegments(int time, Action action) 
        {
            var segments = GetTimeSegments();
            int index = 0;

            if (segments.Count == 0)
            {
                segments.Add(MakeNewTimeSegment(time, action));
            }

            else
            {
                foreach (var segment in segments)
                {
                    if (segment.SegmentTime == time)
                    {
                       segment.SegmentQueue.Enqueue(action);
                        break;
                    }
                    else if (BelongsBefore(time, segment))
                    {
                        segments.Insert(index, MakeNewTimeSegment(time, action));
                        break;
                    }
                    else if (index == segments.Count)
                    {
                        segments.Add(MakeNewTimeSegment(time, action));
                        break;
                    }
                    else
                    {
                        index += 1;
                    }
                }
            }

            Segments = segments;

        }

        public void AddToAgenda(int time, Action action)
        {
            AddToSegments(time, action);
        }

        public void RemoveFirstAgendaItem() 
        {
            var q = GetTimeSegments().First().SegmentQueue;
            q.Dequeue();

            if (IsEmpty(q))
            {
                Segments = RestOfSegments(); 
            }

        }

        public bool IsEmpty(IEnumerable<Action> q)
        {
            if (q == null) 
            {
                return true;
            }
 
            return !q.Any();
        }

        public Action FirstAgendaItem() 
        {
            if (IsAgendaEmpty())
            {
                throw new Exception("Agenda is empty -- FIRST-AGENDA-ITEM");
            }

            var firstSeg = FirstSegment();
            CurrentTime = firstSeg.SegmentTime;
            return firstSeg.SegmentQueue.First();

        }

        public void Propogate() 
        {
            if (IsAgendaEmpty())
            {
                Console.WriteLine("Done!");
            }
            else
            {
                var firstItem = FirstAgendaItem();
                firstItem();
                RemoveFirstAgendaItem();
                Propogate();
            }
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
