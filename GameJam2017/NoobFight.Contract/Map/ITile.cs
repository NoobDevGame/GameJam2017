namespace NoobFight.Contract.Map
{
    public interface ITile
    {
        int X { get; set; }
        int Y { get; set; }
        int Asset { get; }
        bool Collidable { get; }
    }
}
