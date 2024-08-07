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
        game.GetPlayerData(player).AddBalance(20);
        return true;
    }
}