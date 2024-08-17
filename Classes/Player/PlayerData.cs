namespace MonopolyGame;

public class PlayerData
{
    public PlayerPieces Piece;
    public int Balance;
    public List<Property> propertyPlayer { get; set; }
    public List<ICard> cardSpesialSave;
    public ISquare playerPosition;

    public PlayerData(PlayerPieces playerPieces, int balance){
        Piece = playerPieces;
        Balance = balance;
        propertyPlayer = new List<Property>();
        cardSpesialSave = new List<ICard>();
    }
    public bool HaveAdvanceToGoCard()
    {
        // untuk mengetahuinya dari riwayat mendapatkan cardchance
        return cardSpesialSave.OfType<AdvanceToGo>().Any();
    }
    public bool HaveJailFreeCard()
    {
        return cardSpesialSave.OfType<GetOutOfJailFree>().Any();
    }
    public void AddBalance(int cash){
        Balance += cash;
    }
    public void DeductBalance(int cash){
        Balance -= cash;
    }
    // public int GetBalance(){
    //     return Balance;
    // }
    public void SetPosition(ISquare newPosition){
        playerPosition = newPosition;
    }
    public List<Property> GetPropertiesPlayer(){
        return propertyPlayer;
    }

}
