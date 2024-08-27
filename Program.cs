using MonopolyGame;
using System.Collections.Generic;
using System.Text.Json;

class Program
{
	private static GameController game;

	static void Main()
	{
		int maxPlayer = 8;
		int totalSquare = 40;

		Board board = new Board(totalSquare);
		IDice dice = new Dice(new int[] { 1, 2, 3, 4, 5, 6, });
		game = new GameController(maxPlayer, board, dice);

		InitializeBoard();
		ChanceCardAll();
		CommunityCardAll();

		
		DisplayBoard(board,game);

		CheckGameStatus();

		while (CheckGameStatus() != GameStatus.End)
		{
			ChooseAction(out int action);
			switch(action){
				case 1:
					int newPlayerId = game.GetPlayers().Count + 1;
					InputPlayer(newPlayerId);
					break;
				case 2:
					if (game.GetPlayers().Count < 2)
					{
						Console.WriteLine("\nAdd more players (min. 2 players to start the game)");
						Thread.Sleep(2000);
						continue;
					}
					game.Start();
					// CheckGameStatus();
					DisplayBoard(board, game);

					StartGame();					
					

			while (!game.CheckWinner()){
						IPlayer currentPlayer = game.GetCurrentPlayer();
						Console.WriteLine($"\nGiliran {currentPlayer.Name}");


						StartTurnPlayer();
						game.SetTurnPlayer(currentPlayer);
						GetDice(dice, currentPlayer);
						GetPlayerInfo();
						GetPropertiesInfo();
						BuyPropertyPlayer();
						game.EndTurn();
						EndTurnPlayer();
						DisplayBoard(board, game);
						game.ChangeTurnPlayer();
						
					}
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
		}


	}
	static GameStatus CheckGameStatus()
	{
		GameStatus currentStatus = game.GetStatusGame();
		if (currentStatus == GameStatus.Preparation)
		{
			Console.WriteLine("\nPermainan akan segera disiapkan");

		}
		else if (currentStatus == GameStatus.Play)
		{
			Console.WriteLine("\nPermainan akan dimulai");
		}
		else if (currentStatus == GameStatus.End)
		{
			Console.WriteLine("\nPermainan telah berakhir");
		}
		return currentStatus;
	}
	static void StartTurnPlayer(){
		Console.WriteLine("\nApakah Anda ingin memulai giliran untuk bermain ? (Y)");
		string inputPlayer = Console.ReadLine();
			
			if (inputPlayer.ToLower() == "Y")
			{
				game.StartTurn();
			}
			else
			{
				Console.WriteLine("\nTolong masukkan input yang valid");
			Console.WriteLine("\nApakah Anda ingin memulai giliran untuk bermain ? (Y)");
			inputPlayer = Console.ReadLine();
		}
		
	}
	static void EndTurnPlayer(){
		Console.WriteLine("\nApakah Anda ingin mengakhiri giliran ? (Y/N)");
		string inputPlayer = Console.ReadLine();

		if (inputPlayer.ToLower() == "Y")
		{
			game.ChangeTurnPlayer();
		}
		else if(inputPlayer.ToLower() == "N"){
			Console.WriteLine("\nTolong masukkan input yang valid");
			Console.WriteLine("\nApakah Anda ingin memulai giliran untuk bermain ? (Y)");
			inputPlayer = Console.ReadLine();
		}
		else{
			Console.WriteLine("\nTolong masukkan input yang valid");
		}
	}
	static void ChooseAction(out int action)
	{
		action = 0;
		bool validInput = false;

		int maxPlayer = 8;
		while (!validInput)
		{
			if (game.GetPlayers().Count >= maxPlayer)
			{
				Console.WriteLine("\nJumlah pemain maksimal tercapai. Permainan akan segera dimulai.");
				action = 2;
				validInput = true;
				break; 
			}

			Console.WriteLine("\nPlease choose one of this following action ");
			Console.WriteLine("1. Add player");
			Console.WriteLine("2. Start game");
			Console.WriteLine();
			Console.Write("Your Input : ");
			Console.WriteLine();
			bool status = int.TryParse(Console.ReadLine(), out int input);
			if (status && input >= 1 && input <= 3)
			{
				action = input;
				validInput = true;
			}
			else
			{
				Console.WriteLine("Please input valid number (1-2)");
				Thread.Sleep(1000);
			}
		}

		Console.Clear();
	}
	static void InputPlayer(int playerId)
	{
		Console.Clear();
		Console.WriteLine($"\nInput name for player {playerId}: ");
		string playerName = Console.ReadLine();

		while (game.GetPlayers().Any(p => p.Name.Equals(playerName, StringComparison.OrdinalIgnoreCase)))
		{
			Console.WriteLine("\nPlayer was existing at the game, please tryy to input other name");
			Console.WriteLine($"\nInput name for player {playerId}: ");
			playerName = Console.ReadLine();
		}
		Console.WriteLine("\nChoose one as your piece : ");
		foreach (var piece in Enum.GetValues(typeof(PlayerPieces)))
		{
			Console.WriteLine($"- {piece}");
		}
		Console.Write("\nEnter the name of the piece you choose : ");
		string pieceInput = Console.ReadLine();

		PlayerPieces selectedPiece;
		while (!Enum.TryParse(pieceInput, true, out selectedPiece) ||
			   game.PieceTaken(selectedPiece))
		{
			Console.WriteLine("Piece tidak valid atau sudah digunakan. Silakan coba lagi:");
			Console.Write("\nEnter the name of the piece you choose : ");
			pieceInput = Console.ReadLine();
		}

		PlayerData playerData = new PlayerData(selectedPiece, 1000);
		IPlayer newPlayer = new Player(playerId, playerName);

		game.AddPlayer(newPlayer, playerData);
		Console.WriteLine($"\n{playerName} telah bergabung pada permainan.");

	}
	static void StartGame(){
		CheckGameStatus();
		Console.WriteLine("Press any key to continue ...");
		string input = Console.ReadLine();
		Console.Clear();
	}
	static void InitializeBoard()
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
		board.SquareBoard.Clear();
		

		foreach (var city in cityMonopoly)
		{
			board.SquareBoard.Add(city);
		}

		foreach (var railroad in railroadsMonopoly)
		{
			board.SquareBoard.Add(railroad);
		}

		foreach (var utility in utilitiesMonopoly)
		{
			board.SquareBoard.Add(utility);
		}

		board.SquareBoard.Add(new GoSquare(1, "Go Square","GO", "Go"));
		board.SquareBoard.Add(new LuxuryTaxSquare(5, "Luxury Tax Square", "LUX", "Luxury Tax"));
		board.SquareBoard.Add(new JailSquare(11, "Jail Square","JAIL", "Jail"));
		board.SquareBoard.Add(new IncomeTaxSquare(21, "Income Tax Square","TAX", "Income Tax"));
		board.SquareBoard.Add(new GoToJailSquare(31, "Go To Jail Square","GTJ", "Go to Jail"));
		board.SquareBoard.Add(new FreeParkingSquare(21, "Free Parking","FRE", "Free Parking"));
		board.SquareBoard.Add(new CardCommunitySquare(3, "Card Community Square","COM", "Community Chest"));
		board.SquareBoard.Add(new CardCommunitySquare(18, "Card Community Square","COM", "Community Chest"));
		board.SquareBoard.Add(new CardCommunitySquare(34, "Card Community Square","COM", "Community Chest"));
		board.SquareBoard.Add(new CardChanceSquare(8, "Card Chance Square","CAN", "Chance"));
		board.SquareBoard.Add(new CardChanceSquare(23, "Card Chance Square","CAN", "Chance"));
		board.SquareBoard.Add(new CardChanceSquare(37, "Card Chance Square","CAN", "Chance"));

		var sortedSquares = board.SquareBoard.OrderBy(square => square.Id).ToList();

		board.SquareBoard.Clear();
		foreach (var square in sortedSquares)
		{
			board.SquareBoard.Add(square);
		}

	}

