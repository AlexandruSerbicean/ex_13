#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfraSim.Pages.Models.Observer;

namespace InfraSim.Services
{
    public class UserCounter : ISubject
    {
        public int Counter { get; private set; } = 0;

        private bool _isRunning = false;
        private bool _canceled = false;

        public event Action? OnCounterChanged;

        private readonly List<IObserver> _observers = new();

        public void RegisterObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
                observer.Update(Counter);
        }

        public void ResetCounter()
        {
            Counter = 0;
            OnCounterChanged?.Invoke();
            NotifyObservers();
        }

        public async Task StartIncrementing()
        {
            if (_isRunning) return;

            _isRunning = true;
            _canceled = false;
            Counter = 0;
            OnCounterChanged?.Invoke();
            NotifyObservers();

            while (Counter < 200_000)
            {
                if (_canceled) break;

                Counter += 1000;
                OnCounterChanged?.Invoke();
                NotifyObservers();
                await Task.Delay(40);
            }

            Counter = Math.Min(Counter, 200_000);
            OnCounterChanged?.Invoke();
            NotifyObservers();
            _isRunning = false;
        }

        public void Cancel() => _canceled = true;
    }
}
