using System.Text;
using System.Text.Json;
using tbk_crypto.Entities;

namespace tbk_crypto.Infrastructure
{
    public class KeyRepository : IKeyRepository
    {
        private static string KEYS_FILE = "keys.json";
        private static string PUBLIC_KEY_FILE = "public-key.json";

        private KeyPair keys;
        private PublicKey publicKey;

        public string ReadFile(string fileName)
        {
            Console.WriteLine("Reading file: " + fileName);
            return File.ReadAllText(fileName, Encoding.UTF8);
        }

        public PublicKey GetPublicKey()
        {
            if (publicKey == null)
            {
                Console.WriteLine("Reading public key file");
                string value = ReadFile(PUBLIC_KEY_FILE);
                Console.WriteLine(value);
                publicKey = JsonSerializer.Deserialize<PublicKey>(value);
            }

            return publicKey;
        }

        public KeyPair GetKeys()
        {
            if (keys == null)
            {
                Console.WriteLine("Reading key pair file");
                string value = ReadFile(KEYS_FILE);
                Console.WriteLine(value);
                keys = JsonSerializer.Deserialize<KeyPair>(value);
            }

            return keys;
        }
    }
}
