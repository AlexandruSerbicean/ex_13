#nullable enable
using System;
using System.Threading.Tasks;

namespace InfraSim.Services
{
    public class UserCounter
    {
        public int Counter { get; private set; } = 1;
        public event Action? OnCounterChanged;

        public async Task StartIncrementing()
        {
            while (Counter < 200000)
            {
                Counter += 1000; 
                OnCounterChanged?.Invoke();
                await Task.Delay(10); 
            }
            
            Counter = 200000; 
            OnCounterChanged?.Invoke();
        }
    }
}
