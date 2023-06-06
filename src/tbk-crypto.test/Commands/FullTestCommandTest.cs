using Moq;
using tbk_crypto.Commands;
using tbk_crypto.Services;

namespace tbk_crypto.test.Commands
{
    [TestClass]
    public class FullTestCommandTest
    {
        private readonly Mock<IJoseCryptographyService> _cryptoService = new();

        private FullTestCommand CreateSut()
        {
            return new FullTestCommand(_cryptoService.Object);
        }

        [TestMethod]
        public void Run()
        {
            string data = "unit-test";
            string privateEncryptedData = "private";
            string publicEncryptedData = "public";

            _cryptoService.Setup(x => x.PrivateEncrypt(data)).Returns(privateEncryptedData);
            _cryptoService.Setup(x => x.PublicEncrypt(data)).Returns(publicEncryptedData);

            _cryptoService.Setup(x => x.PrivateDecrypt(privateEncryptedData)).Returns(data);
            _cryptoService.Setup(x => x.PublicDecrypt(privateEncryptedData)).Returns(data);

            _cryptoService.Setup(x => x.PrivateDecrypt(publicEncryptedData)).Returns(data);
            _cryptoService.Setup(x => x.PublicDecrypt(publicEncryptedData)).Returns(data);

            var sut = CreateSut();
            sut.Run(data);

            _cryptoService.Verify(x => x.PublicEncrypt(data), Times.Once());
            _cryptoService.Verify(x => x.PrivateEncrypt(data), Times.Once());
            _cryptoService.Verify(x => x.PublicDecrypt(privateEncryptedData), Times.Once());
            _cryptoService.Verify(x => x.PublicDecrypt(privateEncryptedData), Times.Once());
            _cryptoService.Verify(x => x.PrivateDecrypt(publicEncryptedData), Times.Once());
            _cryptoService.Verify(x => x.PrivateDecrypt(publicEncryptedData), Times.Once());
        }
    }
}