	static void DisplayBoard(IBoard board, GameController game)
	{
		Console.Clear();
		Console.WriteLine("Board Layout:");

		for (int i = 0; i <= 10; i++)
		{
			var square = board.SquareBoard[i];
			string playerPosition = GetPlayerMarker(game, i);
			Console.Write($"[{(square.Code + playerPosition).PadRight(5)}]");
		}
		Console.WriteLine();

		for (int i = 0; i <= 8; i++)
			{
				var leftSquare = board.SquareBoard[40 - i];
				var rightSquare = board.SquareBoard[11 + i];
				string leftMarker = GetPlayerMarker(game, leftSquare.Id);
				string rightMarker = GetPlayerMarker(game, rightSquare.Id);

				Console.Write($"[{(leftSquare.Code + leftMarker).PadRight(5)}]");
				for (int j = 0; j < 5; j++)
				{
					Console.Write(" ".PadRight(42));
				}

				Console.WriteLine($"[{(rightSquare.Code + rightMarker).PadRight(5)}]");

			}
		Console.WriteLine();
		for (int i = 0; i <= 10; i++)
		{
			var square = board.SquareBoard[31-i];
			string playerPosition = GetPlayerMarker(game, square.Id);
			Console.Write($"[{(square.Code + playerPosition).PadRight(5)}]");
		}
		Console.WriteLine();
	}

