using MonopolyGame;
using System.Collections.Generic;

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

		board.InitializeBoard();
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
				DisplayBoard();

				while (!game.CheckWinner())
				{
					IPlayer currentPlayer = game.GetCurrentPlayer();
					Console.WriteLine($"Giliran {currentPlayer.Name}");

					// Start turn for the current player
					game.StartTurn();
					game.SetTurnPlayer(currentPlayer);
					GetPlayerInfo(currentPlayer);
					GetPropertiesInfo(currentPlayer);
					BuyPropertyPlayer(currentPlayer);
					game.EndTurn();
					// Change player turn
					game.ChangeTurnPlayer();
				}

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

	private static void DisplayBoard()
	{
		IBoard board = game.GetBoard();
		foreach (var square in board.SquareBoard)
		{
			Console.WriteLine($"ID: {square.Id}, Name: {square.Name}");
		}
	}
	private static PlayerData GetPlayerData(IPlayer player)
	{
		return game.GetPlayerData(player);
	}

	public static void GetPlayerInfo(IPlayer player)
	{
		IPlayer currentPlayer = game.GetCurrentPlayer();
		if (currentPlayer != null)
		{
			PlayerData data = GetPlayerData(currentPlayer);
			ISquare currentSquare = data.playerPosition;
			Console.WriteLine($"Posisi {player.Name} sekarang di {currentSquare.Name} ({currentSquare.GetType().Name})");

			if (data == null)
			{
				Console.WriteLine("Data pemain tidak ditemukan.");
				return;
			}

			Console.WriteLine("\n*Data Player :");
			string playerInfo = $"ID: {currentPlayer.Id}, \nNama: {currentPlayer.Name}, \nSaldo: ${data.Balance}";

			Console.WriteLine(playerInfo);
			data.GetPropertiesPlayer();
		}
		else
		{
			Console.WriteLine("Tidak ada pemain yang sedang bermain saat ini.");
		}
	}

	public static void GetPropertiesInfo(IPlayer player)
	{
		IPlayer currentPlayer = game.GetCurrentPlayer();
		if (currentPlayer != null)
		{
			PlayerData data = GetPlayerData(currentPlayer);
			if (data == null)
			{
				Console.WriteLine("Data pemain tidak ditemukan.");
				return;
			}

			ISquare currentSquare = data.playerPosition;
			if (currentSquare is Property property)
			{
				string propertyInfo = $"\nId: {property.Id}, \nNama: {property.Name}, \nHarga: ${property.Price}, \nPemilik: {property.Owner?.Name ?? "Belum Dimiliki"}";
				Console.WriteLine($"Informasi Properti di Posisi Saat Ini:{propertyInfo}");
			}
			else
			{
				Console.WriteLine("Posisi saat ini bukan properti.");
				currentSquare.EffectSquare(currentPlayer, game);
			}
		}
		else
		{
			Console.WriteLine("Tidak ada pemain yang sedang bermain saat ini.");
		}
	}

	public static void BuyPropertyPlayer(IPlayer player)
	{
		PlayerData data = GetPlayerData(player);
		ISquare currentSquare = data.playerPosition;
		if (currentSquare is Property property)
		{
			if (property.Owner == null)
			{
				Console.WriteLine($"\nApakah Player {player.Name} ingin membeli {property.Name} seharga {property.Price}? (Y/N)");
				string input = Console.ReadLine();
				if (input?.Trim().ToUpper() == "Y")
				{
					if (data.Balance >= property.Price)
					{
						property.BuyProperty(player, game);
						Console.WriteLine($"Player {player.Name} membeli {property.Name}.");
					}
					else
					{
						Console.WriteLine($"Player {player.Name} tidak memiliki cukup uang untuk membeli {property.Name}.");
					}
				}

			}
			else
			{
				if (property.Owner != player)
				{
					property.PayRent(player, game);
					GetPlayerData(property.Owner).AddBalance(property.RentPrice);
					Console.WriteLine($"Player {player.Name} membayar sewa {property.RentPrice} kepada {property.Owner.Name}.");
				}
			}

		}
		else
		{
			currentSquare.EffectSquare(player, game);
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
