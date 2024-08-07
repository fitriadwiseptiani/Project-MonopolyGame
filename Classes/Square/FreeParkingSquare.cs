namespace MonopolyGame;

public class FreeParkingSquare : SpecialSquare, ISquare
{
	public string Description { get; private set; }

	public FreeParkingSquare(int id, string name) : base(id, name)
	{
	}
	public bool EffectSquare(IPlayer player, GameController game)
	{
		return true;
	}
}
