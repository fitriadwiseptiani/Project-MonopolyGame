namespace MonopolyGame;

public interface ISquare
{
	public int Id { get; }
	public string Name { get; }
	public string Code { get; }

	public bool EffectSquare(IPlayer player, GameController game);
}
