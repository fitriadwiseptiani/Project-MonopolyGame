namespace MonopolyGame;

public class City : Property
{
    public City(int id, string name, string code, int price, int rentPrice)
        : base(id, name, code, price, rentPrice)
    {}
    public bool EffectSquare(IPlayer player, GameController game)
    {
        return true;
    }
}
