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
		game.GetPlayerData(player).DeductBalance(150);
        return true;
	}
}
