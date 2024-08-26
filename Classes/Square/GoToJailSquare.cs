namespace MonopolyGame;

public class GoToJailSquare : SpecialSquare, ISquare
{
	public GoToJailSquare(int id, string name, string code, string description) : base(id, name, code, description) { }

	public bool EffectSquare(IPlayer player, GameController game)
	{
		game.HandleGoToJail(player);
		return true;
	}
}