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

        public static bool GetBool() => Range(2) == 0;
        public static int GetPosNeg() => GetBool() ? 1 : -1;

        public static byte GetByte() => (byte)Range(256);
        public static sbyte GetSByte() => (sbyte)Range(-128, 128);

        public static float GetFloat() => (float)GetDouble();
        public static double GetDouble() => random.NextDouble();

        public static int Range(int max) => random.Next(max);
        public static int Range(int min, int max) => random.Next(min, max);

        public static float Range(float max) => GetFloat() * max;
        public static float Range(float min, float max) => GetFloat() * (max - min) + min;

        public static double Range(double max) => GetDouble() * max;
        public static double Range(double min, double max) => GetDouble() * (max - min) + min;

        public static T ChooseRandom<T>(params T[] elements) => GetRandomFrom(elements);

        public static T GetRandomFrom<T>(IEnumerable<T> enumerable) => enumerable.ElementAt(Range(enumerable.Count()));

        public static bool Chance(float chance)
        {
            chance /= 100f;
            return chance > GetFloat();
        }

        public static float GetUnusedRare<TValue>(IEnumerable<IRareValue<TValue>> rareValues) => MaxChance - rareValues.Sum(x => x.Chance);

        public static void CheckRare<TValue>(IEnumerable<IRareValue<TValue>> rareValues)
        {
            float unusedRare = GetUnusedRare(rareValues);

            if (float.IsNegative(unusedRare))
            {
                throw new RareSumHigherThanMaxValueException($"The amount of rare is higher than the maximum by {-unusedRare}");
            }
        }

        public static TValue CheckedRare<TValue>(TValue defaultValue, IEnumerable<IRareValue<TValue>> rareValues)
        {
            CheckRare(rareValues);
            return Rare(defaultValue, rareValues);
        }

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