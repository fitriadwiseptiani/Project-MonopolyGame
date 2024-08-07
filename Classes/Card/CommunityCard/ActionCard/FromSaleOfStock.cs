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
		game.GetPlayerData(player).AddBalance(50);
		return true;
	}
}
