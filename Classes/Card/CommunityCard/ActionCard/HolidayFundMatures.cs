namespace MonopolyGame;

public class HolidayFundMatures : CardCommunity
{
    public int Id { get; }
    public string Description { get; }
    public HolidayFundMatures(int id, string description)
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
