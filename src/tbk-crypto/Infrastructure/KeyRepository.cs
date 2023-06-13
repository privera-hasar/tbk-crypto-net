using System.Text.Json;
using tbk_crypto.Entities;

namespace tbk_crypto.Infrastructure
{
    public class KeyRepository : IKeyRepository
    {
        private static readonly string HASAR_KEYS_FILE = "hasar-keys.json";
        private static readonly string HASAR_PUBLIC_KEY_FILE = "hasar-public-key.json";
        private static readonly string TBK_KEYS_FILE = "tbk-keys.json";
        private static readonly string TBK_PUBLIC_KEY_FILE = "tbk-public-key.json";

        private string? hasarKeys;
        private string? hasarPublicKey;
        private string? tbkKeys;
        private string? tbkPublicKey;

        private readonly IFileReader reader;

        public KeyRepository(IFileReader reader)
        {
            this.reader = reader;
        }

        public PublicKey GetHasarPublicKey()
        {
            if (hasarPublicKey == null)
            {
                Console.WriteLine("Reading Hasar public key file");
                hasarPublicKey = reader.ReadFile(HASAR_PUBLIC_KEY_FILE);
                Console.WriteLine(hasarPublicKey);
            }

            return JsonSerializer.Deserialize<PublicKey>(hasarPublicKey);
        }

        public string GetJsonHasarPublicKey()
        {
            if (hasarPublicKey == null)
            {
                Console.WriteLine("Reading Hasar public key file");
                hasarPublicKey = reader.ReadFile(HASAR_PUBLIC_KEY_FILE);
                Console.WriteLine(hasarPublicKey);
            }

            return hasarPublicKey;
        }

        public KeyPair GetHasarKeys()
        {
            if (hasarKeys == null)
            {
                Console.WriteLine("Reading Hasar key pair file");
                hasarKeys = reader.ReadFile(HASAR_KEYS_FILE);
                Console.WriteLine(hasarKeys);
            }

            return JsonSerializer.Deserialize<KeyPair>(hasarKeys); ;
        }

        public PublicKey GetTbkPublicKey()
        {
            if (tbkPublicKey == null)
            {
                Console.WriteLine("Reading TBK public key file");
                tbkPublicKey = reader.ReadFile(TBK_PUBLIC_KEY_FILE);
                Console.WriteLine(tbkPublicKey);
            }

            return JsonSerializer.Deserialize<PublicKey>(tbkPublicKey);
        }

        public KeyPair GetTbkKeys()
        {
            if (tbkKeys == null)
            {
                Console.WriteLine("Reading TBK key pair file");
                tbkKeys = reader.ReadFile(TBK_KEYS_FILE);
                Console.WriteLine(tbkKeys);
            }

            return JsonSerializer.Deserialize<KeyPair>(tbkKeys); ;
        }
    }
}
