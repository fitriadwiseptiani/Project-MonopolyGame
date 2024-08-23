namespace MonopolyGame;

public class FromSaleOfStock : CardCommunity
{
	public int Id { get; }
	public string Description { get; }
	public FromSaleOfStock(int id, string description)
	{
		Id = id;
		Description = description;
	}
	public bool ActionCard(IPlayer player, GameController game)
	{
		int currentBalance = game.GetPlayerBalance(player);

		int newBalance = currentBalance + 50;

		game.UpdatePlayerBalance(player, newBalance);
		return true;
	}
}
