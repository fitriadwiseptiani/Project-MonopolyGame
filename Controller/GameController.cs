using System.ComponentModel;
using System.Collections.Generic;

namespace MonopolyGame;

public class GameController
{
	private readonly int _maxPlayer;
	private IDice _dice;
	private IBoard _board;
	private Dictionary<IPlayer, PlayerData> _player;
	private List<IPlayer> players;
	private List<CardChance> _chanceCards;
	public List<CardCommunity> _communityCards;
	private Dictionary<IPlayer, bool> _playerTurn = new();
	// public Action<int>? OnTurnChanged { get; set; }

	public event Action<string> OnDisplayMessage;

	public GameController(IBoard board, IDice dice, int maxPlayer = 8)
	{
		_maxPlayer = maxPlayer;
		_dice = dice;
		_board = board;
		_player = new Dictionary<IPlayer, PlayerData>();
		players = new List<IPlayer>();
		_chanceCards = new List<CardChance>();
		_communityCards = new List<CardCommunity>();

		// CommunityCard
		_communityCards.Add(new BankError(1, "Selamat Anda mendapatkan tambahan dana sebesar 200"));
		_communityCards.Add(new ConsultancyFee(2, "Anda mendapatkan 25 dari konsultasi yang telah dijalankan"));
		_communityCards.Add(new DoctorsFee(3, "Anda harus membayar untuk pemeriksaan dokter"));
		_communityCards.Add(new FromSaleOfStock(5,"Dapat dana nih dari pembelajaanmu"));
		_communityCards.Add(new GoToJailBro(4, "Maaf Anda akan pergi ke penjara"));
		_communityCards.Add(new HolidayFundMatures(6,"Kamu dapat tambahan dana untuk liburan"));
		_communityCards.Add(new IncomeTaxRefund(7,"Selamat Anda mendapatkan dana dari tax yang telah dibayarkan"));
		_communityCards.Add(new JailFree(8, "Anda sudah bebas dan bisa melanjutkan permaianan"));
		_communityCards.Add(new LifeInsuranceMatures(9, "Bagus ini hadiah dari kami untukmu sebesar 100"));
		_communityCards.Add(new PayHospital(10, "Jangan lupa kamu harus membayar tagihan rumah sakit"));
		_communityCards.Add(new PaySchool(11, "Utamakanan kepentingan sekolah anakmu"));
		_communityCards.Add(new YouInherit(12, "Kamu terpilih untuk mendapatkan uang 100, selamat yaa"));		
		
		// ChanceCard
		_chanceCards.Add(new AdvanceToMilan(1, "Player dapat pergi ke Milan"));
		_chanceCards.Add(new AdvanceToGo(2,"Kembalilah ke tempat asalmu"));
		_chanceCards.Add(new AdvanceToLondon(3,"Ayo kita liburan ke London"));
		_chanceCards.Add(new AdvanceToLyon(4,"Beli jajan ke Lyon yuk"));
		_chanceCards.Add(new BankPaysDividend(5,"Kamu dapat keuntungan dari kami sebeasr 100"));
		_chanceCards.Add(new BuildingAndLoanAssociation(6,"Karena kami baik maka kami akan memberimu 150"));
		_chanceCards.Add(new BuildingLoanMatures(7,"Ini kita kasih hadiah buat kamu"));
		_chanceCards.Add(new GeneralRepairs(8,"Kamu harus bayar biaya perbaikan ini"));
		_chanceCards.Add(new GetOutOfJailFree(9,"Selamat kamu sudah bebas"));
		_chanceCards.Add(new GoToJail(10,"Jangan nakal, ini hukumanmu"));
		_chanceCards.Add(new GoToVacation(11, "Sepertinya kamu kurang liburan ayo pergi"));
		_chanceCards.Add(new PayPoorTax(12,"Ayo jangan lupa bayar taxmu"));
		_chanceCards.Add(new SpeedingFine(13,"Sorry ini harus diperbaiki"));
		
		
	}

	public void DisplayBoard()
	{
		_board.DisplayBoard();
	}

