using System;
using System.Media;

namespace CyberLockChatbot
{
    public static class Utility
    {
        public static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("greeting.wav");
                player.PlaySync();
            }
            catch
            {
                Console.WriteLine("[Audio greeting failed to play]");
            }
        }

        public static void DisplayAsciiArt()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                          C Y B E R   L O C K                         ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }
    }
}
