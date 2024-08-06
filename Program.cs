// using System.Collections.Generic;
// using MonopolyGame;

// class Program{
//     private static bool startGame;
//     int maxPlayer = 8;
//     static void Main(){
//         IDice dice= new Dice(new int[] {1,2,3,4,5,6});
//         dice.RollTwoDice(out int firstRoll, out int secondRoll);
//         HandleDiceRolled(firstRoll, secondRoll, firstRoll + secondRoll);

//         static void HandleDiceRolled(int firstRoll, int secondRoll, int totalRoll)
//         {
//             // Tampilkan hasil lemparan dadu
//             Console.WriteLine("Hasil dadu pertama: " + firstRoll);
//             Console.WriteLine("Hasil dadu kedua: " + secondRoll);
//             Console.WriteLine("Jumlah total: " + totalRoll);
//         }
//         IBoard board = new Board(40);

//         GameController game = new GameController(board, dice);
//         // board.InitializeBoard();
//         // board.DisplayBoard();
//         game.Start();
//         // board.InitializeBoard();
//         game.DisplayBoard();

//         PlayerData playerData1 = new PlayerData(1, PlayerPieces.Battleship, 15000, "Player 1");
//         PlayerData playerData2 = new PlayerData(2, PlayerPieces.RaceCar, 15000, "Player 2");

//         game.AddPlayer(playerData1, playerData1);
//         game.AddPlayer(playerData2, playerData2);



//         Console.WriteLine("Apakah Anda ingin memulai permainan? (Y/N)");
//         string input = Console.ReadLine();
//         if (input?.Trim().ToUpper() == "Y")
//         {
//             Console.WriteLine("Permainan dimulai!");
//             // Mulai giliran pertama atau lempar dadu pertama
//         }
//         else
//         {
//             Console.WriteLine("Permainan belum dimulai.");
//         }

//         // Tampilkan daftar pemain
//         Console.WriteLine("Daftar Pemain:");
//         foreach (var player in game.players)
//         {
//             var playerData = game.GetPlayerData(player);
//             Console.WriteLine($"Id: {player.Id}, Nama: {player.Name}, Potongan: {playerData.Piece}, Saldo: {playerData.balance}");
//         }

//         while (!startGame)
//         {
//             Console.WriteLine("\nEnter player name or type 'start' to begin the game:");
//             string input = Console.ReadLine();

//             if (input.ToLower() == "start")
//             {
//                 if (game.GetPlayers().Count < 2)
//                 {
//                     Console.WriteLine("\nPlease add at least 2 players to start the game.");
//                     continue;
//                 }

//                 startGame = true;
//                 Console.WriteLine("\nGame started!");

//                 // Initialize the board and start the game
//                 board.InitializeBoard();
//                 game.DisplayBoard(); // Menampilkan board di terminal
//             }
//             else
//             {
//                 if (game.GetPlayers().Count >= maxPlayer)
//                 {
//                     Console.WriteLine("\nCannot add more players. The maximum number of players has been reached.");
//                 }
//                 else
//                 {
//                     // Create a new player with initial balance of $2000 (handled in PlayerData)
//                     IPlayer newPlayer = new Player(input);
//                     PlayerData playerData = new PlayerData(newPlayer, 2000);
//                     game.AddPlayer(newPlayer, playerData);
//                     Console.WriteLine($"\n{input} has joined the game with $2000.");
//                 }
//             }
//         }


//         // while (!startGame)
//         // {
//         //     Console.WriteLine("\nEnter player name or type 'start' to begin the game:");

//         //     string input = Console.ReadLine();

//         //     if (input.ToLower() == "start")
//         //     {
//         //         if (game.GetPlayers().Count < 2)
//         //         {
//         //             Console.WriteLine("\nPlease add at least 2 players to start the game.");
//         //             continue;
//         //         }

//         //         startGame = true;
//         //         Console.WriteLine("\nGame started!");
//         //     }
//         //     else
//         //     {
//         //         if (GetPlayers().Count >= maxPlayer)
//         //         {
//         //             Console.WriteLine("\nCannot add more players. The maximum number of players has been reached.");
//         //         }
//         //         else
//         //         {
//         //             AddPlayer(input, 2000);
//         //             Console.WriteLine($"\n{input} has joined the game with $2000.");
//         //         }
//         //     }
//         // }

//     }
// }

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MonopolyGame;

class Program
{
    static void Main()
    {
        Console.WriteLine("########################################################################################");
        Console.WriteLine("|											|");
        Console.WriteLine("|					Monopoly Game					|");
        Console.WriteLine("|											|");
        Console.WriteLine("########################################################################################");

        // Inisialisasi objek dice
        IDice dice = new Dice(new int[] { 1, 2, 3, 4, 5, 6 });

        // Inisialisasi board dan game controller
        Board board = new Board(40);
        GameController gameController = new GameController(board, dice);
        // Menyambungkan event OnDisplayMessage
        gameController.OnDisplayMessage += DisplayMessage;
        // Membaca data JSON dan mengisi board
        board.InitializeBoard();

        bool startGame = false;
        int maxPlayer = 8;
        int playerIdCounter = 1;

        while (!startGame)
        {
            Console.WriteLine("Masukkan nama untuk player yang akan bermain atau 'start' untuk memulai permainan:");
            string input = Console.ReadLine();

            if (input.ToLower() == "start")
            {
                if (gameController.GetPlayers().Count < 2)
                {
                    Console.WriteLine("\nTambahkan player lagi, minimal 2 player");
                    continue;
                }

                startGame = true;
                Console.WriteLine("\nGame started!");
                gameController.Start();               
            }
            else
            {
                if (gameController.GetPlayers().Count >= maxPlayer)
                {
                    Console.WriteLine("\nTidak bisa menambahkan pemain lagi.");
                    startGame = true;
                    Console.WriteLine("\nGame started!");
                    gameController.Start();
                }
                else
                {
                    // Create a new player with initial balance of $2000
                    IPlayer newPlayer = new Player(playerIdCounter, input);
                    PlayerData playerData = new PlayerData(newPlayer, 2000);
                    gameController.AddPlayer(newPlayer, playerData);
                    // int[] myPlayers = new GetPlayer
                    Console.WriteLine($"{input} baru saja bergabung pada permainan dan mendapatkan $2000.");

                    playerIdCounter++;
                }
            }

        }

        // Proceed with the game loop or other game logic here
    }

    private static void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    static void GetPlayerInfo(IPlayer player){
        PlayerData playerInfo = new (player);
    }
}
