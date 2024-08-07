namespace MonopolyGame;

public class JailFree : CardCommunity
{
    public int Id { get; }
    public string Description { get; }
    public JailFree(int id, string description)
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
