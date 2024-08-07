namespace MonopolyGame;

public class BuildingLoanMatures : CardChance
{
    public int Id { get; }
    public string Description { get; }
    public BuildingLoanMatures(int id, string description)
    {
    	Id = id;
		Description = description;
    }
    public bool ActionCard(IPlayer player, GameController game)
    {
        game.GetPlayerData(player).AddBalance(150);
        return true;
    }
}
