namespace MonopolyGame;

public class BuildingAndLoanAssociation : CardChance
{
    public int Id { get; }
    public string Description { get; }
    public BuildingAndLoanAssociation(int id, string description)
    {
    	Id = id;
		Description = description;
    }
    public bool ActionCard(IPlayer player, GameController game)
    {
        int currentBalance = game.GetPlayerBalance(player);

        int newBalance = currentBalance + 150;

        game.UpdatePlayerBalance(player, newBalance);
        return true;
    }
}
