namespace MonopolyGame;

public class LifeInsuranceMatures : CardCommunity
{
    public int Id { get; }
    public string Description { get; }
    public LifeInsuranceMatures(int id, string description)
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
