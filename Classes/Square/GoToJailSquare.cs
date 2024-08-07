namespace MonopolyGame;

public class GoToJailSquare : SpecialSquare, ISquare
{
	public string Description { get; private set; }
	public GoToJailSquare(int id, string name) : base(id, name) { }

	public bool EffectSquare(IPlayer player, GameController game)
	{
		game.HandleGoToJail(player);
		return true;
	}
}