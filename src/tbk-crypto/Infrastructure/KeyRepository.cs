using System.Text.Json;
using tbk_crypto.Entities;

namespace tbk_crypto.Infrastructure
{
    public class KeyRepository : IKeyRepository
    {
        private static readonly string KEYS_FILE = "keys.json";
        private static readonly string PUBLIC_KEY_FILE = "public-key.json";

        private KeyPair? keys;
        private PublicKey? publicKey;

        private readonly IFileReader reader;

        public KeyRepository(IFileReader reader)
        {
            this.reader = reader;
        }

        public PublicKey GetPublicKey()
        {
            if (publicKey == null)
            {
                Console.WriteLine("Reading public key file");
                string value = reader.ReadFile(PUBLIC_KEY_FILE);
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
                string value = reader.ReadFile(KEYS_FILE);
                Console.WriteLine(value);
                keys = JsonSerializer.Deserialize<KeyPair>(value);
            }

            return keys;
        }
    }
}
