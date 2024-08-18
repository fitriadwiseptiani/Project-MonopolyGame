namespace MonopolyGame;

public class PayHospital : CardCommunity
{
    public int Id { get; }
    public string Description { get; }
    public PayHospital(int id, string description)
    {
    	Id = id;
		Description = description;
    }
    public bool ActionCard(IPlayer player, GameController game)
    {
        int currentBalance = game.GetPlayerBalance(player);

        int newBalance = currentBalance - 100;

        game.UpdatePlayerBalance(player, newBalance);
        return true;
    }
}