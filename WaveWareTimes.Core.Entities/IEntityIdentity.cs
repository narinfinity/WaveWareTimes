namespace WaveWareTimes.Core.Entities
{
    public interface IEntityIdentity<TKey>
    {
        TKey Id { get; set; }
    }
}
