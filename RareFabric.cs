using System;
using System.Collections.Generic;
using System.Text;

namespace Sidit.Randomizer
{
    public static class RareFabric
    {
        public static RareValue<T> Create<T>(float chance, T value) => new RareValue<T>(chance, value);

        public static RareValue<T>[] CreateArray<T>(params RareValue<T>[] array) => array;

        public static RareValue<Action> CreateAction(float chance, Action action) => new RareValue<Action>(chance, action);

        public static RareValue<Func<TValue>>CreateFunction<TValue>(float chance, Func<TValue> func) => new RareValue<Func<TValue>>(chance, func);
    }
}
