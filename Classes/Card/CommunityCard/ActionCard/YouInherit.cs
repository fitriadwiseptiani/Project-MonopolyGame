namespace MonopolyGame;

public class YouInherit : CardCommunity
{
    public int Id { get; }
    public string Description { get; }
    public YouInherit(int id, string description)
    {
    	Id = id;
		Description = description;
    }
    public bool ActionCard(IPlayer player, GameController game)
    {
        game.GetPlayerData(player).AddBalance(100);
        return true;
    }
}
