using Moq;
using tbk_crypto.Commands;
using tbk_crypto.Services;

namespace tbk_crypto.test.Commands
{
    [TestClass]
    public class DecryptCommandTest
    {
        private readonly Mock<IJoseCryptographyService> _cryptoService = new();

        private HasarDecryptCommand CreateSut()
        {
            return new HasarDecryptCommand(_cryptoService.Object);
        }

        [TestMethod]
        public void Run()
        {
            string data = "unit-test";
            string encryptedData = "tset-tinu";

            _cryptoService.Setup(x => x.HasarDecrypt(encryptedData)).Returns(data);

            var sut = CreateSut();
            sut.Run(encryptedData);

            _cryptoService.Verify(x => x.HasarDecrypt(encryptedData), Times.Once());
        }
    }
}
