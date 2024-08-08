namespace MonopolyGame;

public class GoSquare : SpecialSquare
{
	public GoSquare(int id, string name, string description) : base(id, name, description)
    {
    }
	public bool EffectSquare(IPlayer player, GameController game)
	{
		// mengambil data pada player data dan menambahkan balance sebesar 200 bila melewati GO
		PlayerData playerData = game.GetPlayerData(player);
		playerData.AddBalance(200);
		return true;
	}
}
