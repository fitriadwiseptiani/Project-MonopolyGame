namespace MonopolyGame;

public class Utilities : Property, ISquare
{
    public int RentPrice { get; private set; }

    public Utilities(int id, string name, int price, int rentPrice)
        : base(id, name, price, rentPrice)
    {
        RentPrice = rentPrice;
    }

    public bool EffectSquare(IPlayer player, GameController game)
    {
        return false;
    }
}
