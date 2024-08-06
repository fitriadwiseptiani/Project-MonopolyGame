namespace MonopolyGame;

public class Railroads : Property, ISquare
{
    public int RentPrice { get; private set; }

    public Railroads(int id, string name, int price, int rentPrice)
        : base(id, name, price, rentPrice)
    {
        RentPrice = rentPrice;
    }

    public bool EffectSquare(IPlayer player, GameController game)
    {

        return false;
    }
}
