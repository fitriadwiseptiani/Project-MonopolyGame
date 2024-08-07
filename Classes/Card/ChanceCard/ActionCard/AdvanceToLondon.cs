namespace MonopolyGame;

public class AdvanceToLondon : CardChance
{
    public int Id { get; }
    public string Description { get; }
    public AdvanceToLondon(int id, string description)
    {
    	Id = id;
		Description = description;
    }
    public bool ActionCard(IPlayer player, GameController game)
    {
        var london = game.GetBoard().SquareBoard.First(s => s is Property && s.Name == "London");
        game.MovePlayerToSquare(player, london); // Pindahkan pemain ke kota Londonn
        return true;
    }
}
