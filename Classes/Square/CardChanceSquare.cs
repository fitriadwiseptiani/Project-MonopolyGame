namespace MonopolyGame;

public class CardChanceSquare : SpecialSquare, ISquare
{
	public CardChanceSquare(int id, string name, string description) : base(id, name, description)
	{
	}
	public bool EffectSquare(IPlayer player, GameController game)
	{
		ICard card = game.DrawCardChance();
		if (card != null)
		{
			card.ActionCard(player, game);
		}
		else
		{
			throw new Exception("Tidak ada kartu Chance yang tersedia.");
		}
		return true;
	}
}
