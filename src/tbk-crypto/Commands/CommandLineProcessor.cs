namespace tbk_crypto.Commands
{
    public class CommandLineProcessor
    {
        private const string ENCRYPT = "--encrypt";
        private const string SHORT_ENCRYPT = "-e";
        private const string DECRYPT = "--decrypt";
        private const string SHORT_DECRYPT = "-d";

        public CommandLine ReadCommand()
        {
            string[] args = Environment.GetCommandLineArgs();
            return ReadCommand(args);
        }

        public CommandLine ReadCommand(string[] args)
        {
            if (args.Length < 1 || args.Length > 3)
            {
                return new CommandLine(SupportedCommands.Error);
            }

            if (args.Length == 1)
            {
                return new CommandLine(SupportedCommands.FullTest, "test");
            }

            if (args.Length == 2)
            {
                return new CommandLine(SupportedCommands.FullTest, args[1]);
            }

            string commandValue = args[1];

            if (commandValue == ENCRYPT || commandValue == SHORT_ENCRYPT)
            {
                return new CommandLine(SupportedCommands.Encrypt, args[2]);
            }

            if (commandValue == DECRYPT || commandValue == SHORT_DECRYPT)
            {
                return new CommandLine(SupportedCommands.Decrypt, args[2]);
            }

            return new CommandLine(SupportedCommands.Error);
        }

    }
}
