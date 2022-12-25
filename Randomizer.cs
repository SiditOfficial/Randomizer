using System;
using System.Linq;
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

        public static void SafeRare(Action redundantAction, params IRareAction[] rareActions)
        {
            float rareSum = rareActions.Sum(x => x.Chance);

            if (rareSum > MaxChance)
            {
                throw new RareSumHigherThanMaxValueException($"The amount of rare is higher than the maximum by {rareSum - MaxChance}");
            }
            else
            {
                Rare(redundantAction, rareActions);
            }
        }

        public static void Rare(Action redundantAction, params IRareAction[] rareActions)
        {
            float chanceMultipler = 1f;
            float remainingChance = MaxChance;

            foreach (IRareAction rareAction in rareActions.OrderByDescending(x => x.Chance))
            {
                if (Chance(rareAction.Chance * chanceMultipler))
                {
                    rareAction.Invoke();
                    return;
                }
                else
                {
                    remainingChance -= rareAction.Chance;
                    chanceMultipler = MaxChance / remainingChance;
                }

            }
            redundantAction?.Invoke();
        }
    }
}