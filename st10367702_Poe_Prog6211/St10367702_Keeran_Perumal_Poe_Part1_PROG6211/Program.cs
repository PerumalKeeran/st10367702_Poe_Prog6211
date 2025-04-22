using System;              // Provides basic functionalities like input/output
using System.Media;        // Enables playing WAV audio files

namespace St10367702_Keeran_Perumal_Poe_Part1_PROG6211
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Play the audio greeting when the program starts
            PlayGreeting();

            // Display the Cyber Lock ASCII logo
            DisplayAsciiArt();

            // Ask for the user's name and personalize the experience
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nPlease enter your name: ");
            string userName = Console.ReadLine();
            Console.ResetColor();

            // If no name is entered, use "User" by default
            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = "User";
            }

            // Display the welcome message and instructions
            Console.WriteLine($"\nWelcome, {userName}! I’m your Cybersecurity Awareness Bot.");
            Console.WriteLine("You can ask me about phishing, passwords, and safe browsing.");
            Console.WriteLine("Type 'exit' to leave the chat.");
            Console.WriteLine("\n══════════════════════════════════════════════════════════════════════\n");

            // Start chatbot loop
            while (true)
            {
                // Get user input
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("You: ");
                Console.ResetColor();

                string userInput = Console.ReadLine()?.ToLower().Trim();

                // Handle empty input
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Please enter a valid question.");
                    continue;
                }

                // Exit if the user types "exit" or "bye"
                if (userInput == "exit" || userInput == "bye")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nBot: Goodbye! Stay safe online.");
                    Console.ResetColor();
                    break;
                }

                // Respond to the user's question
                string response = GetBotResponse(userInput);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nBot: {response}\n");
                Console.ResetColor();
            }
        }

        // Plays the recorded voice greeting to improve user engagement
        static void PlayGreeting()
        {
            try
            {
                string path = "greeting.wav";
                SoundPlayer player = new SoundPlayer(path);
                player.PlaySync(); // Improves interactivity with a human-like greeting (Pieterse, 2021)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Audio error]: {ex.Message}");
            }
        }

        // Displays the ASCII art logo with a Cyber Lock title for branding
        static void DisplayAsciiArt()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║                          C Y B E R   L O C K                         ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");

            Console.ResetColor();
        }

        // Generates appropriate bot responses to questions about cybersecurity
        static string GetBotResponse(string input)
        {
            switch (input)
            {
                case "how are you?":
                    return "I'm doing well, thank you! I'm here to help you stay cyber safe.";

                case "what's your purpose?":
                case "what is your purpose?":
                    return "I educate users on cybersecurity threats like phishing, weak passwords, and unsafe browsing. (NIST, 2022)";

                case "what can i ask you about?":
                    return "Ask me about phishing, strong passwords, safe browsing habits, and online safety tips. (StaySafeOnline, 2023)";

                case "what is phishing?":
                    return "Phishing is a cybercrime where attackers impersonate trusted sources—usually through emails or messages—to trick you into sharing personal information, like passwords or credit card numbers. (Norton, 2023)";

                case "give me an example of a strong password.":
                    return "A strong password example is: P@ssw0rd!2025 — it combines uppercase, lowercase, symbols, and numbers. Avoid personal info like your name or birthday. (Google Safety Centre, 2023)";

                case "give me a few safe browsing habits.":
                    return "1. Avoid clicking unknown links.\n2. Always check for 'https://' in URLs.\n3. Don’t download files from sketchy websites.\n4. Keep your browser and antivirus updated. (StaySafeOnline, 2023)";

                default:
                    return "I didn’t quite understand that. Could you rephrase your question?";
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
