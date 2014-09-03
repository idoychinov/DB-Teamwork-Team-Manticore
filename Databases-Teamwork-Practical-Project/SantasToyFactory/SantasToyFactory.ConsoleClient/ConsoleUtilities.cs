namespace SantasToyFactory.ConsoleClient
{
    using System;

    public static class ConsoleUtilities
    {
        const ConsoleColor ConsoleMenuColor = ConsoleColor.Cyan;
        const ConsoleColor ConsoleErrorColor = ConsoleColor.Red;
        const ConsoleColor ConsoleSuccessColor = ConsoleColor.Green;
        const ConsoleColor ConsoleProcessingColor = ConsoleColor.DarkYellow;
        const ConsoleColor ConsoleDefaultColor = ConsoleColor.White;

        private static void ConsoleMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleDefaultColor;
        }

        public static void MenuMessage(string message)
        {
            ConsoleMessage(message, ConsoleMenuColor);
        }

        public static void ErrorMessage(string message)
        {
            ConsoleMessage(message, ConsoleErrorColor);
        }

        public static void SuccessMessage(string message)
        {
            ConsoleMessage(message, ConsoleSuccessColor);
        }

        public static void ProcessingMessage(string message)
        {
            ConsoleMessage(message, ConsoleProcessingColor);
        }
    }
}
