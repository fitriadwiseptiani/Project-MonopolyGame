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
        int currentBalance = game.GetPlayerBalance(player);

        int newBalance = currentBalance - 50 ;

        game.UpdatePlayerBalance(player, newBalance);
        return true;
    }
}
