namespace MonopolyGame;

public class City : Property, ISquare
{
    public int RentPrice { get; private set; }

    public City(int id, string name, int price, int rentPrice)
        : base(id, name, price, rentPrice)
    {
        RentPrice = rentPrice;
    }
    public bool EffectSquare(IPlayer player, GameController game){
        return true;
    }
}
