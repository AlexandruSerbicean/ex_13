#nullable enable
using System;
using System.Threading.Tasks;

namespace InfraSim.Services
{
    public class UserCounter
    {
        public int Counter { get; private set; } = 0;

        private bool _isRunning = false;

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
            Counter = 0;
            OnCounterChanged?.Invoke();

            while (Counter < 200_000)
            {
                Counter += 1_000;
                OnCounterChanged?.Invoke();
                await Task.Delay(40); // smooth
            }

            Counter = 200_000;
            OnCounterChanged?.Invoke();
            _isRunning = false;
        }
    }
}
