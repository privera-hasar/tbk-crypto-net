namespace tbk_crypto.Commands
{
    internal class CommandLine
    {
        public Commands Command { get; set; }
        public string? Argument { get; set; }

        public CommandLine(Commands command)
        {
            Command = command;
        }

        public CommandLine(Commands command, string argument)
        {
            Command = command;
            Argument = argument;
        }
    }
}
