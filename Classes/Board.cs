namespace MonopolyGame;
using System.Text.Json;
public class Board : IBoard
{
    public int NumberOfSquare { get; private set; }
    public List<ISquare> SquareBoard { get; private set; }

    public Board(int numberOfSquare, int maxFields = 40)
    {
        NumberOfSquare = numberOfSquare;
        SquareBoard = new List<ISquare>(maxFields);
    }
    public void InitializeBoard()
    {
        string result;

        using (StreamReader sr = new("JSON/City.json"))
        {
            result = sr.ReadToEnd();
        }
        List<City> cityMonopoly = JsonSerializer.Deserialize<List<City>>(result);

        string result2;

        using (StreamReader sr = new("JSON/Utilities.json"))
        {
            result2 = sr.ReadToEnd();
        }
        List<Utilities> utilitiesMonopoly = JsonSerializer.Deserialize<List<Utilities>>(result2);

        string result3;

        using (StreamReader sr = new("JSON/Railroads.json"))
        {
            result3 = sr.ReadToEnd();
        }
        List<Railroads> railroadsMonopoly = JsonSerializer.Deserialize<List<Railroads>>(result3);
        // city
        foreach (var city in cityMonopoly)
        {
            SquareBoard.Add(city);
        }

        // railroads
        foreach (var railroad in railroadsMonopoly)
        {
            SquareBoard.Add(railroad);
        }

        // Add
        foreach (var utility in utilitiesMonopoly)
        {
            SquareBoard.Add(utility);
        }

        SquareBoard.Add(new GoSquare(1, "Go"));
        SquareBoard.Add(new LuxuryTaxSquare(4, "Luxury Tax"));
        SquareBoard.Add(new JailSquare(10, "Jail"));
        SquareBoard.Add(new IncomeTaxSquare(20, "Income Tax"));
        SquareBoard.Add(new GoToJailSquare(30, "Go to Jail"));
        SquareBoard.Add(new FreeParkingSquare(21, "Free Parking"));
        SquareBoard.Add(new CardCommunitySquare(2, "Community Chest"));
        SquareBoard.Add(new CardCommunitySquare(17, "Community Chest"));
        SquareBoard.Add(new CardCommunitySquare(33, "Community Chest"));
        SquareBoard.Add(new CardChanceSquare(7, "Chance"));
        SquareBoard.Add(new CardChanceSquare(22, "Chance"));
        SquareBoard.Add(new CardChanceSquare(36, "Chance"));

        SquareBoard = SquareBoard.OrderBy(square => square.Id).ToList();
    }
    public ISquare GetGoSquare()
    {
        // Mendapatkan square pertama sebagai Go Square
        return SquareBoard.FirstOrDefault(square => square is GoSquare);
    }
    public void DisplayBoard()
    {
        foreach (var square in SquareBoard)
        {
            Console.WriteLine($"ID: {square.Id}, Name: {square.Name}");
        }
    }
}
