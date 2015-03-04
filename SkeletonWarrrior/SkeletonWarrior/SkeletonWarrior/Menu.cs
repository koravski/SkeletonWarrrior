//The menu the player sees when he starts the game. Contains the functions for starting new game, difficulty level, leaderboards, credits, exit game
using System;
using System.IO;
using System.Threading;
using System.Linq;

namespace SkeletonWarrior
{
    class Menu
    {
        /// <summary>
        /// Main menu
        /// </summary>
        private static readonly string[] menu = { "Start New Game", "Leaderboards", "Credits", "Exit Game" };

        private static readonly string selector = ">";
        private static int currentSelection = 0;
        private static string characterName = "";
        
        public static void Show()
        {
            ShowLogo();
            while (true)
            {
                ShowMenu();
                HandleInput();
                Thread.Sleep(75);
            }
        }
        public static string CharacterName
        {
            get { return characterName; }
        }
        public static void Init()
        {
            while (characterName == "")
            {
                Console.Clear();
                Console.SetCursorPosition(Console.WindowWidth / 2 - 25, Console.WindowHeight / 2);
                Console.Write("Enter character name: ");
                characterName = Console.ReadLine();
            }

            Console.Clear();
            GameLogic.StartGame();
        }
 
        /// <summary>
        /// Show main menu
        /// </summary>
        private static void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    currentSelection--;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    currentSelection++;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    CheckUserChoice();
                }

