namespace MonopolyGame;

public class IncomeTaxSquare : SpecialSquare, ISquare
{
	public IncomeTaxSquare(int id, string name, string description) : base(id, name, description)
    {
    }
	public bool EffectSquare(IPlayer player, GameController game)
	{
		PlayerData playerData = game.GetPlayerData(player);
		decimal tax = playerData.Balance * 0.1m;
		playerData.DeductBalance((int)tax);
		return true;
	}
}
