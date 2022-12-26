using System;
using System.Linq;
using System.Collections.Generic;
using Random = System.Random;

namespace Sidit.Randomizer
{
    public static class Randomizer
    {
        public const float MaxChance = 100f;
        public const float MinChance = 0f;

        public static readonly Random random = new Random();

        public static bool GetBool() => random.Next(2) == 0;

        public static byte GetByte() => (byte)random.Next(256);
        public static sbyte GetSByte() => (sbyte)random.Next(-128, 128);

        public static float GetFloat() => (float)random.NextDouble();
        public static double GetDouble() => random.NextDouble();

        public static int Range(int max) => random.Next(max);
        public static float Range(float max) => random.Next((int)max) + GetFloat();

        public static bool Chance(float chance)
        {
            chance /= 100f;
            return chance > GetFloat();
        }

        public static float GetUnusedRare<TValue>(IEnumerable<IRareValue<TValue>> rareValues) => MaxChance - rareValues.Sum(x => x.Chance);

        public static TValue SafeRare<TValue>(TValue defaultValue, params IRareValue<TValue>[] rareValues) => SafeRare(defaultValue, rareValues);
        public static TValue SafeRare<TValue>(TValue defaultValue, IEnumerable<IRareValue<TValue>> rareValues)
        {
            float unusedRare = GetUnusedRare(rareValues);

            if (float.IsNegative(unusedRare))
                throw new RareSumHigherThanMaxValueException($"The amount of rare is higher than the maximum by {-unusedRare}");
            else
                return Rare(defaultValue, rareValues);
        }

        public static TValue Rare<TValue>(TValue defaultValue, params IRareValue<TValue>[] rareValues) => Rare(defaultValue, rareValues);
        public static TValue Rare<TValue>(TValue defaultValue, IEnumerable<IRareValue<TValue>> rareValues)
        {
            float chanceMultipler = 1f;
            float remainingChance = MaxChance;

            foreach (IRareValue<TValue> rareValue in rareValues.OrderByDescending(x => x.Chance))
            {
                if (Chance(rareValue.Chance * chanceMultipler))
                {
                    return rareValue.Get();
                }
                else
                {
                    remainingChance -= rareValue.Chance;
                    chanceMultipler = MaxChance / remainingChance;
                }
            }
            return defaultValue;
        }
    }
}