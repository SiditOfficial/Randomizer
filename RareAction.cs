using System;

namespace Sidit.Randomizer
{
    public class RareAction : IRareAction
    {
        public float Chance { get; }
        private readonly Action _action;

        public RareAction(float chance, Action action)
        {
            Chance = chance;
            _action = action;
        }

        public void Invoke() => _action?.Invoke();
    }
}
