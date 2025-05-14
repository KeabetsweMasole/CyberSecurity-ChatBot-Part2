using System;
using System.Text.RegularExpressions;

namespace POE
{
    //This is the main class for the CyberBot.
    public class CyberBot
    {
        //Private fields for the CyberBot class.
        private string name;
        private AudioImageHandler mediaHandler;
        private QuestionHandler questionHandler;
        private MemoryManager memoryManager;

        //Constructor for the CyberBot class.
        public CyberBot()
        {
            mediaHandler = new AudioImageHandler();
            questionHandler = new QuestionHandler();
            memoryManager = new MemoryManager();
            memoryManager.EnsureFileExists();
        }

        //This method will run the CyberBot.
        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            ShowLoading();
            mediaHandler.DisplayLogo();
            mediaHandler.PlayWelcomeAudio();
            WelcomeUser();
            Menu();
        }

        //This method will display the loading screen.
        private void ShowLoading(int seconds = 3)
        {
            Console.Write("\n The CSB (CyberSecurity Bot) is getting ready to help...");
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".");
                System.Threading.Thread.Sleep(1000);
            }
            Console.WriteLine();

        }//End of ShowLoading method.

        //This method will welcome the user.
        private void WelcomeUser()
        {
            Console.WriteLine("\n===================================================================================================");
            Console.WriteLine("CSB AI: Hello! Welcome to the Cybersecurity Awareness Bot. I'm here to help you stay safe online.");
            Console.WriteLine("===================================================================================================");
            Console.WriteLine("CSB AI: Please enter your full name (letters only):");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("User: ");
            name = ValidateName(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===================================================================================================");
            Console.WriteLine("CSB AI: Your full name is: " + name);
            Console.WriteLine("===================================================================================================");

        }//End of WelcomeUser method.

        //This method will validate the user's name and make sure there's no numbers.
        private string ValidateName(string input)
        {
            while (string.IsNullOrWhiteSpace(input) || !System.Text.RegularExpressions.Regex.IsMatch(input, "^[a-zA-Z ]+$"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid name! Please enter a name with letters only.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("User: ");
                input = Console.ReadLine();

            }//End of while loop.

            return input;

        }//End of ValidateName method.

        //This method will display the menu to the user.
        private void Menu()
        {
            while (true)
            {
                Console.WriteLine("\n===================================================================================================");
                Console.WriteLine("CSB AI: Would you like to ask some questions?");
                Console.WriteLine("         [y] Yes  |  [n] No (exit)  |  [p] View chat history");
                Console.WriteLine("===================================================================================================");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(name + ": ");
                string answer = Console.ReadLine()?.ToLower();
                Console.ForegroundColor = ConsoleColor.Cyan;


                //Switch statement to handle the user's input.
                switch (answer)
                {
                    case "y":
                        questionHandler.HandleQuestions(name);
                        break;

                    case "n":
                        Console.WriteLine("\n===================================================================================================");
                        Console.WriteLine("I hope I answered your questions. Feel free to come back at any time if you need more help.");
                        Console.WriteLine("===================================================================================================");
                        return;

                    case "p":
                        Console.WriteLine("\n==== Chat History ====");
                        var history = memoryManager.GetHistory();
                        foreach (string line in history)
                        {
                            Console.WriteLine(line);
                        }
                        break;

                    default:
                        Console.WriteLine("\n=======================================================================================================");
                        Console.WriteLine("Invalid input! Please enter 'y' for yes or 'n' for no, or enter 'p' to view previous conversation history");
                        Console.WriteLine("=========================================================================================================");
                        break;

                }//End of switch.

            }//End of while loop.

        }//End of Menu method.

    }//End of CyberBot class.

}//End of POE namespace.