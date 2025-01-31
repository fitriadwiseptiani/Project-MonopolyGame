namespace MonopolyGame;

public class CardChanceSquare : SpecialSquare
{
	public CardChanceSquare(int id, string name, string code, string description) : base(id, name, code, description)
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
