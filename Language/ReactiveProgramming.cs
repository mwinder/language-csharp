using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Language
{
    class ReactiveProgramming
    {
        static void Main()
        {
            var gru = new Gru();

            using (gru.Subscribe(new Minion("Tim")))
            using (gru.Subscribe(new Minion("Bob")))
            using (gru.Subscribe(new Minion("Phil")))
            using (gru.Where(x => x.Message.Contains("Moon")).Subscribe(new Minion("REACTIVE")))
            {
                gru.Start();
            }

            Console.ReadLine();
        }
    }

    public class Speech
    {
        public string Message { get; set; }
    }

    public class Gru : IObservable<Speech>
    {
        private readonly List<IObserver<Speech>> observers = new List<IObserver<Speech>>();

        public IDisposable Subscribe(IObserver<Speech> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Subscription(observers, observer);
        }

        public void Start()
        {
            foreach (var message in new[] { "Light-bulb", "We are going to steal the Moon!!!", "It is disintegrated, by definition it cannot be fixed" })
            {
                Publish(message);
            }
        }

        private void Publish(string message)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(new Speech { Message = message });
            }
        }

        private class Subscription : IDisposable
        {
            private readonly List<IObserver<Speech>> observers;
            private readonly IObserver<Speech> observer;

            public Subscription(List<IObserver<Speech>> observers, IObserver<Speech> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }

            public void Dispose()
            {
                if (observer != null && observers.Contains(observer))
                    observers.Remove(observer);
            }
        }
    }

    public class Minion : IObserver<Speech>
    {
        private readonly string name;

        public Minion(string name)
        {
            this.name = name;
        }

        public void OnNext(Speech value)
        {
            Console.WriteLine(name + " heard message: {0}", value.Message);
        }

        public void OnError(Exception exception)
        {
            Console.WriteLine(name + " Error: {0}", exception.Message);
        }

        public void OnCompleted()
        {
            Console.WriteLine(name + " Completed");
        }
    }
}
