namespace MonopolyGame;

public class LuxuryTaxSquare : SpecialSquare, ISquare
{

	public LuxuryTaxSquare(int id, string name,string code, string description) : base(id, name, code, description)
	{
	}
	public bool EffectSquare(IPlayer player, GameController game)
	{
		try
		{
			int balance = game.GetPlayerBalance(player);
			if (balance >= 75)
			{
				game.PayTax(player, 75);
				return true;
			}
			else
			{
				if (game.DeclareBankrupt(player))
				{
					throw new Exception("");
				}
				return false;
			}
		}
		catch (Exception e)
		{
			Console.WriteLine("Terdapat kesalahan : " + e.Message);
			return false;
		}
	}
}
