namespace MonopolyGame;

public class GeneralRepairs : CardChance
{
	public int Id { get; }
	public string Description { get; }
	public GeneralRepairs(int id, string description)
	{
		Id = id;
		Description = description;
	}
	public bool ActionCard(IPlayer player, GameController game)
	{
		int currentBalance = game.GetPlayerBalance(player);

		int newBalance = currentBalance + 150;

		game.UpdatePlayerBalance(player, newBalance);
		return true;
	}
}
