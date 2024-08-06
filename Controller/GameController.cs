// 

using System.ComponentModel;

namespace MonopolyGame;

public class GameController
{
    private readonly int _maxPlayer;
    private IDice _dice;
    private IBoard _board;
    private Dictionary<IPlayer, PlayerData> _player;
    public List<IPlayer> players;
    public List<CardChance> ChanceCards;
    public List<CardCommunity> CommunityCards;
    private Dictionary<IPlayer, bool> _playerTurn = new();
    private Dictionary<IPlayer, bool> _playerHasPlayed = new();
    public Action<int>? OnTurnChanged { get; set; }

    public event Action<string> OnDisplayMessage;

    public GameController(IBoard board, IDice dice, int maxPlayer = 8)
    {
        _maxPlayer = maxPlayer;
        _dice = dice;
        _board = board;
        _player = new Dictionary<IPlayer, PlayerData>();
        players = new List<IPlayer>();
        ChanceCards = new List<CardChance>();
        CommunityCards = new List<CardCommunity>();
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

        string propertiesOwned = data.PropertyPlayer != null && data.PropertyPlayer.Count > 0
            ? string.Join(", ", data.PropertyPlayer.Select(p => p.Name))
            : "Tidak ada properti";
        
        OnDisplayMessage?.Invoke("\n*Data Player :");
        string playerInfo = $"ID: {player.Id}, \nNama: {player.Name}, \nSaldo: ${data.balance}, \nProperti: {propertiesOwned}";

        OnDisplayMessage?.Invoke(playerInfo);
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
        
        // // Pastikan pemain memiliki giliran
        // if (!_playerTurn.ContainsKey(player) || !_playerTurn[player])
        // {
        //     _playerTurn[player] = true;
        // }
        // else
        // {
        //     OnDisplayMessage?.Invoke($"Player {player.Name} sudah bermain.");
        //     return;
        // }

        

        GetPlayerInfo(player);

        // Gulirkan dadu
        _dice.RollTwoDice(out int firstRoll, out int secondRoll, out int totalRoll);
        OnDisplayMessage?.Invoke($"Player {player.Name} melempar dadu: {firstRoll} dan {secondRoll}, total: {totalRoll}");

        MovePlayer(player, totalRoll);
        // Tampilkan posisi baru pemain
        PlayerData data = GetPlayerData(player);
        ISquare currentSquare = data.playerPosition;
        OnDisplayMessage?.Invoke($"\nPosisi {player.Name} sekarang di {currentSquare.Name} ({currentSquare.GetType().Name})");

        if (currentSquare is Property property)
    {
        if (property.Owner == null)
        {
            OnDisplayMessage?.Invoke($"Apakah Player {player.Name} ingin membeli {property.Name} seharga {property.Price}? (Y/N)");
            string input = Console.ReadLine();
            if (input?.Trim().ToUpper() == "Y")
            {
                if (data.balance >= property.Price)
                {
                    data.DeductBalance(property.Price);
                    property.SetOwner(player); // Gunakan metode SetOwner
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
                int rent = property.CalculateRent();
                data.DeductBalance(rent);
                GetPlayerData(property.Owner).AddBalance(rent);
                OnDisplayMessage?.Invoke($"Player {player.Name} membayar sewa {rent} kepada {property.Owner.Name}.");
            }
        }
        
    }
        NextTurnPlayer(player);
    }
    public void NextTurnPlayer(IPlayer player){

        // // Pastikan pemain saat ini telah selesai bermain
        // if (!_playerTurn.ContainsKey(player) || !_playerTurn[player])
        // {
        //     _playerTurn[player] = true;
        // }
        int currentPlayer = players.IndexOf(player);
        int nextPlayerIndex = (currentPlayer + 1) % players.Count;
        IPlayer nextPlayer = players[nextPlayerIndex];

        _playerTurn[player] = false;
        _playerTurn[nextPlayer] = true;

        OnDisplayMessage?.Invoke($"Giliran berpindah ke Player {nextPlayer.Name}.");
        TurnPlayer(nextPlayer);
    }

    public void MovePlayer(IPlayer player, int diceRoll)
    {
        PlayerData data = GetPlayerData(player);
        int currentIndex = GetPlayerPosition(data);
        int newIndex = (currentIndex + diceRoll) % _board.SquareBoard.Count;
        data.playerPosition = _board.SquareBoard[newIndex];
    }

    public int GetPlayerPosition(PlayerData data)
    {
        return _board.SquareBoard.IndexOf(data.playerPosition);
    }
}

