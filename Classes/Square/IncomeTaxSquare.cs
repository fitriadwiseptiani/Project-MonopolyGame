namespace MonopolyGame;

public class IncomeTaxSquare : SpecialSquare, ISquare
{
	public IncomeTaxSquare(int id, string name, string description) : base(id, name, description)
    {
    }
	public bool EffectSquare(IPlayer player, GameController game)
	{
		int currentBalance = game.GetPlayerBalance(player) ;
		decimal tax = currentBalance * 0.1m;
		int newBalance = currentBalance - ((int)tax);
		game.UpdatePlayerBalance(player, newBalance);
		return true;
	}
}
