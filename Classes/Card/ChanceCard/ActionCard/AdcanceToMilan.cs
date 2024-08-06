namespace MonopolyGame;

public class AdvanceToMilan : CardChance
{
    public int Id { get; }
    public string Description { get; }
    // public TypeCard typeCard { get; }

    public bool ActionCard(IPlayer player, GameController game)
    {
        // var milan = game.GetBoard().SquareBoard.First(s => s is Property && s.Name == "Milan");
        // game.MovePlayer(player, milan, null); // Pindahkan pemain ke kota Londonn
        return true;
    }
}