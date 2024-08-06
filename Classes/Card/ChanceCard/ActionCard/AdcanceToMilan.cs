namespace MonopolyGame;

public class AdvanceToMilan : CardChance
{
    public int Id { get; }
    public string Description { get; }
    // public TypeCard typeCard { get; }

    public bool ActionCard(IPlayer player, GameController game)
    {
        var milan = game.GetBoard().SquareBoard.FirstOrDefault(s => s is Property && s.Name == "Milan");

        if (milan != null)
        {
            game.MovePlayerToSquare(player, milan); // Pindahkan pemain ke Milan
            return true;
        }
        return false;
    }
}