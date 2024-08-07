namespace MonopolyGame;

public class AdvanceToLyon : CardChance
{
	public int Id { get; }
	public string Description { get; }

	public AdvanceToLyon(int id, string description)
	{
		Id = id;
		Description = description;
	}
	public bool ActionCard(IPlayer player, GameController game)
	{
		var lyon = game.GetBoard().SquareBoard.First(s => s is Property && s.Name == "Lyon");
		game.MovePlayerToSquare(player, lyon); // Pindahkan pemain ke Illinois Avenue
		return true;
	}
}