namespace MonopolyGame;

public class GoToVacation : CardChance
{
    public int Id { get; }
    public string Description { get; }
    public GoToVacation(int id, string description)
    {
    	Id = id;
		Description = description;
    }
    public bool ActionCard(IPlayer player, GameController game)
    {
        var vacation = game.GetBoard().SquareBoard.First(FreeParkingSquare => true);
        game.MovePlayerToSquare(player, vacation); // Pindahkan pemain ke kota Londonn
        return true;
    }
}