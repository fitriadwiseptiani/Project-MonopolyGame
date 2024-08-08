namespace MonopolyGame;

public class GameController
{
	private readonly int _maxPlayer;
	private int _currentPlayer;
	private IBoard _board;
	private IDice _dice;
	private GameStatus _gameStatus;
	private Dictionary<IPlayer, PlayerData> _players;
	private List<ICard> _chanceCards;
	public List<ICard> _communityCards;
	// public Action<IPlayer, IDice> MovePlayer;
	public Action<IPlayer, Property> BuyProperty;
	public Action<IPlayer, Property> RentProperty;
	public Action<IPlayer, ICard> handlecard;

	public GameController(int maxPlayer, IBoard board, IDice dice)
	{
		_maxPlayer = maxPlayer;
		_currentPlayer = 0;
		_board = board;
		_dice = dice;
		_gameStatus = GameStatus.Preparation;
		_players = new Dictionary<IPlayer, PlayerData>();
		_chanceCards = new List<ICard>();
		_communityCards = new List<ICard>();
		
	}
	public Board GetBoard()
	{
		return (Board)_board;
	}
	public void AddPlayer(IPlayer player, PlayerData playerData)
	{
		try
		{
			if (_players.Count < _maxPlayer) // Change <= to < to ensure max player limit
			{
				SetStartPlayerPosition(_board, playerData);
				_players[player] = playerData;
			}
			else
			{
				throw new Exception("Jumlah maksimum pemain telah tercapai");
			}
		}
		catch (Exception e)
		{
			throw new Exception(e.Message);
		}
	}
	public void SetStartPlayerPosition(IBoard board, PlayerData playerData)
	{
		playerData.playerPosition = board.GetGoSquare();
	}
	public bool Start()
	{
		if (!(_players.Count == 2))
		{
			_gameStatus = GameStatus.Play;
			return true;
		}
		return false;
	}
	public bool End()
	{
		return true;
	}
	public List<IPlayer> GetPlayers()
	{
		return _players.Keys.ToList();
	}
	public PlayerData GetPlayerData(IPlayer player)
	{
		return _players[player];
	}
	public void SetTurnPlayer(IPlayer player)
	{
		_dice.RollTwoDice(out int firstRoll, out int secondRoll, out int totalRoll);

		MovePlayer(player, totalRoll);
		// ChangeTurnPlayer();
	}
	public IPlayer GetCurrentPlayer()
	{
		return _players.Keys.ElementAt(_currentPlayer);
	}
	public bool StartTurn()
	{
		return true;
	}
	public bool EndTurn()
	{
		return true;
	}
	public void ChangeTurnPlayer()
	{
		_currentPlayer = (_currentPlayer + 1) % _players.Count;

	}
	public int GetPlayerPosition(PlayerData data)
	{
		return _board.SquareBoard.IndexOf(data.playerPosition);
	}
	public void MovePlayer(IPlayer player, int diceResult)
	{
		PlayerData data = GetPlayerData(player);
		int currentIndex = GetPlayerPosition(data);
		int newIndex = (currentIndex + diceResult) % _board.SquareBoard.Count;

		data.playerPosition = _board.SquareBoard[newIndex];
	}
	public bool DeclareBankrupt(IPlayer player)
	{
		PlayerData playerData = GetPlayerData(player);
		return GetPlayerData(player).Balance <= 0 && !playerData.propertyPlayer.Any();
	}
	public void SetChanceCards(List<ICard> chanceCards)
	{
		_chanceCards = chanceCards;
	}
	public void SetCommunityCards(List<ICard> communityCards)
	{
		_communityCards = communityCards;
	}
	public ICard DrawCardChance()
	{
		if (_chanceCards.Count == 0)
		{
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
			return null;
		}
		Random rdm = new Random();
		int index = rdm.Next(_communityCards.Count);
		ICard card = _communityCards[index];
		_communityCards.RemoveAt(index);
		return card;
	}
	public void HandleGoToJail(IPlayer player)
	{
		PlayerData data = GetPlayerData(player);
		var positionPlayer = _board.SquareBoard[10];
		data.playerPosition = positionPlayer;
		ChangeTurnPlayer();
	}
	public void MovePlayerToSquare(IPlayer player, ISquare targetSquare)
	{
		PlayerData data = GetPlayerData(player);
		int currentIndex = GetPlayerPosition(data);
		int targetIndex = _board.SquareBoard.IndexOf(targetSquare);
		// Perbarui posisi pemain
		data.playerPosition = targetSquare;
	}
	public void HandleGetOutJail(IPlayer player)
	{
		PlayerData data = GetPlayerData(player);
		data.DeductBalance(50);
		var positionPlayer = _board.SquareBoard[10];
		data.playerPosition = positionPlayer;
	}
	// public bool HandleSquareEffect(IPlayer player, ISquare square){

	// }
	// public bool HandleCardEffect(IPlayer player, ICard card){

	// }
	public bool PayTax(IPlayer player, int amountOfMoney)
	{
		if (player == player)
		{
			GetPlayerData(player).DeductBalance(15);
			return true;
		}
		return false;
	}
	public bool CheckWinner()
	{
		int activePlayers = _players.Count(player => !DeclareBankrupt(player.Key));
		return activePlayers <= 1;
	}
	public IPlayer GetWinner()
	{
		var winner = _players.Keys.FirstOrDefault(player => !DeclareBankrupt(player));

		return winner;
	}


}
