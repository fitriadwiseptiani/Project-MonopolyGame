
namespace MonopolyGame;

public class Property : ISquare
{
	public int Id { get; private set; }
	public string Name { get; private set; }
	public string Code { get; private set; }
	public int Price { get; private set; }
	public int RentPrice { get; private set; }
	public string Description { get; private set; }
	public IPlayer Owner { get; private set; }

	public Property(int id, string name, string code, int price, int rentPrice)
	{
		Id = id;
		Name = name;
		Code = code;
		Price = price;
		RentPrice = rentPrice;
		Owner = null;
	}
	public void SetOwner(IPlayer player)
	{
		Owner = player;
	}
	public bool EffectSquare(IPlayer player, GameController game)
	{
		return true;
	}
	public void PayRent(IPlayer player, GameController game)
	{
		int currentBalance = game.GetPlayerBalance(player);
		if (!(Owner == player))
		{
			int newBalance = currentBalance - RentPrice;
			game.UpdatePlayerBalance(player, newBalance);
		}
		else
		{
			throw new InvalidOperationException($"Properti adalah milik Anda");
		}
	}

	public void BuyProperty(IPlayer player, GameController game)
	{
		IPlayer players = game.GetCurrentPlayer();
		int currentBalance = game.GetPlayerBalance(player);
		if (Owner == null)
		{
			int newBalance = currentBalance - RentPrice;
			game.UpdatePlayerBalance(player, newBalance);
		}
		else
		{
			throw new InvalidOperationException($"Properti sudah dimiliki oleh {players.Name}");
		}

		SetOwner(player);
	}

}
