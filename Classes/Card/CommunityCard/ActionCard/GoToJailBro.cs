namespace MonopolyGame;

public class GoToJailBro : CardCommunity
{
    public int Id { get; }
    public string Description { get; }
    public GoToJailBro(int id, string description)
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
