#nullable enable
using System;
using System.Threading.Tasks;

namespace InfraSim.Services
{
    public class UserCounter
    {
        public int Counter { get; private set; } = 0;

        private bool _isRunning = false;
        private bool _canceled = false; 

        public event Action? OnCounterChanged;

        public void ResetCounter()
        {
            Counter = 0;
            OnCounterChanged?.Invoke();
        }

        public async Task StartIncrementing()
        {
            if (_isRunning) return;

            _isRunning = true;
            _canceled = false; 
            Counter = 0;
            OnCounterChanged?.Invoke();

            while (Counter < 200_000)
            {
                if (_canceled) break; 

                Counter += 1_000;
                OnCounterChanged?.Invoke();
                await Task.Delay(40);
            }

            Counter = Math.Min(Counter, 200_000);
            OnCounterChanged?.Invoke();
            _isRunning = false;
        }

        public void Cancel() => _canceled = true; 
    }
}
