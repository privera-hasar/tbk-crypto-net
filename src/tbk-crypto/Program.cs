using tbk_crypto;
using tbk_crypto.Commands;

Console.WriteLine("===========================================");
Console.WriteLine("TBK Jose Crypto Conceptual Test");
Console.WriteLine("===========================================");
Console.WriteLine();


var command = new CommandLineProcessor().ReadCommand();

switch (command.Command)
{
    case Commands.Encrypt:
        Factory.GetInstance().GetEncryptCommand().Run(command.Argument);
        break;
    case Commands.Decrypt:
        Factory.GetInstance().GetDecryptCommand().Run(command.Argument);
        break;
    case Commands.FullTest:
        Factory.GetInstance().GetFullTestCommand().Run(command.Argument);
        break;
    case Commands.Error:
    default:
        Console.WriteLine("Invalid parameters.");
        break;
}


