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
			if (_players.Count <= _maxPlayer) // Change <= to < to ensure max player limit
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
		if (_players.Count == 2 || _players.Count <= _maxPlayer)
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
	public int GetPlayerId(IPlayer player){
		if(_players.ContainsKey(player)){
			return player.Id;
		}
		throw new Exception();
	}
	public string GetPlayerName(IPlayer player){
		if(_players.ContainsKey(player)){
			return player.Name;
		}
		throw new Exception();
	}
	public PlayerPieces GetPlayerPiece(IPlayer player){
		if(_players.ContainsKey(player)){
			return _players[player].Piece;
		}
		throw new Exception();
	}
	public int GetPlayerBalance(IPlayer player){
		if(_players.ContainsKey(player)){
			return _players[player].Balance;
		}
		throw new Exception();
	}
	public List<Property> GetPlayerProperty(IPlayer player){
		if(_players.ContainsKey(player)){
			return _players[player].propertyPlayer;
		}
		throw new Exception();
	}
	public List<ICard> GetPlayerCardSave(IPlayer player){
		if(_players.ContainsKey(player)){
			return _players[player].cardSpesialSave;
		}
		throw new Exception();
	}
	// public PlayerData GetPlayerData(IPlayer player)
	// {
	// 	return _players[player];
	// }
	public ISquare GetPlayerPosition(IPlayer player){
		if (_players.ContainsKey(player))
		{
			return _players[player].playerPosition;
		}
		throw new Exception();
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
		ISquare currentPosition = GetPlayerPosition(player);
		int currentIndex = _board.SquareBoard.IndexOf(currentPosition);

		int newIndex = (currentIndex + diceResult) % _board.SquareBoard.Count;

		ISquare newPosition = _board.SquareBoard[newIndex];
		_players[player].playerPosition = newPosition;
	}
	public bool DeclareBankrupt(IPlayer player)
	{
		return GetPlayerBalance(player) <= 0 && !GetPlayerProperty(player).Any();
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
		var newPosition = _board.SquareBoard[10];
		_players[player].playerPosition = newPosition;
		ChangeTurnPlayer();
	}
	public void MovePlayerToSquare(IPlayer player, ISquare targetSquare)
	{
		// ISquare currentPosition = GetPlayerPosition(player);
		int targetIndex = _board.SquareBoard.IndexOf(targetSquare);

		ISquare newPosition = _board.SquareBoard[targetIndex];
		_players[player].playerPosition = newPosition;
	}
	public void HandleGetOutJail(IPlayer player)
	{
		var data = _players[player];
		data.DeductBalance(50);
		var newPosition = _board.SquareBoard[10];
		_players[player].playerPosition = newPosition;
	}
	// public bool HandleSquareEffect(IPlayer player, ISquare square){

	// }
	// public bool HandleCardEffect(IPlayer player, ICard card){

	// }
	public bool PayTax(IPlayer player, int amountOfMoney)
	{
		if (player == player)
		{
			var data = _players[player];
			data.DeductBalance(15);
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
