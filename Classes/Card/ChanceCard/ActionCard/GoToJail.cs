namespace MonopolyGame;

public class GoToJail : CardChance
{
    public int Id { get; }
    public string Description { get; }
    public GoToJail(int id, string description)
    {
    	Id = id;
		Description = description;
    }
    public bool ActionCard(IPlayer player, GameController game)
    {
        game.HandleGoToJail(player);
        return true;
    }
}