// namespace MonopolyGame;

// public class AdvanceToLyon : CardChance
// {
//     public int Id { get; }
//     public string Description { get; }
//     // public TypeCard typeCard { get; }

//     public bool ActionCard(IPlayer player, GameController game)
//     {
//         var lyon = game.GetBoard().SquareBoard.First(s => s is Property && s.Name == "Lyon");
//         game.MovePlayer(player, lyon, null); // Pindahkan pemain ke Illinois Avenue
//         return true;
//     }
// }