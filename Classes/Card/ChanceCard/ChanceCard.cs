namespace MonopolyGame;
public interface CardChance : ICard
{
	public int Id { get; }
	public string Description { get; }

	public bool ActionCard(IPlayer player, GameController game);
}