using MonopolyGame;
using System.Collections.Generic;
using System.Text.Json;

class Program
{
	private static GameController game;

	static void Main()
	{
		int maxPlayer = 8;
		int playerIdCounter = 1;

		Board board = new Board(40);
		IDice dice = new Dice(new int[] { 1, 2, 3, 4, 5, 6 });
		game = new GameController(maxPlayer, board, dice);

		InitializeBoard();
		ChanceCardAll();
		CommunityCardAll();

		while (true)
		{
			Console.WriteLine("Masukkan nama untuk player yang akan bermain atau 'start' untuk memulai permainan:");
			string input = Console.ReadLine();

			if (input.ToLower() == "start")
			{
				if (game.GetPlayers().Count < 2)
				{
					Console.WriteLine("Tambahkan lagi minimal 2 pemain");
					continue;
				}
				Console.WriteLine("Permainan dimulai");
				game.Start();
				DisplayBoard(board, game);

				// while (!game.CheckWinner())
				// {
				// 	IPlayer currentPlayer = game.GetCurrentPlayer();
				// 	Console.WriteLine($"Giliran {currentPlayer.Name}");

				// 	// Start turn for the current player
				// 	// game.StartTurn();
				// 	// game.SetTurnPlayer(currentPlayer);
				// 	// GetPlayerInfo();
				// 	// GetPropertiesInfo();
				// 	// BuyPropertyPlayer();
				// 	// game.EndTurn();
				// 	// // Change player turn
				// 	// game.ChangeTurnPlayer();
				// }

				// Announce the winner
				game.End();
				IPlayer winner = game.GetWinner();
				if (winner != null)
				{
					Console.WriteLine($"Permainan selesai! Pemenangnya adalah {winner.Name}");
				}
				else
				{
					Console.WriteLine("Permainan selesai! Tidak ada pemenang.");
				}
				break;
			}

			Console.WriteLine("Pilih salah satu dari berikut ini sebagai piece Anda:");
			foreach (var piece in Enum.GetValues(typeof(PlayerPieces)))
			{
				Console.WriteLine($"- {piece}");
			}
			Console.WriteLine("Masukkan nama piece yang dipilih:");
			string pieceInput = Console.ReadLine();

			if (Enum.TryParse(pieceInput, true, out PlayerPieces selectedPiece))
			{
				IPlayer newPlayer = new Player(playerIdCounter, input);
				PlayerData playerData = new PlayerData(selectedPiece, 1000);
				game.AddPlayer(newPlayer, playerData);
				Console.WriteLine($"{input} telah bergabung pada permainan.");
				playerIdCounter++;
			}
			else
			{
				Console.WriteLine("Piece tidak valid, silakan coba untuk mencoba lagi");
			}
		}
	}
	public static void InitializeBoard()
	{
		IBoard board = game.GetBoard();
		string result;

		using (StreamReader sr = new("JSON/City.json"))
		{
			result = sr.ReadToEnd();
		}
		List<City> cityMonopoly = JsonSerializer.Deserialize<List<City>>(result);

		string result2;

		using (StreamReader sr = new("JSON/Utilities.json"))
		{
			result2 = sr.ReadToEnd();
		}
		List<Utilities> utilitiesMonopoly = JsonSerializer.Deserialize<List<Utilities>>(result2);

		string result3;

		using (StreamReader sr = new("JSON/Railroads.json"))
		{
			result3 = sr.ReadToEnd();
		}
		List<Railroads> railroadsMonopoly = JsonSerializer.Deserialize<List<Railroads>>(result3);
		// city
		foreach (var city in cityMonopoly)
		{
			board.SquareBoard.Add(city);
		}

		// railroads
		foreach (var railroad in railroadsMonopoly)
		{
			board.SquareBoard.Add(railroad);
		}

		// Add
		foreach (var utility in utilitiesMonopoly)
		{
			board.SquareBoard.Add(utility);
		}

		board.SquareBoard.Add(new GoSquare(1, "Go Square", "Go"));
		board.SquareBoard.Add(new LuxuryTaxSquare(5, "Luxury Tax Square", "Luxury Tax"));
		board.SquareBoard.Add(new JailSquare(11, "Jail Square", "Jail"));
		board.SquareBoard.Add(new IncomeTaxSquare(21, "Income Tax Square", "Income Tax"));
		board.SquareBoard.Add(new GoToJailSquare(31, "GoToJailSquare", "Go to Jail"));
		board.SquareBoard.Add(new FreeParkingSquare(21, "Go To Jail Square", "Free Parking"));
		board.SquareBoard.Add(new CardCommunitySquare(3, "Card Community Square", "Community Chest"));
		board.SquareBoard.Add(new CardCommunitySquare(18, "Card Community Square", "Community Chest"));
		board.SquareBoard.Add(new CardCommunitySquare(34, "Card Community Square", "Community Chest"));
		board.SquareBoard.Add(new CardChanceSquare(8, "Card Chance Square", "Chance"));
		board.SquareBoard.Add(new CardChanceSquare(23, "Card Chance Square", "Chance"));
		board.SquareBoard.Add(new CardChanceSquare(37, "Card Chance Square", "Chance"));

		var sortedSquares = board.SquareBoard.OrderBy(square => square.Id).ToList();

		board.SquareBoard.Clear();
		foreach (var square in sortedSquares)
		{
			board.SquareBoard.Add(square);
		}

	}
	static void DisplayBoard(IBoard board, GameController game)
	{
		
		for(int i = 20; i >= 10; i--){
			var square = board.SquareBoard[i];
			string playerPosition = GetPlayerMarker(game, i);
			Console.Write($"[{(square.Id.ToString() + playerPosition).PadRight(10)}]");

		}	
		Console.WriteLine();

		for (int i = 0; i < 9; i++){
			var leftSquare = board.SquareBoard[30 +i];
			var rightSquare = board.SquareBoard[20 - i];
			string leftMarker = GetPlayerMarker(game, 30 + i);
			string rightMarker = GetPlayerMarker(game, 20 - i);
			
			Console.Write($"[{(leftSquare.Id.ToString() + leftMarker).PadRight(10)}]");
			for (int j = 0; j < 7; j++)
			{
				Console.Write(" ".PadRight(20));
			}

			Console.WriteLine($"[{(rightSquare.Id.ToString() + rightMarker).PadRight(10)}]");

		}

		for (int i = 0; i <= 9; i++)
		{
			var square = board.SquareBoard[i];
			string playerMarker = GetPlayerMarker(game, i);
			Console.Write($"[{(square.Id.ToString() + playerMarker).PadRight(10)}]");
		}
		Console.WriteLine();
		// IBoard board = game.GetBoard();
		// foreach (var square in board.SquareBoard)
		// {
		// 	Console.WriteLine($"ID: {square.Id}, Name: {square.Name}");
		// }

	}
	static string GetPlayerMarker(GameController game, int positionIndex)
	{
		foreach(var player in game.GetPlayers()){
			var position = game.GetPlayerPosition(player);
			int playerIndex = game.GetBoard().SquareBoard.IndexOf(position);
			if(playerIndex == positionIndex){
				return $"({player.Name})";
			}
		}
		return "";
	}
	public static void GetPlayerInfo()
	{
		IPlayer currentPlayer = game.GetCurrentPlayer();

		if (currentPlayer == null)
		{
			Console.WriteLine("Tidak ada pemain saat ini");
			return;
		}

		try
		{
			int id = game.GetPlayerId(currentPlayer);
			string name = game.GetPlayerName(currentPlayer);
			PlayerPieces piece = game.GetPlayerPiece(currentPlayer);
			int balance = game.GetPlayerBalance(currentPlayer);
			List<Property> properties = game.GetPlayerProperty(currentPlayer);
			List<ICard> cardsSave = game.GetPlayerCardSave(currentPlayer);
			ISquare currentPosition = game.GetPlayerPosition(currentPlayer);

			Console.WriteLine("\n*Data Player :");
			Console.WriteLine($"ID: {id}");
			Console.WriteLine($"Name: {name}");
			Console.WriteLine($"Balance: {balance}");
			Console.WriteLine($"Piece: {piece}");

			Console.WriteLine($"Properties : ");
			foreach (Property property in properties)
			{
				Console.WriteLine($"- {property.Name}");
			}
			Console.WriteLine("Cards Owned: ");
			foreach (ICard card in cardsSave)
			{
				Console.WriteLine($"- {card.Id}, {card.GetType().Name}, {card.Description}");
			}

			Console.WriteLine($"Posisi {name} sekarang berada di {currentPosition.Name} ({currentPosition.GetType().Name})");
		}
		catch (Exception ex)
		{
			Console.WriteLine("Terjadi kesalahan : " + ex.Message);
		}

	}

