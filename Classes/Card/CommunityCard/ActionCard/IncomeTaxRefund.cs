namespace MonopolyGame;

public class IncomeTaxRefund : CardCommunity
{
    public int Id { get; }
    public string Description { get; }
    public IncomeTaxRefund(int id, string description)
    {
    	Id = id;
		Description = description;
    }
    public bool ActionCard(IPlayer player, GameController game)
    {
        int currentBalance = game.GetPlayerBalance(player);

        int newBalance = currentBalance + 20;

        game.UpdatePlayerBalance(player, newBalance);
        return true;
    }
}