namespace MonopolyGame;

public class ChairmanOfTheBoard : CardCommunity
{
    public int Id { get; }
    public string Description { get; }

    public bool ActionCard(IPlayer player, GameController game)
    {
        var amount = 50;

        return true;
    }
}