	public static void GetPropertiesInfo()
	{
		IPlayer currentPlayer = game.GetCurrentPlayer();
		if (currentPlayer == null)
		{
			Console.WriteLine("Tidak ada pemain saat ini");
			return;
		}
		try
		{
			ISquare currentPosition = game.GetPlayerPosition(currentPlayer);
			if (currentPosition is Property property)
			{
				Console.WriteLine("\n*Detail Properties :");
				Console.WriteLine($"ID: {property.Id}");
				Console.WriteLine($"Name: {property.Name}");
				Console.WriteLine($"Harga: {property.Price}");
				Console.WriteLine($"Harga Sewa: {property.RentPrice}");
				Console.WriteLine($"Pemilik : {property.Owner?.Name ?? "Belum Dimiliki"}");
			}
			else
			{
				Console.WriteLine("Posisi saat ini bukan properti.");
				currentPosition.EffectSquare(currentPlayer, game);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Terjadi kesalahan : " + ex.Message);
		}
	}

	public static void BuyPropertyPlayer()
	{
		IPlayer currentPlayer = game.GetCurrentPlayer();
		ISquare currentPosition = game.GetPlayerPosition(currentPlayer);
		if (currentPosition is Property property)
		{
			if (property.Owner == null)
			{
				Console.WriteLine($"Apakah player {currentPlayer.Name} ingin membeli {property.Name} seharga {property.Price}? (Y/N)");
				string input = Console.ReadLine();
				if (input?.Trim().ToLower() == "Y")
				{
					var balancePlayer = game.GetPlayerBalance(currentPlayer);
					if (balancePlayer >= property.Price)
					{
						property.BuyProperty(currentPlayer, game);
						Console.WriteLine($"Player {currentPlayer.Name} telah membeli {property.Name}");
					}
					else
					{
						Console.WriteLine($"Player {currentPlayer.Name} tidak memiliki cukup uang untuk membeli {property.Name}");
					}
				}
			}
			else
			{
				if (property.Owner != currentPlayer)
				{

					property.PayRent(currentPlayer, game);

					int rentAmount = property.RentPrice;
					game.GetPlayerProperty(property.Owner);
					int ownerBalance = game.GetPlayerBalance(property.Owner);
					int newOwnerBalance = ownerBalance += rentAmount;
					game.UpdatePlayerBalance(currentPlayer, newOwnerBalance);
					Console.WriteLine($"Player {currentPlayer.Name} membayar sewa {property.RentPrice} kepada {property.Owner.Name}.");
				}
			}
		}
		else
		{
			currentPosition.EffectSquare(currentPlayer, game);
		}
	}


	private static void ChanceCardAll()
	{
		var chanceCards = new List<ICard>
		{
			new AdvanceToMilan(1, "Player dapat pergi ke Milan"),
			new AdvanceToGo(2,"Kembalilah ke tempat asalmu"),
			new AdvanceToLondon(3,"Ayo kita liburan ke London"),
			new AdvanceToLyon(4,"Beli jajan ke Lyon yuk"),
			new BankPaysDividend(5,"Kamu dapat keuntungan dari kami sebeasr 100"),
			new BuildingAndLoanAssociation(6,"Karena kami baik maka kami akan memberimu 150"),
			new BuildingLoanMatures(7,"Ini kita kasih hadiah buat kamu"),
			new GeneralRepairs(8,"Kamu harus bayar biaya perbaikan ini"),
			new GetOutOfJailFree(9,"Selamat kamu sudah bebas"),
			new GoToJail(10,"Jangan nakal, ini hukumanmu"),
			new GoToVacation(11, "Sepertinya kamu kurang liburan ayo pergi"),
			new PayPoorTax(12,"Ayo jangan lupa bayar taxmu"),
			new SpeedingFine(13,"Sorry ini harus diperbaiki")
		};
		game.SetChanceCards(chanceCards);
	}
	private static void CommunityCardAll()
	{
		var communityCard = new List<ICard>

		{
			new BankError(1, "Selamat Anda mendapatkan tambahan dana sebesar 200"),
			new ConsultancyFee(2, "Anda mendapatkan 25 dari konsultasi yang telah dijalankan"),
			new DoctorsFee(3, "Anda harus membayar untuk pemeriksaan dokter"),
			new FromSaleOfStock(5,"Dapat dana nih dari pembelajaanmu"),
			new GoToJailBro(4, "Maaf Anda akan pergi ke penjara"),
			new HolidayFundMatures(6,"Kamu dapat tambahan dana untuk liburan"),
			new IncomeTaxRefund(7,"Selamat Anda mendapatkan dana dari tax yang telah dibayarkan"),
			new JailFree(8, "Anda sudah bebas dan bisa melanjutkan permaianan"),
			new LifeInsuranceMatures(9, "Bagus ini hadiah dari kami untukmu sebesar 100"),
			new PayHospital(10, "Jangan lupa kamu harus membayar tagihan rumah sakit"),
			new PaySchool(11, "Utamakanan kepentingan sekolah anakmu"),
			new YouInherit(12, "Kamu terpilih untuk mendapatkan uang 100, selamat yaa")
		};
		game.SetCommunityCards(communityCard);
	}
}
