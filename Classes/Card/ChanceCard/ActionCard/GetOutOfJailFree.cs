namespace MonopolyGame;

public class GetOutOfJailFree : CardChance
{
    public int Id { get; }
    public string Description { get; }
    public GetOutOfJailFree(int id, string description)
    {
    	Id = id;
		Description = description;
    }
    public bool ActionCard(IPlayer player, GameController game)
    {
        game.HandleGetOutJail(player);
        return true;
    }
}