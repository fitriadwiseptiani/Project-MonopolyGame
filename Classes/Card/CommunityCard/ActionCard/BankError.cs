namespace MonopolyGame;

public class BankError : CardCommunity
{
	public int Id { get; }
	public string Description { get; }
	public BankError(int id, string description)
	{
		Id = id;
		Description = description;
	}

	public bool ActionCard(IPlayer player, GameController game)
	{
		game.GetPlayerData(player).AddBalance(200);
		return true;
	}
}