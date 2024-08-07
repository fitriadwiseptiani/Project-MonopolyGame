namespace MonopolyGame;

public class SpecialSquare : ISquare
{
	public int Id { get; private set; }
	public string Name { get; private set; }
	public string Description { get; private set; }

	public SpecialSquare(int id, string name)
	{
		Id = id;
		Name = name;
	}
	public bool EffectSquare(IPlayer player, GameController game)
	{
		return true;
	}
}
