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
                    IPlayer newPlayer = new Player(playerIdCounter, input);
                    PlayerData playerData = new PlayerData(newPlayer, 0);
                    gameController.AddPlayer(newPlayer, playerData);
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
