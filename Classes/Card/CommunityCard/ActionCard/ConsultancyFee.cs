namespace MonopolyGame;

public class ConsultancyFee : CardCommunity
{
    public int Id { get; }
    public string Description { get; }

    public bool ActionCard(IPlayer player, GameController game)
    {
        game.GetPlayerData(player).AddBalance(25);
        return true;


    }
}