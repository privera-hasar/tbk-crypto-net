using tbk_crypto.Commands;
using tbk_crypto.Infrastructure;
using tbk_crypto.Mappers;
using tbk_crypto.Services;

namespace tbk_crypto
{
    public class Factory
    {
        private static Factory? instance;

        private IKeyRepository? _repository;
        private IFileReader? _reader;
        private IJoseCryptographyService? _service;
        private IKeyPairMapper? _keyPairMapper;
        private IPublicKeyMapper? _publicKeyMapper;
        private FullTestCommand? _fullTestCommand;
        private TbkEncryptCommand? _encryptCommand;
        private HasarDecryptCommand? _decryptCommand;

        private Factory() { }

        public static Factory GetInstance()
        {
            if (instance == null)
            {
                instance = new Factory();
            }

            return instance;
        }

        public IKeyRepository GetKeyRepository()
        {
            if (_repository == null)
            {
                _repository = new KeyRepository(
                    GetFileReader());
            }

            return _repository;
        }

        public IFileReader GetFileReader()
        {
            if (_reader == null)
            {
                _reader = new FileReader();
            }

            return _reader;
        }

        public IJoseCryptographyService GetJoseCryptographyService()
        {
            if (_service == null)
            {
                _service = new JoseCryptographyService(
                    GetKeyRepository(),
                    GetKeyPairMapper(),
                    GetPublicKeyMapper());
            }

            return _service;
        }

        public IKeyPairMapper GetKeyPairMapper()
        {
            if (_keyPairMapper == null)
            {
                _keyPairMapper = new KeyPairMapper();
            }

            return _keyPairMapper;
        }

        public IPublicKeyMapper GetPublicKeyMapper()
        {
            if (_publicKeyMapper == null)
            {
                _publicKeyMapper = new PublicKeyMapper();
            }

            return _publicKeyMapper;
        }

        public TbkEncryptCommand GetEncryptCommand()
        {
            if (_encryptCommand == null)
            {
                _encryptCommand = new TbkEncryptCommand(
                    GetJoseCryptographyService());
            }

            return _encryptCommand;
        }

        public HasarDecryptCommand GetDecryptCommand()
        {
            if (_decryptCommand == null)
            {
                _decryptCommand = new HasarDecryptCommand(
                    GetJoseCryptographyService());
            }

            return _decryptCommand;
        }

        public FullTestCommand GetFullTestCommand()
        {
            if (_fullTestCommand == null)
            {
                _fullTestCommand = new FullTestCommand(
                    GetJoseCryptographyService());
            }

            return _fullTestCommand;
        }
    }
}
