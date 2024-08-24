namespace MonopolyGame;

public class GoSquare : SpecialSquare
{
	public GoSquare(int id, string name,string code, string description) : base(id, name,code, description)
    {
    }
	public bool EffectSquare(IPlayer player, GameController game)
	{
		// mengambil data pada player data dan menambahkan balance sebesar 200 bila melewati GO
		int currentBalance = game.GetPlayerBalance(player);

		int newBalance = currentBalance + 200;

		game.UpdatePlayerBalance(player, newBalance);
		return true;
	}
}
