namespace MonopolyGame;

public class CardCommunitySquare : SpecialSquare, ISquare
{
	public string Description { get; private set; }

	public CardCommunitySquare(int id, string name) : base(id, name)
	{
	}
	public bool EffectSquare(IPlayer player, GameController game)
	{
		ICard card = game.DrawCardCommunity();
		if (card == null)
		{
			return false;
		}

		// Terapkan efek kartu
		card.ActionCard(player, game);
		return true;
	}
}
