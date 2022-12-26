
namespace Sidit.Randomizer
{
    public interface IRareValue<TValue>
    {
        public float Chance { get; set; }
        public TValue Get();
    }
}