	public void AddPlayer(IPlayer player, PlayerData playerData)
	{
		if (_player.Count < _maxPlayer)
		{
			playerData.playerPosition = _board.GetGoSquare();
			_player[player] = playerData;
			players.Add(player);
		}
		else
		{
			throw new ArgumentException("Maaf pemain sudah mencapai jumlah maksimal");
		}
	}
	public Board GetBoard()
	{
		return (Board)_board;
	}
	public PlayerData GetPlayerData(IPlayer player)
	{
		return _player[player];
	}
	public void GetPlayerInfo(IPlayer player)
	{
		if (player == null)
		{
			OnDisplayMessage?.Invoke("Pemain tidak valid.");
			return;
		}

		if (!_player.ContainsKey(player))
		{
			OnDisplayMessage?.Invoke("Pemain tidak ditemukan.");
			return;
		}

		PlayerData data = GetPlayerData(player);

		if (data == null)
		{
			OnDisplayMessage?.Invoke("Data pemain tidak ditemukan.");
			return;
		}

		OnDisplayMessage?.Invoke("\n*Data Player :");
		string playerInfo = $"ID: {player.Id}, \nNama: {player.Name}, \nSaldo: ${data.balance}";

		OnDisplayMessage?.Invoke(playerInfo);
		data.GetPropertiesPlayer(player);
	}
	public void PropertiesOwned(IPlayer player)
	{
		PlayerData data = GetPlayerData(player);
		var aset = data.GetPropertiesPlayer(player);
		foreach (var property in aset)
		{
			OnDisplayMessage?.Invoke($"\n{property.Name}");
		}
	}

	public void GetPropertyInfo(IPlayer player)
	{
		if (player == null)
		{
			OnDisplayMessage?.Invoke("Pemain tidak valid.");
			return;
		}

		if (!_player.ContainsKey(player))
		{
			OnDisplayMessage?.Invoke("Pemain tidak ditemukan.");
			return;
		}
		PlayerData data = GetPlayerData(player);
		ISquare currentSquare = data.playerPosition;

		if (currentSquare is Property property)
		{
			string propertyInfo = $"\nId: {property.Id}, \nNama: {property.Name}, \nHarga: ${property.Price}, \nPemilik: {property.Owner?.Name ?? "Belum Dimiliki"}";
			OnDisplayMessage?.Invoke($"Informasi Properti di Posisi Saat Ini:{propertyInfo}");
		}
		else
		{
			currentSquare.EffectSquare(player, this);
		}
	}

	public List<IPlayer> GetPlayers()
	{
		return players;
	}

	public void Start()
	{
		_board.DisplayBoard();
		int currentPlayerIndex = 0;

		IPlayer currentPlayer = players[currentPlayerIndex];
		TurnPlayer(currentPlayer);
	}

	public bool IsBankrupt(IPlayer player)
	{
		return GetPlayerData(player).balance <= 0;
	}

	public bool IsGameOver()
	{
		return players.Count(p => !IsBankrupt(p)) <= 1;
	}

	public void Finish()
	{
		OnDisplayMessage?.Invoke("Game Over!");
	}

