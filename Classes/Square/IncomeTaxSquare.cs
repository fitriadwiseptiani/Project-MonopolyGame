namespace MonopolyGame;

public class IncomeTaxSquare : SpecialSquare
{
    public string Name { get; private set; }

    public IncomeTaxSquare(int id, string name) : base(id, name)
    {
    }
    public virtual bool EffectSquare(IPlayer player, GameController game)
    {
        PlayerData playerData = game.GetPlayerData(player);
        decimal tax = playerData.balance * 0.1m;
        playerData.DeductBalance((int)tax);
        return true;
    }
}
