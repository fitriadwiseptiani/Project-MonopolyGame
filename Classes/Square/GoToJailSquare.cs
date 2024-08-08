namespace MonopolyGame;

public class GoToJailSquare : SpecialSquare, ISquare
{
	public GoToJailSquare(int id, string name, string description) : base(id, name, description) { }

	public bool EffectSquare(IPlayer player, GameController game)
	{
		game.HandleGoToJail(player);
		return true;
	}
}