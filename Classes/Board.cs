using System.Collections.Generic;
using System.Text.Json;

namespace MonopolyGame;

public class Board : IBoard
{
    public int NumberOfSquare { get; private set; }
    public List<ISquare> SquareBoard { get; private set; }

    public Board(int numberOfSquare, int maxFields = 40)
    {
        NumberOfSquare = numberOfSquare;
        SquareBoard = new List<ISquare>(maxFields);
    }
    public ISquare GetGoSquare()
    {
        // Mendapatkan square pertama sebagai Go Square
        return SquareBoard.FirstOrDefault(square => square is GoSquare);
    }
}
