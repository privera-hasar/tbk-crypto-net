using tbk_crypto;
using tbk_crypto.Commands;

AbstractCommand.WriteTitle("TBK Jose Crypto Conceptual Test");

var command = new CommandLineProcessor().ReadCommand();

switch (command.Command)
{
    case SupportedCommands.Encrypt:
        Factory.GetInstance().GetEncryptCommand().Run(command.Argument);
        break;
    case SupportedCommands.Decrypt:
        Factory.GetInstance().GetDecryptCommand().Run(command.Argument);
        break;
    case SupportedCommands.FullTest:
        Factory.GetInstance().GetFullTestCommand().Run(command.Argument);
        break;
    case SupportedCommands.Error:
    default:
        Console.WriteLine("Invalid parameters.");
        break;
}


