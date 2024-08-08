namespace MonopolyGame;

public class CardCommunitySquare : SpecialSquare, ISquare
{
	public CardCommunitySquare(int id, string name, string description) : base(id, name, description)
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
