namespace MonopolyGame;

public class CardCommunitySquare : SpecialSquare, ISquare
{
	public CardCommunitySquare(int id, string name, string code, string description) : base(id, name, code, description)
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
