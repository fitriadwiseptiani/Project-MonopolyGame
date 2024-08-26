namespace MonopolyGame;

public class Utilities : Property, ISquare
{
    public int RentPrice { get; private set; }

    public Utilities(int id, string name,string code, int price, int rentPrice)
        : base(id, name,code, price, rentPrice)
    {
        RentPrice = rentPrice;
    }

    public bool EffectSquare(IPlayer player, GameController game)
    {
        return false;
    }
}
