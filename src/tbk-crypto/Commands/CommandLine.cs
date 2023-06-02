namespace tbk_crypto.Commands
{
    public class CommandLine
    {
        public SupportedCommands Command { get; set; }
        public string? Argument { get; set; }

        public CommandLine(SupportedCommands command)
        {
            Command = command;
        }

        public CommandLine(SupportedCommands command, string argument)
        {
            Command = command;
            Argument = argument;
        }
    }
}
