using tbk_crypto.Commands;
using tbk_crypto.Infrastructure;
using tbk_crypto.Services;

namespace tbk_crypto
{
    internal class Factory
    {
        private static Factory? instance;

        private KeyRepository? _repository;
        private JoseCryptographyService? _service;
        private FullTestCommand? _fullTestCommand;
        private EncryptCommand? _encryptCommand;
        private DecryptCommand? _decryptCommand;

        private Factory() { }

        public static Factory GetInstance()
        {
            if (instance == null)
            {
                instance = new Factory();
            }

            return instance;
        }

        public KeyRepository GetKeyRepository()
        {
            if (_repository == null) {
                _repository = new KeyRepository();
            }

            return _repository;
        }

        public JoseCryptographyService GetJoseCryptographyService()
        {
            if (_service == null)
            {
                _service = new JoseCryptographyService(GetKeyRepository());
            }

            return _service;
        }

        public EncryptCommand GetEncryptCommand()
        {
            if (_encryptCommand == null)
            {
                _encryptCommand = new EncryptCommand(GetJoseCryptographyService());
            }

            return _encryptCommand;
        }

        public DecryptCommand GetDecryptCommand()
        {
            if (_decryptCommand == null)
            {
                _decryptCommand = new DecryptCommand(GetJoseCryptographyService());
            }

            return _decryptCommand;
        }

        public FullTestCommand GetFullTestCommand()
        {
            if (_fullTestCommand == null)
            {
                _fullTestCommand = new FullTestCommand(GetJoseCryptographyService());
            }

            return _fullTestCommand;
        }
    }
}
