namespace MonopolyGame;

public class FreeParkingSquare : SpecialSquare
{
	public FreeParkingSquare(int id, string name, string code, string description) : base(id, name, code, description)
    {
    }
	public bool EffectSquare(IPlayer player, GameController game)
	{
		return true;
	}
}
