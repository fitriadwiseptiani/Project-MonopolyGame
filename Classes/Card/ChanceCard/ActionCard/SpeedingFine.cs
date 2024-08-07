namespace MonopolyGame;

public class SpeedingFine : CardChance
{
    public int Id { get; }
    public string Description { get; }
    public SpeedingFine(int id, string description)
    {
    	Id = id;
		Description = description;
    }
    public bool ActionCard(IPlayer player, GameController game)
    {
        game.GetPlayerData(player).DeductBalance(15);
        return true;
    }
}
