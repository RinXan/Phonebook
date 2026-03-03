namespace Phonebook.Presentation
{
    public class ConsoleHelper
    {
        public static void Write(string message)
        {
            Console.WriteLine(message);
        }
        public static void WriteHeader(string header)
        {
            Console.Clear();
            Console.WriteLine(header);
        }
        public static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void WriteSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void WriteInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
