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
    // public void InitializeBoard()
    // {
    //     string result;

    //     using (StreamReader sr = new("JSON/City.json"))
    //     {
    //         result = sr.ReadToEnd();
    //     }
    //     List<City> cityMonopoly = JsonSerializer.Deserialize<List<City>>(result);

    //     string result2;

    //     using (StreamReader sr = new("JSON/Utilities.json"))
    //     {
    //         result2 = sr.ReadToEnd();
    //     }
    //     List<Utilities> utilitiesMonopoly = JsonSerializer.Deserialize<List<Utilities>>(result2);

    //     string result3;

    //     using (StreamReader sr = new("JSON/Railroads.json"))
    //     {
    //         result3 = sr.ReadToEnd();
    //     }
    //     List<Railroads> railroadsMonopoly = JsonSerializer.Deserialize<List<Railroads>>(result3);
    //     // city
    //     foreach (var city in cityMonopoly)
    //     {
    //         SquareBoard.Add(city);
    //     }

    //     // railroads
    //     foreach (var railroad in railroadsMonopoly)
    //     {
    //         SquareBoard.Add(railroad);
    //     }

    //     // Add
    //     foreach (var utility in utilitiesMonopoly)
    //     {
    //         SquareBoard.Add(utility);
    //     }

    //     SquareBoard.Add(new GoSquare(1, "Go Square", "Go"));
    //     SquareBoard.Add(new LuxuryTaxSquare(5, "Luxury Tax Square", "Luxury Tax"));
    //     SquareBoard.Add(new JailSquare(11, "Jail Square", "Jail"));
    //     SquareBoard.Add(new IncomeTaxSquare(21, "Income Tax Square", "Income Tax"));
    //     SquareBoard.Add(new GoToJailSquare(31, "GoToJailSquare", "Go to Jail"));
    //     SquareBoard.Add(new FreeParkingSquare(21, "Go To Jail Square", "Free Parking"));
    //     SquareBoard.Add(new CardCommunitySquare(3, "Card Community Square", "Community Chest"));
    //     SquareBoard.Add(new CardCommunitySquare(18, "Card Community Square", "Community Chest"));
    //     SquareBoard.Add(new CardCommunitySquare(34, "Card Community Square","Community Chest"));
    //     SquareBoard.Add(new CardChanceSquare(8, "Card Chance Square", "Chance"));
    //     SquareBoard.Add(new CardChanceSquare(23, "Card Chance Square", "Chance"));
    //     SquareBoard.Add(new CardChanceSquare(37, "Card Chance Square", "Chance"));

    //     SquareBoard = SquareBoard.OrderBy(square => square.Id).ToList();
    // }
    public ISquare GetGoSquare()
    {
        // Mendapatkan square pertama sebagai Go Square
        return SquareBoard.FirstOrDefault(square => square is GoSquare);
    }
}
