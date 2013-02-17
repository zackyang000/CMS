namespace AtomLab.Domain
{
    public interface IEntity<TEntityId>
    {
        TEntityId Id { get; }
    }
}
