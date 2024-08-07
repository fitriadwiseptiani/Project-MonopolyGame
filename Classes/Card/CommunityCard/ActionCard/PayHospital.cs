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
        game.GetPlayerData(player).DeductBalance(100);
        return true;
    }
}