namespace MonopolyGame;

public class JailSquare : SpecialSquare, ISquare
{
	public JailSquare(int id, string name, string description) : base(id, name, description) { }

	public bool EffectSquare(IPlayer player, GameController game)
	{
		// Player goes to jail
		game.HandleGoToJail(player);
		return true;
	}
}