	static string GetPlayerMarker(GameController game, int positionIndex)
	{
		foreach (var player in game.GetPlayers())
		{
			var playerPosition = game.GetPlayers()
			.Where(player =>
			{
				var position = game.GetPlayerPosition(player);
				int playerIndex = game.GetBoard().SquareBoard.IndexOf(position);
				return playerIndex == positionIndex;
			})
			.Select(player => player.Name);
			
			if (playerPosition.Any())
			{
				return $"({string.Join(", " , playerPosition)})";
			}
		}
		return string.Empty;
	}
	public static void GetDice(IDice dice, IPlayer player){
		var totalDice = dice.RollTwoDice(out int firstRoll, out int secondRoll, out int totalRoll);
		Console.WriteLine($"{player.Name} mendapatkan hasil lemparan dadu sebesar {firstRoll} dan {secondRoll} dengan total {totalRoll}");
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
				GetSquareDescribe(currentPosition, game);

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
			GetSquareDescribe(currentPosition, game);
			
		}
	}

	
	static void ChanceCardAll()
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
	static void CommunityCardAll()
	{
		var communityCards = new List<ICard>
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
		game.SetCommunityCards(communityCards);
	}
	static void CardDescribe(ICard card)
	{
			string cardClassName = card.GetType().Name;
			Console.WriteLine($"Card Name : {cardClassName}");
			Console.WriteLine($"Description : {card.Description}");
			Console.WriteLine();
		
			
		}
	
	static void GetSquareDescribe(ISquare square, GameController game){
		if (square is SpecialSquare specialSquare)
		{
			Console.WriteLine($"Name       : {specialSquare.Name}");
			Console.WriteLine($"Code       : {specialSquare.Code}");
			Console.WriteLine($"Description: {specialSquare.Description}");
			Console.WriteLine("---------------------------------");
			if(square is CardChanceSquare chanceSquare){
				Console.WriteLine("This is Community Card Square");
				ICard card = game.DrawCardCommunity();
				if(card != null){
					CardDescribe(card);
				}
			}
			else if (square is CardCommunitySquare communitySquare)
			{
				Console.WriteLine("This is a chance card square");
				ICard card = game.DrawCardChance();
				if (card != null)
				{
					CardDescribe(card);
				}
			}
		}
	}
}
