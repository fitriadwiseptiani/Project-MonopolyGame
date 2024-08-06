
namespace MonopolyGame;

public class Property : ISquare
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Price { get; private set; }
    public int RentPrice { get; private set; }
    public IPlayer Owner { get; private set; }

    public Property(int id, string name, int price, int rentPrice)
    {
        Id = id;
        Name = name;
        Price = price;
        RentPrice = rentPrice;
        Owner = null;
    }
    public void SetOwner(IPlayer player){
        Owner = player;
    }
    public bool EffectSquare(IPlayer player, GameController game)
    {
        PlayerData playerData = game.GetPlayerData(player);

        // Jika properti dimiliki oleh orang lain, bayar sewa
        if (Owner != null && Owner != player)
        {
            PayRent(player, game);
            return true;
        }

        // Jika properti tidak dimiliki, tawarkan untuk dijual
        if (Owner == null && playerData.balance >= Price)
        {
            BuyProperty(player, game);
            return true;
        }

        return false;
    }
    public virtual void PayRent(IPlayer player, GameController game)
    { }

    protected void BuyProperty(IPlayer player, GameController game)
    {
        Owner = player;
        PlayerData playerData = game.GetPlayerData(player);
        playerData.DeductBalance(Price);
        // game.BuyProperty?.Invoke(player, this);
    }

    internal int CalculateRent()
    {
        throw new NotImplementedException();
    }

}
