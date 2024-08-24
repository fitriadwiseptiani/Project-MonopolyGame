namespace MonopolyGame;

public class Railroads : Property, ISquare
{
    public Railroads(int id, string name, string code, int price, int rentPrice)
        : base(id, name, code, price, rentPrice)
    {}

    public bool EffectSquare(IPlayer player, GameController game)
    {

        return false;
    }
}
