namespace MonopolyGame;

public class Railroads : Property, ISquare
{
    public Railroads(int id, string name, int price, int rentPrice)
        : base(id, name, price, rentPrice)
    {}

    public bool EffectSquare(IPlayer player, GameController game)
    {

        return false;
    }
}
