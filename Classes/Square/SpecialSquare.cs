namespace MonopolyGame;

public class SpecialSquare : ISquare
{
	public int Id { get; private set; }
	public string Name { get; private set; }
	public string Code { get; private set; }
	public string Description { get; private set; }

   public SpecialSquare(int id, string name, string code, string description)
    {
        Id = id;
        Name = name;
		Code = code;
        Description = description;
    }
	public bool EffectSquare(IPlayer player, GameController game)
	{
		return true;
	}
}
