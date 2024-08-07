namespace MonopolyGame;

public interface ISquare
{
	public int Id { get; }
	public string Name { get; }
	public string Description { get; }

	public bool EffectSquare(IPlayer player, GameController game);
}
