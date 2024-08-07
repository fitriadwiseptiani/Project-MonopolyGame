namespace MonopolyGame;

public class JailSquare : SpecialSquare, ISquare
{
	public string Description { get; private set; }
	public JailSquare(int id, string name) : base(id, name) { }

	public bool EffectSquare(IPlayer player, GameController game)
	{
		// Player goes to jail
		game.HandleGoToJail(player);
		return true;
	}
}