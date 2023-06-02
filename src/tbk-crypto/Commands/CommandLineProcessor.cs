using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tbk_crypto.Commands
{
    internal class CommandLineProcessor
    {
        private const string ENCRYPT = "--encrypt";
        private const string SHORT_ENCRYPT = "-e";
        private const string DECRYPT = "--decrypt";
        private const string SHORT_DECRYPT = "-d";

        public CommandLine ReadCommand()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length < 1 || args.Length > 3)
            {
                return new CommandLine(Commands.Error);
            }

            if (args.Length == 1)
            {
                return new CommandLine(Commands.FullTest, "test");
            }

            if (args.Length == 2)
            {
                return new CommandLine(Commands.FullTest, args[1]);
            }

            string commandValue = args[1];

            if (commandValue == ENCRYPT || commandValue == SHORT_ENCRYPT) {
                return new CommandLine(Commands.Encrypt, args[2]);
            }

            if (commandValue == DECRYPT || commandValue == SHORT_DECRYPT)
            {
                return new CommandLine(Commands.Decrypt, args[2]);
            }

            return new CommandLine(Commands.Error);
        }

    }
}
