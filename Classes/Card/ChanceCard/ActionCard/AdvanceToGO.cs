namespace MonopolyGame;

public class AdvanceToGo : CardChance
{
	public int Id { get; }
	public string Description { get; }
	public AdvanceToGo(int id, string description)
	{
		Id = id;
		Description = description;
	}
	public bool ActionCard(IPlayer player, GameController game)
	{
		var goSquare = game.GetBoard().SquareBoard.First(s => s is GoSquare); // Pindahkan pemain ke Go
		game.MovePlayerToSquare(player, goSquare);
		game.GetPlayerData(player).AddBalance(200);
		return true;
	}
}