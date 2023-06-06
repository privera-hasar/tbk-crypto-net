using System.Text;

namespace tbk_crypto.Infrastructure
{
    internal class FileReader : IFileReader
    {
        public string ReadFile(string fileName)
        {
            Console.WriteLine("Reading file: " + fileName);
            return File.ReadAllText(fileName, Encoding.UTF8);
        }
    }
}
