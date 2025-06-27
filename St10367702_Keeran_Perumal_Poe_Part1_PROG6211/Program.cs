using System;
using System.Media;

namespace CyberLockChatbot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Greet user with voice and logo
            Utility.PlayGreeting();
            Utility.DisplayAsciiArt();

            // Ask for name
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nPlease enter your name: ");
            string userName = Console.ReadLine();
            Console.ResetColor();

            if (string.IsNullOrWhiteSpace(userName))
                userName = "User";

            Console.WriteLine($"\nWelcome, {userName}! I’m your Cybersecurity Awareness Bot.");
            Console.WriteLine("Ask me about phishing, passwords, scams, or safe browsing.");
            Console.WriteLine("Type 'exit' to end the conversation.\n");

            var bot = new CyberBot(userName); // Initialize bot with user name

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("You: ");
                Console.ResetColor();

                string input = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter a valid question.");
                    continue;
                }

                if (input == "exit" || input == "bye")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nBot: Goodbye! Stay safe online.");
                    Console.ResetColor();
                    break;
                }

                string response = bot.RespondTo(input);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nBot: {response}\n");
                Console.ResetColor();
            }
        }
    }
}


//Referance List

//Pieterse, H. (2021). The Cyber Threat Landscape in South Africa: A 10-Year Review. The African Journal of Information and Communication, 28(28). Available at: https://www.scielo.org.za/scielo.php?pid=S2077- [Accessed 22 Apr. 2025].

//NIST(2022).Cybersecurity Framework.National Institute of Standards and Technology. Available at: https://www.nist.gov/cyberframework [Accessed 22 Apr. 2025].

//Google Safety Centre(2023).How to create a strong password. Available at: https://safety.google/passwords/ [Accessed 22 Apr. 2025].

//Norton(2023).What is Phishing? Available at: https://us.norton.com/blog/emerging-threats/what-is-phishing [Accessed 22 Apr. 2025].

//StaySafeOnline.org(2023).Online Safety Basics. National Cybersecurity Alliance. Available at: https://staysafeonline.org/stay-safe-online/ [Accessed 22 Apr. 2025].
