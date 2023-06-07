using System.Text.Json;
using tbk_crypto.Entities;

namespace tbk_crypto.Infrastructure
{
    public class KeyRepository : IKeyRepository
    {
        private static readonly string KEYS_FILE = "keys.json";
        private static readonly string PUBLIC_KEY_FILE = "public-key.json";

        private string? keys;
        private string? publicKey;

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
                publicKey = reader.ReadFile(PUBLIC_KEY_FILE);
                Console.WriteLine(publicKey);
            }

            return JsonSerializer.Deserialize<PublicKey>(publicKey);
        }

        public string GetJsonPublicKey()
        {
            if (publicKey == null)
            {
                Console.WriteLine("Reading public key file");
                publicKey = reader.ReadFile(PUBLIC_KEY_FILE);
                Console.WriteLine(publicKey);
            }

            return publicKey;
        }

        public KeyPair GetKeys()
        {
            if (keys == null)
            {
                Console.WriteLine("Reading key pair file");
                keys = reader.ReadFile(KEYS_FILE);
                Console.WriteLine(keys);
            }

            return JsonSerializer.Deserialize<KeyPair>(keys); ;
        }
    }
}
