namespace MonopolyGame;

public class JailSquare : SpecialSquare, ISquare
{
	public JailSquare(int id, string name, string code, string description) : base(id, name,code, description) { }

	public bool EffectSquare(IPlayer player, GameController game)
	{
		// Player goes to jail
		game.HandleGoToJail(player);
		return true;
	}
}