	public void TurnPlayer(IPlayer player)
	{
		GetPlayerInfo(player);

		_dice.RollTwoDice(out int firstRoll, out int secondRoll, out int totalRoll);
		OnDisplayMessage?.Invoke($"\nPlayer {player.Name} melempar dadu: {firstRoll} dan {secondRoll}, total: {totalRoll}");

		MovePlayer(player, totalRoll);
		
		PlayerData data = GetPlayerData(player);
		ISquare currentSquare = data.playerPosition;
		OnDisplayMessage?.Invoke($"\nPosisi {player.Name} sekarang di {currentSquare.Name} ({currentSquare.GetType().Name})");

		GetPropertyInfo(player);

		if (currentSquare is Property property)
		{
			if (property.Owner == null)
			{
				OnDisplayMessage?.Invoke($"\nApakah Player {player.Name} ingin membeli {property.Name} seharga {property.Price}? (Y/N)");
				string input = Console.ReadLine();
				if (input?.Trim().ToUpper() == "Y")
				{
					if (data.balance >= property.Price)
					{
						property.BuyProperty(player, this);
						OnDisplayMessage?.Invoke($"Player {player.Name} membeli {property.Name}.");
					}
					else
					{
						OnDisplayMessage?.Invoke($"Player {player.Name} tidak memiliki cukup uang untuk membeli {property.Name}.");
					}
				}

			}
			else
			{
				if (property.Owner != player)
				{
					property.PayRent(player, this);
					GetPlayerData(property.Owner).AddBalance(property.RentPrice);
					OnDisplayMessage?.Invoke($"Player {player.Name} membayar sewa {property.RentPrice} kepada {property.Owner.Name}.");
				}
			}

		}
		else
		{
			currentSquare.EffectSquare(player,this);
			// OnDisplayMessage?.Invoke($"{currentSquare.Description}");
		}
		NextTurnPlayer(player);
	}
	public void NextTurnPlayer(IPlayer player)
	{
		int currentPlayer = players.IndexOf(player);
		int nextPlayerIndex = (currentPlayer + 1) % players.Count;
		IPlayer nextPlayer = players[nextPlayerIndex];

		OnDisplayMessage?.Invoke($"\nGiliran berpindah ke Player {nextPlayer.Name}.");
		
		_playerTurn[player] = false;
		_playerTurn[nextPlayer] = true;

		
		if (IsGameOver())
		{
			Finish();
			GetWinner();
		}
		else
		{
			TurnPlayer(nextPlayer);
		}
	}

	public void MovePlayer(IPlayer player, int diceRoll)
	{
		PlayerData data = GetPlayerData(player);
		int currentIndex = GetPlayerPosition(data);
		int newIndex = (currentIndex + diceRoll) % _board.SquareBoard.Count;

		data.playerPosition = _board.SquareBoard[newIndex];
	}
	public void MovePlayerToSquare(IPlayer player, ISquare targetSquare)
	{
		PlayerData data = GetPlayerData(player);
		int currentIndex = GetPlayerPosition(data);
		int targetIndex = _board.SquareBoard.IndexOf(targetSquare);
		// Perbarui posisi pemain
		data.playerPosition = targetSquare;
	}
	public void HandleGoToJail(IPlayer player)
	{
		PlayerData data = GetPlayerData(player);
		var positionPlayer = _board.SquareBoard[10];
		data.playerPosition = positionPlayer;
		NextTurnPlayer(player);
	}
	public void HandleGetOutJail(IPlayer player)
	{
		PlayerData data = GetPlayerData(player);
		data.DeductBalance(50);
		var positionPlayer = _board.SquareBoard[10];
		data.playerPosition = positionPlayer;
	}
	public int GetPlayerPosition(PlayerData data)
	{
		return _board.SquareBoard.IndexOf(data.playerPosition);
	}
	public ICard DrawCardChance()
	{
		if (_chanceCards.Count == 0)
		{
			OnDisplayMessage?.Invoke("Tidak ada kartu Chance yang tersedia.");
			return null; 
		}
		Random rdm = new Random();
		int index = rdm.Next(_chanceCards.Count);
		ICard card = _chanceCards[index];
		_chanceCards.RemoveAt(index);
		return card;
	}
	public ICard DrawCardCommunity()
	{
		if (_communityCards.Count == 0)
		{
			OnDisplayMessage?.Invoke("Tidak ada kartu Community yang tersedia.");
			return null; 
		}

		Random rdm = new Random();
		int index = rdm.Next(_communityCards.Count);
		ICard card = _communityCards[index];
		_communityCards.RemoveAt(index);
		return card;
	}
	public bool PayTax (IPlayer player, int amountOfMoney)
	{
		if (player == player)
		{
			GetPlayerData(player).DeductBalance(15);
			return true;
		}
		return false;
	}
	public void GetWinner()
	{
		var winner = players.FirstOrDefault(p => !IsBankrupt(p));
		if (winner != null)
		{
			OnDisplayMessage?.Invoke($"Pemenang dalam permainan kali ini adalah {winner.Name}");
		}
		else
		{
			OnDisplayMessage?.Invoke("Tidak ada pemenang.");
		}
	}
}

