namespace MonopolyGame;

public class LuxuryTaxSquare : SpecialSquare, ISquare
{

	public LuxuryTaxSquare(int id, string name, string description) : base(id, name, description)
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
