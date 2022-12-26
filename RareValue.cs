
namespace Sidit.Randomizer
{
    public class RareValue<TValue> : IRareValue<TValue>
    {
        public float Chance { get; set; }
        private TValue _value;

        public TValue Get() => _value;
        public void Set(TValue value) => _value = value;

        public RareValue(float chance, TValue value)
        {
            Chance = chance;
            _value = value;
        }
    }

   
}
