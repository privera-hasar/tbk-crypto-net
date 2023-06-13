namespace tbk_crypto.Commands
{
    public abstract class AbstractCommand
    {
        protected static readonly string CONSOLE_LINE = "======================================================";

        public abstract void Run(string data);

        public static void WriteTitle(string title)
        {
            Console.WriteLine(CONSOLE_LINE);
            Console.WriteLine(title);
            Console.WriteLine(CONSOLE_LINE);
            Console.WriteLine();
        }
    }
}
