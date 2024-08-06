// namespace MonopolyGame;

// public class AdvanceToGo : CardChance
// {
//     public int Id { get; }
//     public string Description { get; }
//     // public TypeCard typeCard { get; }

//     public bool ActionCard(IPlayer player, GameController game)
//     {
//         var goSquare = game.GetBoard().SquareBoard.First(s => s is GoSquare); // Pindahkan pemain ke Go
//         game.MovePlayer(player, goSquare, null); // Pindahkan pemain ke posisi Go
//         game.GetPlayerData(player).AddBalance(200);
//         return true;
//     }
// }