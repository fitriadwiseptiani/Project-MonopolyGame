namespace MonopolyGame;

public class LuxuryTaxSquare : SpecialSquare, ISquare
{

	public string Description { get; private set; }
	public LuxuryTaxSquare(int id, string name) : base(id, name)
	{
	}
	public bool EffectSquare(IPlayer player, GameController game)
	{
		// nilai untuk luxurytax = 75
		PlayerData playerData = game.GetPlayerData(player);
		playerData.DeductBalance(75);
		return true;
	}
}
