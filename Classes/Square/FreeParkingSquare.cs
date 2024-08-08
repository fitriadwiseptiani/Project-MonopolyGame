namespace MonopolyGame;

public class FreeParkingSquare : SpecialSquare
{
	public FreeParkingSquare(int id, string name, string description) : base(id, name, description)
    {
    }
	public bool EffectSquare(IPlayer player, GameController game)
	{
		return true;
	}
}
