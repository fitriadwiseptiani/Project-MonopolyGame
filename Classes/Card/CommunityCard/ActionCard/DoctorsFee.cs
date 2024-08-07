namespace MonopolyGame;

public class DoctorsFee : CardCommunity
{
    public int Id { get; }
    public string Description { get; }
    public DoctorsFee(int id, string description)
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
