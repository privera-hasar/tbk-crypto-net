using tbk_crypto.Entities;
using tbk_crypto.Mappers;

namespace tbk_crypto.test.Mappers
{
    [TestClass]
    public class publicKeyMapperTest
    {
        public PublicKeyMapper CreateSut()
        {
            return new PublicKeyMapper();
        }

        [TestMethod]
        public void map()
        {
            PublicKey publicKey = new()
            {
                E = "AQAB",
                N = "lbbzxoiQ2wikgVqiKx2sz2sixf8wWd0yrBQ2V2VYW58ogGsClk6KthYI8pXH9wPGZoOmQ7gD6ODpLMjwGl0g_QMAM6PeDFW4qDgyBgs0f8Z6sNrLLYgxFTK_gCUAGIDkJeNdtQFKNXHQTSn9IvirTjMBi9iUMpIjqXZQ6szL3YV6hUr8y1NKB3qPLKhkxDN6T7pr83EXChkAsQhA5odkwswEFd5zujOwqCdsdbGMjNBKTM6V1Q2h_7krJan7c8izR1LN6rp3vYQdmYRjqU4sUWtLqP19ZlmjiGaHiOVLGHsDfpg3Cc-0bpFJXi3k44mmVurgA5h-_DBTWcI4l2r00Q",
                Kty = "RSA",
                Use = "enc",
                Kid = "4vsc3Qk32IrLnMcmqkLLgtJNNm3GhUP3Ztz2unBPBdM",
                Alg = "RSA-OAEP-256"
            };

            var sut = CreateSut();
            var actual = sut.map(publicKey);

            Assert.IsNotNull(actual);
            Assert.AreEqual(publicKey.Alg, actual.Alg);
            Assert.AreEqual(publicKey.E, actual.E);
            Assert.AreEqual(publicKey.Kid, actual.KeyId);
            Assert.AreEqual(publicKey.Kty, actual.Kty);
            Assert.AreEqual(publicKey.N, actual.N);
            Assert.AreEqual(publicKey.Use, actual.Use);
        }
    }
}
