using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Random = System.Random;
using System.Collections;

namespace Randomizers
{
    /// <summary>
    /// Thread save static randomizer
    /// </summary>
    public static class Randomizer
    {
        private static int _seed = Environment.TickCount;
        private readonly static ThreadLocal<Random> _threadLocalRandomProvider = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

        private static int NextInt(int min, int max) => _threadLocalRandomProvider.Value.Next(min, max);
        private static void NextBytes(byte[] buffer) => _threadLocalRandomProvider.Value.NextBytes(buffer);
        private static double NextDouble() => _threadLocalRandomProvider.Value.NextDouble();

        /// <returns>0 or 1</returns>
        public static int GetBit() => NextInt(0, 2);
        /// <returns>true or false</returns>
        public static bool GetBool() => GetBit() == 1;
        /// <returns>1 or -1</returns>
        public static int GetPosNeg() => GetBool() ? 1 : -1;

        /// <summary>
        /// Sets a random bytes in buffer
        /// </summary>
        public static void SetBytes(byte[] buffer) => NextBytes(buffer);
        /// <returns>
        /// Buffer with random bytes
        /// </returns>
        public static byte[] GetBytes(int count)
        {
            var buffer = new byte[count];
            NextBytes(buffer);
            return buffer;
        }

        /// <returns>
        /// A random 8-bit unsigned integer, not including the max value (255)
        /// <br/>0 &#60;= returnsValue &#60; 255
        /// </returns>
        public static byte GetByte() => GetByte(byte.MinValue, byte.MaxValue);
        /// <returns>
        /// A random 8-bit unsigned integer in a certian range, not including the max value
        /// <br/>minValue &#60;= returnsValue &#60; maxValue
        /// </returns>
        public static byte GetByte(byte minValue, byte maxValue) => (byte)NextInt(minValue, maxValue);

        /// <returns>
        /// A random 8-bit signed integer, not including the max value (172)
        /// <br/>-128 &#60;= returnsValue &#60; 127
        /// </returns>
        public static sbyte GetSByte() => GetSByte(sbyte.MinValue, sbyte.MaxValue);
        /// <returns>
        /// A random 8-bit signed integer in a certian range, not including the max value
        /// <br/>minValue &#60;= returnsValue &#60; maxValue
        /// </returns>
        public static sbyte GetSByte(sbyte minValue, sbyte maxValue) => (sbyte)NextInt(minValue, maxValue);



        /// <returns>
        /// A random 16-bit signed integer, not including the max value (32767)
        /// <br/>-32768 &#60;= returnsValue &#60; 32767
        /// </returns>
        public static short GetShort() => GetShort(short.MinValue, short.MaxValue);
        /// <returns>
        /// A random 16-bit signed integer in a certian range, not including the max value
        /// <br/>minValue &#60;= returnsValue &#60; maxValue
        /// </returns>
        public static short GetShort(short minValue, short maxValue) => (short)NextInt(minValue, maxValue);

        /// <returns>
        /// A random 16-bit unsigned integer, not including the max value (65535)
        /// <br/>0 &#60;= returnsValue &#60; 65535
        /// </returns>
        public static ushort GetUShort() => GetUShort(ushort.MinValue, ushort.MaxValue);
        /// <returns>
        /// A random 16-bit unsigned uninteger in a certian range, not including the max value
        /// <br/>minValue &#60;= returnsValue &#60; maxValue
        /// </returns>
        public static ushort GetUShort(ushort minValue, ushort maxValue) => (ushort)NextInt(minValue, maxValue);



        /// <returns>
        /// A random 32-bit signed integer, not including the max value (2147483647)
        /// <br/>-2147483648 &#60;= returnsValue &#60; 2147483647
        /// </returns>
        public static int GetInt() => GetInt(int.MinValue, int.MaxValue);
        /// <returns>
        /// A random 32-bit signed integer in a certian range, not including the max value
        /// <br/>minValue &#60;= returnsValue &#60; maxValue
        /// </returns>
        public static int GetInt(int minValue, int maxValue) => NextInt(minValue, maxValue);

        /// <returns>
        /// A random 32-bit unsigned integer, not including the max value (2147483647)
        /// <br/>-2147483648 &#60;= returnsValue &#60; 2147483647
        /// </returns>
        public static uint GetUInt() => GetUInt(uint.MinValue, uint.MaxValue);
        /// <returns>
        /// A random 32-bit unsigned integer in a certian range, not including the max value
        /// <br/>minValue &#60;= returnsValue &#60; maxValue
        /// </returns>
        public static uint GetUInt(uint minValue, uint maxValue) => (uint)GetULong(minValue, maxValue);



