namespace MonopolyGame;

public class PaySchool : CardCommunity
{
    public int Id { get; }
    public string Description { get; }
    public PaySchool(int id, string description)
    {
    	Id = id;
		Description = description;
    }
    public bool ActionCard(IPlayer player, GameController game)
    {
        game.GetPlayerData(player).DeductBalance(50);
        return true;
    }
}
