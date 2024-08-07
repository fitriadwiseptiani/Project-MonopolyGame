namespace MonopolyGame;

public class ConsultancyFee : CardCommunity
{
    public int Id { get; }
    public string Description { get; }
    public ConsultancyFee(int id, string description)
    {
    	Id = id;
		Description = description;
    }
    public bool ActionCard(IPlayer player, GameController game)
    {
        game.GetPlayerData(player).AddBalance(25);
        return true;
    }
}