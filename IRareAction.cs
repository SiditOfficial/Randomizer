using System;
using System.Collections.Generic;
using System.Text;

namespace Sidit.Randomizer
{
    public interface IRareAction
    {
        public float Chance { get; }
        public void Invoke();
    }
}
