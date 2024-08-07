namespace MonopolyGame;

public class BankPaysDividend : CardChance
{
	public int Id { get; }
	public string Description { get; }

	public BankPaysDividend(int id, string description)
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
