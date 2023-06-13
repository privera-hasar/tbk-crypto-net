namespace tbk_crypto.Commands
{
    public class CommandLineProcessor
    {
        private const string ENCRYPT = "--encrypt";
        private const string ENCRYPT_SHORT = "-e";
        private const string DECRYPT = "--decrypt";
        private const string DECRYPT_SHORT = "-d";

        private const string ENCRYPT_HASAR = "--encrypt-hasar";
        private const string ENCRYPT_HASAR_SHORT = "-E";
        private const string DECRYPT_TBK = "--decrypt-tbk";
        private const string DECRYPT_TBK_SHORT = "-D";

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

            if (commandValue == ENCRYPT || commandValue == ENCRYPT_SHORT)
            {
                return new CommandLine(SupportedCommands.Encrypt, args[2]);
            }

            if (commandValue == DECRYPT || commandValue == DECRYPT_SHORT)
            {
                return new CommandLine(SupportedCommands.Decrypt, args[2]);
            }

            if (commandValue == ENCRYPT_HASAR || commandValue == ENCRYPT_HASAR_SHORT)
            {
                return new CommandLine(SupportedCommands.EncryptHasar, args[2]);
            }

            if (commandValue == DECRYPT_TBK || commandValue == DECRYPT_TBK_SHORT)
            {
                return new CommandLine(SupportedCommands.DecryptTbk, args[2]);
            }

            return new CommandLine(SupportedCommands.Error);
        }

    }
}
