namespace NoobFight.Contract.Map
{
    public interface ILayer
    {
        int Id { get; }
        ITile[] Tiles { get; }
    }
}