        /// <returns>
        /// A random 64-bit signed integer, not including the max value (9223372036854775807)
        /// <br/>-9223372036854775808 &#60;= returnsValue &#60; 9223372036854775807
        /// </returns>
        public static long GetLong() => GetLong(long.MinValue, long.MaxValue);
        /// <returns>
        /// A random 64-bit signed integer in a certian range, not including the max value
        /// <br/>minValue &#60;= returnsValue &#60; maxValue
        /// </returns>
        public static long GetLong(long minValue, long maxValue)
        {
            long result = maxValue - minValue;
            result = Convert.ToInt64(result * NextDouble()) + minValue;
            if (result == maxValue) return minValue;
            return result;
        }

        /// <returns>
        /// A random 64-bit unsigned integer, not including the max value (18446744073709551615)
        /// <br/>0 &#60;= returnsValue &#60; 18446744073709551615
        /// </returns>
        public static ulong GetULong() => GetULong(ulong.MinValue, ulong.MaxValue);
        /// <returns>
        /// A random 64-bit unsigned integer in a certian range, not including the max value
        /// <br/>minValue &#60;= returnsValue &#60; maxValue
        /// </returns>
        public static ulong GetULong(ulong minValue, ulong maxValue)
        {
            ulong result = maxValue - minValue;
            result = Convert.ToUInt64(result * NextDouble()) + minValue;
            if (result == maxValue) return minValue;
            return result;
        }

        /// <returns>
        /// A random floating-point number in the range from 0.0f to 0.999..f, not including the max value (1.0f)
        /// <br/>0.0f &#60;= returnsValue &#60; 1.0f
        /// </returns>
        public static float GetFloat() => (float)NextDouble();
        /// <returns>
        /// A random floating-point number in the range from minValue to maxValue, not including the max value
        /// <br/>minValue &#60;= returnsValue &#60; maxValue
        /// </returns>
        public static float GetFloat(float minValue, float maxValue) => ((maxValue - minValue) * (float)NextDouble()) + minValue;

        /// <returns>
        /// A random floating-point number in the range from 0.0f to 0.999..f, not including the max value (1.0f)
        /// <br/>0.0f &#60;= returnsValue &#60; 1.0f
        /// </returns>
        public static double GetDouble() => NextDouble();
        /// <returns>
        /// A random floating-point number in the range from minValue to maxValue, not including the max value
        /// <br/>minValue &#60;= returnsValue &#60; maxValue
        /// </returns>
        public static double GetDouble(double minValue, double maxValue) => ((maxValue - minValue) * NextDouble()) + minValue;


        /// <returns>A random element from list</returns>
        public static T GetRandom<T>(IList<T> elements)
        {
            return elements[GetInt(0, elements.Count)];
        }
        /// <returns>A random element from enumerable</returns>
        public static T GetRandom<T>(IEnumerable<T> elements)
        {
            return elements.ElementAt(GetInt(0, elements.Count()));
        }

        /// <summary>
        /// Returns true with the specified chance(in the range from 0 to 100)
        /// </summary>
        /// <param name="chance">A number in the range from 0 to 100</param>
        /// <returns>True with the specified chance</returns>
        public static bool Chance(float chance) => (chance / 100f) > GetFloat();

        /// <summary>
        /// Returns value from case with the specified chance like in CS:GO
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="casase"></param>
        /// <returns></returns>
        public static TValue OpenCase<TValue>(ICase<TValue> casase)
        {
            float chanceMultipler = 1f;
            float remainingChange = 100f;
            foreach (KeyValuePair<float, TValue> item in casase)
            {
                if (Chance(item.Key * chanceMultipler))
                {
                    return item.Value;
                }
                else
                {
                    remainingChange -= item.Key;
                    chanceMultipler = 100f / remainingChange;
                }
            }
            return casase.DefaultValue;
        }
    }

    /// <summary>
    /// Default case for Randomizer.OpenCase
    /// </summary>
    public class Case<TValue> : ICase<TValue>
    {
        private KeyValuePair<float, TValue>[] _items = new KeyValuePair<float, TValue>[0];

        public TValue DefaultValue { get; }
        
        public Case(TValue defaultValue)
        {
            DefaultValue = defaultValue;
        }

        /// <summary>
        /// Addes new item, thorw exception if total sum of chances will be more than 100f
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Add(float chance, TValue value)
        {
            List<KeyValuePair<float, TValue>> list = new List<KeyValuePair<float, TValue>>(_items)
            {
                new KeyValuePair<float, TValue>(chance, value)
            };
            if (list.Select(x => x.Key).Sum() > 100f)
            {
                throw new Exception("Total sum of chances overflow");
            }
            _items = list.OrderByDescending(x => x.Key).ToArray();
        }

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
    }

    /// <summary>
    /// Base interface for Randomizer.OpenCase
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface ICase<TValue> : IEnumerable
    {
        TValue DefaultValue { get; }
    }
}
