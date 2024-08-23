namespace MonopolyGame;

public class PayPoorTax : CardChance
{
    public int Id { get; }
    public string Description { get; }
    public PayPoorTax(int id, string description)
    {
    	Id = id;
		Description = description;
    }
    public bool ActionCard(IPlayer player, GameController game)
    {
        game.PayTax(player, 15);
        return true;
    }
}
