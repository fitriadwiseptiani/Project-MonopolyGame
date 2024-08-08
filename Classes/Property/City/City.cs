namespace MonopolyGame;

public class City : Property
{
    public City(int id, string name, int price, int rentPrice)
        : base(id, name, price, rentPrice)
    {}
    public bool EffectSquare(IPlayer player, GameController game)
    {
        return true;
    }
}