                if (currentSelection < 0)
                {
                    currentSelection = menu.GetLength(0) - 1;
                }
                else if (currentSelection > menu.GetLength(0) - 1)
                {
                    currentSelection = 0;
                }
            }
        }

        private static void CheckUserChoice()
        {
            switch (currentSelection)
            {
                case 0:
                    Init();
                    break;
                case 1:
                    ShowLeaderboards();
                    break;
                case 2:
                    ShowCredits();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }

        public static void ShowMenu()
        {
            for (int i = 0; i < menu.GetLength(0); i++)
            {
                if (currentSelection == i)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - menu[i].Length / 2 - selector.Length + 1, Console.WindowHeight / 2 + 2 * i);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(selector + menu[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - menu[i].Length / 2, Console.WindowHeight / 2 + 2 * i);
                    Console.Write(" " + menu[i]);
                }
            }
        }

        private static void ChangeDifficultyLevel()
        {
            Console.Clear();
            //TODO: Change current dificulty
            Console.WriteLine(@"                           _____                 _                                     
                          / ____|               (_)                                    
                          | |     ___  _ __ ___  _ _ __   __ _   ___  ___   ___  _ __  
                          | |    / _ \| '_ ` _ \| | '_ \ / _` | / __|/ _ \ / _ \| '_ \ 
                          | |___| (_) | | | | | | | | | | (_| | \__ \ (_) | (_) | | | |
                           \_____\___/|_| |_| |_|_|_| |_|\__, | |___/\___/ \___/|_| |_|
                                                          __/ |                        
                                                         |___/                         ");
            Console.ReadKey();
            BackToMenu();
        }

        /// <summary>
        /// Read from file all leaders and show them on console
        /// </summary>
        private static void ShowLeaderboards()
        {
            Console.Clear();
            int counter = 0;
            string line;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Console.WindowWidth / 6, Console.WindowHeight / 6);
            Console.WriteLine("Failed Attempts:");
            try
            {
                using (var reader = new StreamReader(GameLogic.FailedAttemptsPath))
                {
                    int failedCount = 1;
                    Console.WriteLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 6, Console.WindowHeight / 5 + counter);
                        Console.WriteLine(failedCount + ". " + line);
                        counter++;
                        failedCount++;
                    }
                    failedCount = 0;
                    Console.ResetColor();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Can't load failed attempts database!");
            }

            int counter1 = 0;
            string line1;
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 6);
            Console.WriteLine("Successful Attempts:");
            try
            {
                using (var reader1 = new StreamReader(GameLogic.SuccessAttemptsPath))
                {
                    int successCount = 1;
                    while ((line1 = reader1.ReadLine()) != null)
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 5 + counter1);
                        Console.WriteLine(successCount + ". " + line1);
                        counter1++;
                        successCount++;
                    }
                    successCount = 0;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Can't load successfully attempts database!");
            }

            Console.ReadKey();
            BackToMenu();
        }

        /// <summary>
        /// Show credits and final text
        /// </summary>
        private static void ShowCredits()
        {
            Console.Clear();
            Thread skull = new Thread(Music);
            skull.Start();

            StreamReader szarvas = new StreamReader(@"..\..\skull.txt");
            string szarvasstring = szarvas.ReadToEnd();
            StreamReader szarvas2 = new StreamReader(@"..\..\skull2.txt");
            string szarvasstring2 = szarvas2.ReadToEnd();
            szarvas.Close();
            szarvas2.Close();
            int i = 0;
            while ((i < szarvasstring.Length) && (szarvasstring[i] != 'Q'))
            {
                i++;
            }

            int l = 0;
            while ((l < szarvasstring2.Length) && (szarvasstring2[l] != 'Q'))
            {
                l++;
            }

            int k = 0;
            while ((k < szarvasstring2.Length) && (szarvasstring2[k] != 'O'))
            {
                k++;
            }

            for (int j = 0; j < 13; j++)
            {
                Console.Write(szarvasstring.Substring(0, i - 1));
                Console.Write(szarvasstring[i]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(szarvasstring.Substring(i + 1));

                Thread.Sleep(500);

                Console.Clear();

                Console.Write(szarvasstring2.Substring(0, l - 1));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(szarvasstring2[l]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(szarvasstring2.Substring(l + 1, szarvasstring2.Length - l - (szarvasstring2.Length - k) - 1));
                Console.Write(szarvasstring2[k]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(szarvasstring2.Substring(k + 1));

                Thread.Sleep(500);

                Console.Clear();

            }

            Console.Write(szarvasstring.Substring(0, i - 1));
            Console.Write(szarvasstring[i]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(szarvasstring.Substring(i + 1));
            
            Console.ReadKey();
            BackToMenu();
        }

        /// <summary>
        /// Return back to main menu
        /// </summary>
        private static void BackToMenu()
        {
            Console.Clear();
            ShowLogo();
            ShowMenu();
        }

        private static void ShowLogo()
        {
            Console.Write(
@"
                 _________-----_____                  
       _____------           __      ----_            
___----             ___------              \                 ___________          .__          __                 
   ----________        ----                 \               /   _____/  | __ ____ |  |   _____/  |_  ____   ____  
               -----__    |             _____)              \_____  \|  |/ // __ \|  | _/ __ \   __\/  _ \ /    \ 
                    __-                /     \              /        \    <\  ___/|  |_\  ___/|  | (  <_> )   |  \
        _______-----    ___--          \    /)\            /_______  /__|_ \\___  >____/\___  >__|  \____/|___|  /
  ------_______      ---____            \__/  /                    \/     \/    \/          \/                 \/ 
               -----__    \ --    _          /\             __      __                     .__              
                      --__--__     \_____/   \_/\          /  \    /  \_____ ______________|__| ___________ 
                              ----|   /          |         \   \/\/   /\__  \\_  __ \_  __ \  |/  _ \_  __ \
                                  |  |___________|          \        /  / __ \|  | \/|  | \/  (  <_> )  | \/
                                  |  | ((_(_)| )_)           \__/\  /  (____  /__|   |__|  |__|\____/|__|
                                  |  \_((_(_)|/(_)
                                  \             (
                                   \_____________)");
        }

        #region musicForTheCredits
        static void Music()
        {
            Console.Beep(440, 500);
            Console.Beep(440, 500);
            Console.Beep(440, 500);
            Console.Beep(349, 350);
            Console.Beep(523, 150);
            Console.Beep(440, 500);
            Console.Beep(349, 350);
            Console.Beep(523, 150);
            Console.Beep(440, 1000);
            for (int i = 0; i < 2; i++)
            {
                Console.Beep(659, 500);
                Console.Beep(659, 500);
                Console.Beep(659, 500);
                Console.Beep(698, 350);
                Console.Beep(523, 150);
                Console.Beep(415, 500);
                Console.Beep(349, 350);
                Console.Beep(523, 150);
                Console.Beep(440, 1000);
            }

            Console.ReadKey();
            BackToMenu();

        }
        static void Kurzor(int j)
        {
            Console.SetCursorPosition(2 * j + 2, 0);
        }
        #endregion
    }
}
