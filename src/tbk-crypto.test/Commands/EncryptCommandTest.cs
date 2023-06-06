using Moq;
using tbk_crypto.Commands;
using tbk_crypto.Services;

namespace tbk_crypto.test.Commands
{
    [TestClass]
    public class EncryptCommandTest
    {
        private readonly Mock<IJoseCryptographyService> _cryptoService = new();

        private EncryptCommand CreateSut()
        {
            return new EncryptCommand(_cryptoService.Object);
        }

        [TestMethod]
        public void Run()
        {
            string data = "unit-test";
            string encryptedData = "tset-tinu";

            _cryptoService.Setup(x => x.PublicEncrypt(data)).Returns(encryptedData);

            var sut = CreateSut();
            sut.Run(data);

            _cryptoService.Verify(x => x.PublicEncrypt(data), Times.Once());
        }
    }
}
