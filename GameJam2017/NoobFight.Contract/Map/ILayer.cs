namespace NoobFight.Contract.Map
{
    public interface ILayer
    {
        int Id { get; }
        int[] Tiles { get; }
    }
}
