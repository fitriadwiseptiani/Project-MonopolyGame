namespace MonopolyGame;

public class PlayerData : IPlayer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public PlayerPieces Piece;
    public int balance = 1000;
    public List<Property> PropertyPlayer{ get; set; }
    public List<ICard> CardChanceSave;
    public ISquare playerPosition;
    private IPlayer newPlayer;
    private int amount;
    private IPlayer player;

    public PlayerData(int id, PlayerPieces piece, int money, string name)
    {
        Id = id;
        Piece = piece;
        balance = money;
        PropertyPlayer = new List<Property>();
        CardChanceSave = new List<ICard>();
        Name = name;
    }

    public PlayerData(IPlayer newPlayer, int amount)
    {
        this.newPlayer = newPlayer;
        this.amount = amount;
    }

    public PlayerData(IPlayer player)
    {
        this.player = player;
    }

    public bool HaveAdvanceToGoCard()
    {
        // untuk mengetahuinya dari riwayat mendapatkan cardchance
        return CardChanceSave.OfType<AdvanceToGo>().Any();
    }
    public bool HaveJailFreeCard()
    {
        return CardChanceSave.OfType<GetOutOfJailFree>().Any();
    }

    public void AddBalance(int cash)
    {
        balance += cash;
    }
    public void DeductBalance(int cash)
    {
        balance -= cash;
    }
    public int GetBalance(IPlayer player)
    {
        return balance;
    }
    public void SetPosition(ISquare newPosition)
    {
        playerPosition = newPosition;
    }
    public List<Property> GetPropertiesPlayer(IPlayer player)
    {
        return PropertyPlayer;
    }
}
