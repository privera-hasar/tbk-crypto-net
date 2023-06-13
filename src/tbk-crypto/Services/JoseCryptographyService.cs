using Jose;
using tbk_crypto.Infrastructure;
using tbk_crypto.Mappers;

namespace tbk_crypto.Services
{
    /// <summary>
    /// https://github.com/dvsekhvalnov/jose-jwt#rs--and-ps--family
    /// </summary>
    public class JoseCryptographyService : IJoseCryptographyService
    {
        private readonly IKeyRepository _keyRepository;
        private readonly IKeyPairMapper _keyPairMapper;
        private readonly IPublicKeyMapper _publicKeyMapper;

        public JoseCryptographyService(
            IKeyRepository keyRepository,
            IKeyPairMapper keyPairMapper,
            IPublicKeyMapper publicKeyMapper)
        {
            _keyRepository = keyRepository;
            _keyPairMapper = keyPairMapper;
            _publicKeyMapper = publicKeyMapper;
        }

        public string HasarEncrypt(string plainText)
        {
            var tbkPublicKey = GetHasarPublicKey();
            var hasarPublicKey = _keyRepository.GetJsonHasarPublicKey();

            var recipients = new[] { new JweRecipient(JweAlgorithm.RSA_OAEP_256, tbkPublicKey) };

            var extraProtectedHeaders = new Dictionary<string, object> {
                { "app-key", hasarPublicKey }
            };

            return JWE.Encrypt(plainText, recipients, JweEncryption.A256GCM, null, SerializationMode.Compact, JweCompression.DEF, extraProtectedHeaders);
        }

        public string HasarDecrypt(string token)
        {
            var privateKey = GetHasarKeys();

            var jwe = JWE.Decrypt(token, privateKey);

            return jwe.Plaintext;
        }

        public string TbkEncrypt(string plainText)
        {
            var tbkPublicKey = GetTbkPublicKey();
            var hasarPublicKey = _keyRepository.GetJsonHasarPublicKey();

            var recipients = new[] { new JweRecipient(JweAlgorithm.RSA_OAEP_256, tbkPublicKey) };

            var extraProtectedHeaders = new Dictionary<string, object> {
                { "app-key", hasarPublicKey }
            };

            return JWE.Encrypt(plainText, recipients, JweEncryption.A256GCM, null, SerializationMode.Compact, JweCompression.DEF, extraProtectedHeaders);
        }

        public string TbkDecrypt(string token)
        {
            var privateKey = GetTbkKeys();

            var jwe = JWE.Decrypt(token, privateKey);

            return jwe.Plaintext;
        }

        public Jwk GetHasarKeys()
        {
            var keys = _keyRepository.GetHasarKeys();

            return _keyPairMapper.map(keys);
        }

        public Jwk GetHasarPublicKey()
        {
            var key = _keyRepository.GetHasarPublicKey();

            return _publicKeyMapper.map(key);
        }

        public Jwk GetTbkKeys()
        {
            var keys = _keyRepository.GetTbkKeys();

            return _keyPairMapper.map(keys);
        }

        public Jwk GetTbkPublicKey()
        {
            var key = _keyRepository.GetTbkPublicKey();

            return _publicKeyMapper.map(key);
        }

    }
}
