using Jose;
using Moq;
using tbk_crypto.Entities;
using tbk_crypto.Infrastructure;
using tbk_crypto.Mappers;
using tbk_crypto.Services;

namespace tbk_crypto.test.Services
{
    [TestClass]
    public partial class JoseCryptographyServiceTest
    {
        private readonly Mock<IKeyRepository> _keyRepository = new();
        private readonly Mock<IKeyPairMapper> _keyPairMapper = new();
        private readonly Mock<IPublicKeyMapper> _publicKeyMapper = new();

        private readonly KeyPair _privateKey = new()
        {
            P = "yCbOKFtH_bxcY8NYn8FkfGZiXEhuHZySkLXGPIeZeXVAMwRw0_Mtq4Ec-A4jFOsIQ5qPEOj_S4TpUpGok6zQzV2W-h9q8YnmQHqxEEADRfSyeo6Q0ZQ53tGt7-Oq7N_PtCviCxFUpP6trhIga-EuFkDh5fSZtZMCIkY5n0WtlfM",
            Kty = "RSA",
            Q = "v31YRZtQHeMqojMuU_8QDMjKzFMxvstWQYilVgwoV6tQXsQGItTDqJyGeqmKVTrk-wuVNrwZYLRpOKVMdJnyF2VkU8z2PzXIvc88PErUQxEmnbbUQDRnHC7SmCB-Kl1GOsGXMEwCoKItV67DPg15_6B6HY5gZInrr2f_LPnaZys",
            D = "ihdrOQdEHVQWcF3J0N0ZRWQ7ecdMsMbPc1tdWDVZT4E5WWHazAr79MHUVPUEsXmg6XOqyzGTIZ06Vh2tIZQe1mCLMhaGvHfg0ACtW1fAf87x4eidmYzSQQxDCI6YTQGt7qiOCWN8MFmfWV1OaJBXadSwK26EGzUiga9pKXnm3JZVU4dl2yAfyLAgMnacCGl5fZ3urLSHUYwBe6O7QwuMhgow_uZioA3Hz9e1Zqfnl2D6oSra3W8jt3F3OTVcS33co8MVguSUD5Mhw0bIg0fIjO74SXgQYzUs9dSWX7c8WbQdSVH7r3SSN1pTeWQ7hx37IgO4rUXsIuX0NuWVsmbeNQ",
            E = "AQAB",
            Use = "enc",
            KeyId = "4vsc3Qk32IrLnMcmqkLLgtJNNm3GhUP3Ztz2unBPBdM",
            QI = "drVuPxQrE-X7_JVv17qLG0ZSh_oVsh6j0vjgARSy7K8fO1BnykPkjyHP0NCiumboeX3WwvKTpNiYjgjtdOPnzDWPEalxC3mVtqbSYtW3fR8LtNc75m_PcZoHTGeuxxZ3jdpdwmzgxFW3YGJibIp8P5_3P15q8dItGhvXmtfdnoI",
            DP = "Rp8Klsq-eBUCNGE04AmIvywRAKa908zvGghJTgo6aNSqNlYWyXJAZwdDhXLXhgX4AIKEgF711VusR-oFJTYQ5qVjJBX50gfqKk4gM6-ieAiDASNUjd5THP-287HNGY9O5i-lrmoLB7yk44qZzfbCIC_8hPrtUVfxpM5KfpxDR2k",
            Alg = "RSA-OAEP-256",
            DQ = "lPWHxlYEY8uELbsMrfC7ZoaII5wQFXCN_9qxaxd4BoLQuXdZopzKO47PmfnrD1QKLjQbdgGq6CxPse38ZLQgvAx2mC3X3OIU73qXS_uV1ToxRPylbfCVyTmvPwH_wyrd5_USjFKEtLqT9SKQ5OOM5MrMU9OerncPrVqECVOvjxM",
            N = "lbbzxoiQ2wikgVqiKx2sz2sixf8wWd0yrBQ2V2VYW58ogGsClk6KthYI8pXH9wPGZoOmQ7gD6ODpLMjwGl0g_QMAM6PeDFW4qDgyBgs0f8Z6sNrLLYgxFTK_gCUAGIDkJeNdtQFKNXHQTSn9IvirTjMBi9iUMpIjqXZQ6szL3YV6hUr8y1NKB3qPLKhkxDN6T7pr83EXChkAsQhA5odkwswEFd5zujOwqCdsdbGMjNBKTM6V1Q2h_7krJan7c8izR1LN6rp3vYQdmYRjqU4sUWtLqP19ZlmjiGaHiOVLGHsDfpg3Cc-0bpFJXi3k44mmVurgA5h-_DBTWcI4l2r00Q"
        };

        private readonly Jwk _jwkPrivateKey = new()
        {
            P = "yCbOKFtH_bxcY8NYn8FkfGZiXEhuHZySkLXGPIeZeXVAMwRw0_Mtq4Ec-A4jFOsIQ5qPEOj_S4TpUpGok6zQzV2W-h9q8YnmQHqxEEADRfSyeo6Q0ZQ53tGt7-Oq7N_PtCviCxFUpP6trhIga-EuFkDh5fSZtZMCIkY5n0WtlfM",
            Kty = "RSA",
            Q = "v31YRZtQHeMqojMuU_8QDMjKzFMxvstWQYilVgwoV6tQXsQGItTDqJyGeqmKVTrk-wuVNrwZYLRpOKVMdJnyF2VkU8z2PzXIvc88PErUQxEmnbbUQDRnHC7SmCB-Kl1GOsGXMEwCoKItV67DPg15_6B6HY5gZInrr2f_LPnaZys",
            D = "ihdrOQdEHVQWcF3J0N0ZRWQ7ecdMsMbPc1tdWDVZT4E5WWHazAr79MHUVPUEsXmg6XOqyzGTIZ06Vh2tIZQe1mCLMhaGvHfg0ACtW1fAf87x4eidmYzSQQxDCI6YTQGt7qiOCWN8MFmfWV1OaJBXadSwK26EGzUiga9pKXnm3JZVU4dl2yAfyLAgMnacCGl5fZ3urLSHUYwBe6O7QwuMhgow_uZioA3Hz9e1Zqfnl2D6oSra3W8jt3F3OTVcS33co8MVguSUD5Mhw0bIg0fIjO74SXgQYzUs9dSWX7c8WbQdSVH7r3SSN1pTeWQ7hx37IgO4rUXsIuX0NuWVsmbeNQ",
            E = "AQAB",
            Use = "enc",
            KeyId = "4vsc3Qk32IrLnMcmqkLLgtJNNm3GhUP3Ztz2unBPBdM",
            QI = "drVuPxQrE-X7_JVv17qLG0ZSh_oVsh6j0vjgARSy7K8fO1BnykPkjyHP0NCiumboeX3WwvKTpNiYjgjtdOPnzDWPEalxC3mVtqbSYtW3fR8LtNc75m_PcZoHTGeuxxZ3jdpdwmzgxFW3YGJibIp8P5_3P15q8dItGhvXmtfdnoI",
            DP = "Rp8Klsq-eBUCNGE04AmIvywRAKa908zvGghJTgo6aNSqNlYWyXJAZwdDhXLXhgX4AIKEgF711VusR-oFJTYQ5qVjJBX50gfqKk4gM6-ieAiDASNUjd5THP-287HNGY9O5i-lrmoLB7yk44qZzfbCIC_8hPrtUVfxpM5KfpxDR2k",
            Alg = "RSA-OAEP-256",
            DQ = "lPWHxlYEY8uELbsMrfC7ZoaII5wQFXCN_9qxaxd4BoLQuXdZopzKO47PmfnrD1QKLjQbdgGq6CxPse38ZLQgvAx2mC3X3OIU73qXS_uV1ToxRPylbfCVyTmvPwH_wyrd5_USjFKEtLqT9SKQ5OOM5MrMU9OerncPrVqECVOvjxM",
            N = "lbbzxoiQ2wikgVqiKx2sz2sixf8wWd0yrBQ2V2VYW58ogGsClk6KthYI8pXH9wPGZoOmQ7gD6ODpLMjwGl0g_QMAM6PeDFW4qDgyBgs0f8Z6sNrLLYgxFTK_gCUAGIDkJeNdtQFKNXHQTSn9IvirTjMBi9iUMpIjqXZQ6szL3YV6hUr8y1NKB3qPLKhkxDN6T7pr83EXChkAsQhA5odkwswEFd5zujOwqCdsdbGMjNBKTM6V1Q2h_7krJan7c8izR1LN6rp3vYQdmYRjqU4sUWtLqP19ZlmjiGaHiOVLGHsDfpg3Cc-0bpFJXi3k44mmVurgA5h-_DBTWcI4l2r00Q"
        };

        private readonly PublicKey _publicKey = new()
        {
            E = "AQAB",
            N = "lbbzxoiQ2wikgVqiKx2sz2sixf8wWd0yrBQ2V2VYW58ogGsClk6KthYI8pXH9wPGZoOmQ7gD6ODpLMjwGl0g_QMAM6PeDFW4qDgyBgs0f8Z6sNrLLYgxFTK_gCUAGIDkJeNdtQFKNXHQTSn9IvirTjMBi9iUMpIjqXZQ6szL3YV6hUr8y1NKB3qPLKhkxDN6T7pr83EXChkAsQhA5odkwswEFd5zujOwqCdsdbGMjNBKTM6V1Q2h_7krJan7c8izR1LN6rp3vYQdmYRjqU4sUWtLqP19ZlmjiGaHiOVLGHsDfpg3Cc-0bpFJXi3k44mmVurgA5h-_DBTWcI4l2r00Q",
            Kty = "RSA",
            Use = "enc",
            Kid = "4vsc3Qk32IrLnMcmqkLLgtJNNm3GhUP3Ztz2unBPBdM",
            Alg = "RSA-OAEP-256"
        };

        private readonly Jwk _jwkPublicKey = new()
        {
            E = "AQAB",
            N = "lbbzxoiQ2wikgVqiKx2sz2sixf8wWd0yrBQ2V2VYW58ogGsClk6KthYI8pXH9wPGZoOmQ7gD6ODpLMjwGl0g_QMAM6PeDFW4qDgyBgs0f8Z6sNrLLYgxFTK_gCUAGIDkJeNdtQFKNXHQTSn9IvirTjMBi9iUMpIjqXZQ6szL3YV6hUr8y1NKB3qPLKhkxDN6T7pr83EXChkAsQhA5odkwswEFd5zujOwqCdsdbGMjNBKTM6V1Q2h_7krJan7c8izR1LN6rp3vYQdmYRjqU4sUWtLqP19ZlmjiGaHiOVLGHsDfpg3Cc-0bpFJXi3k44mmVurgA5h-_DBTWcI4l2r00Q",
            Kty = "RSA",
            Use = "enc",
            KeyId = "4vsc3Qk32IrLnMcmqkLLgtJNNm3GhUP3Ztz2unBPBdM",
            Alg = "RSA-OAEP-256"
        };

        private readonly string _jsonPublicKey = "{\r\n  \"kty\": \"RSA\",\r\n  \"e\": \"AQAB\",\r\n  \"use\": \"enc\",\r\n  \"kid\": \"4vsc3Qk32IrLnMcmqkLLgtJNNm3GhUP3Ztz2unBPBdM\",\r\n  \"alg\": \"RSA-OAEP-256\",\r\n  \"n\": \"lbbzxoiQ2wikgVqiKx2sz2sixf8wWd0yrBQ2V2VYW58ogGsClk6KthYI8pXH9wPGZoOmQ7gD6ODpLMjwGl0g_QMAM6PeDFW4qDgyBgs0f8Z6sNrLLYgxFTK_gCUAGIDkJeNdtQFKNXHQTSn9IvirTjMBi9iUMpIjqXZQ6szL3YV6hUr8y1NKB3qPLKhkxDN6T7pr83EXChkAsQhA5odkwswEFd5zujOwqCdsdbGMjNBKTM6V1Q2h_7krJan7c8izR1LN6rp3vYQdmYRjqU4sUWtLqP19ZlmjiGaHiOVLGHsDfpg3Cc-0bpFJXi3k44mmVurgA5h-_DBTWcI4l2r00Q\"\r\n}";

        private JoseCryptographyService CreateSut()
        {
            return new JoseCryptographyService(
                _keyRepository.Object,
                _keyPairMapper.Object,
                _publicKeyMapper.Object);
        }

        [TestMethod]
        public void HasarEncrypt()
        {
            var plainText = "unit-test";

            _keyRepository.Setup(x => x.GetHasarPublicKey()).Returns(_publicKey);
            _keyRepository.Setup(x => x.GetJsonHasarPublicKey()).Returns(_jsonPublicKey);
            _publicKeyMapper.Setup(x => x.map(_publicKey)).Returns(_jwkPublicKey);

            var sut = CreateSut();
            var actual = sut.HasarEncrypt(plainText);

            Assert.IsNotNull(actual);

            // Validate compact format
            Assert.IsTrue(actual.Contains('.'));
            Assert.AreEqual(5, actual.Split('.').Length);

            _keyRepository.Verify(x => x.GetHasarPublicKey(), Times.Once);
            _keyRepository.Verify(x => x.GetHasarKeys(), Times.Never);
            _keyRepository.Verify(x => x.GetTbkKeys(), Times.Never);
            _keyRepository.Verify(x => x.GetTbkPublicKey(), Times.Never);
            _keyRepository.Verify(x => x.GetJsonHasarPublicKey(), Times.Once);
            _publicKeyMapper.Verify(x => x.map(_publicKey), Times.Once);
        }

        [TestMethod]
        public void HasarDecrypt()
        {
            var data = "unit-test";
            var encryptedData = "eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZHQ00ifQ.QL___QIaD4Fr8Dr4B-Jup9O1TJt4mrQpDheGCYFoKGnL_HM4s5isQvAPSX4uweyUXhULE0s7-3dGHi5Z7c5MqakkyZ3DO3ny6ZUNyrSTcH0ig3TMG5SBonPne2KJ-BscujG59YZVgumB33WLgf0Mg0uGm1CeRGRxUiXWXfQ3KCOekHSsdZBHZTBm8YtClpBy_MQneByY7cZGM6ZjK7_5dTppv6lrUIf5KnBfQZKJiXqNs7yzaQm_LN4z-N9pOCuiCIOVfBa5fXqtlI4IKqERfss4kYfXHysC23w9WMCHCxWLnl9K3Mc9fse13g-qeApjhQpfh6Lvy9FPoKwKYO__Bg.YtRJtpT7DCKlQROS.UFCxaG38mSgU.az-Wn7fmb24LtbcNeSHTZw";

            _keyRepository.Setup(x => x.GetHasarKeys()).Returns(_privateKey);
            _keyPairMapper.Setup(x => x.map(_privateKey)).Returns(_jwkPrivateKey);

            var sut = CreateSut();
            var actual = sut.HasarDecrypt(encryptedData);

            Assert.IsNotNull(actual);
            Assert.AreEqual(data, actual);

            _keyRepository.Verify(x => x.GetHasarKeys(), Times.Once);
            _keyRepository.Verify(x => x.GetHasarPublicKey(), Times.Never);
            _keyRepository.Verify(x => x.GetTbkKeys(), Times.Never);
            _keyRepository.Verify(x => x.GetTbkPublicKey(), Times.Never);
            _keyPairMapper.Verify(x => x.map(_privateKey), Times.Once);
        }

        [TestMethod]
        public void TbkEncrypt()
        {
            var plainText = "unit-test";

            _keyRepository.Setup(x => x.GetTbkPublicKey()).Returns(_publicKey);
            _keyRepository.Setup(x => x.GetJsonHasarPublicKey()).Returns(_jsonPublicKey);
            _publicKeyMapper.Setup(x => x.map(_publicKey)).Returns(_jwkPublicKey);

            var sut = CreateSut();
            var actual = sut.TbkEncrypt(plainText);

            Assert.IsNotNull(actual);

            // Validate compact format
            Assert.IsTrue(actual.Contains('.'));
            Assert.AreEqual(5, actual.Split('.').Length);

            _keyRepository.Verify(x => x.GetTbkPublicKey(), Times.Once);
            _keyRepository.Verify(x => x.GetTbkKeys(), Times.Never);
            _keyRepository.Verify(x => x.GetHasarPublicKey(), Times.Never);
            _keyRepository.Verify(x => x.GetHasarKeys(), Times.Never);
            _keyRepository.Verify(x => x.GetJsonHasarPublicKey(), Times.Once);
            _publicKeyMapper.Verify(x => x.map(_publicKey), Times.Once);
        }

        [TestMethod]
        public void TbkDecrypt()
        {
            var data = "unit-test";
            var encryptedData = "eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZHQ00ifQ.QL___QIaD4Fr8Dr4B-Jup9O1TJt4mrQpDheGCYFoKGnL_HM4s5isQvAPSX4uweyUXhULE0s7-3dGHi5Z7c5MqakkyZ3DO3ny6ZUNyrSTcH0ig3TMG5SBonPne2KJ-BscujG59YZVgumB33WLgf0Mg0uGm1CeRGRxUiXWXfQ3KCOekHSsdZBHZTBm8YtClpBy_MQneByY7cZGM6ZjK7_5dTppv6lrUIf5KnBfQZKJiXqNs7yzaQm_LN4z-N9pOCuiCIOVfBa5fXqtlI4IKqERfss4kYfXHysC23w9WMCHCxWLnl9K3Mc9fse13g-qeApjhQpfh6Lvy9FPoKwKYO__Bg.YtRJtpT7DCKlQROS.UFCxaG38mSgU.az-Wn7fmb24LtbcNeSHTZw";

            _keyRepository.Setup(x => x.GetTbkKeys()).Returns(_privateKey);
            _keyPairMapper.Setup(x => x.map(_privateKey)).Returns(_jwkPrivateKey);

            var sut = CreateSut();
            var actual = sut.TbkDecrypt(encryptedData);

            Assert.IsNotNull(actual);
            Assert.AreEqual(data, actual);

            _keyRepository.Verify(x => x.GetTbkKeys(), Times.Once);
            _keyRepository.Verify(x => x.GetHasarPublicKey(), Times.Never);
            _keyRepository.Verify(x => x.GetHasarKeys(), Times.Never);
            _keyRepository.Verify(x => x.GetTbkPublicKey(), Times.Never);
            _keyPairMapper.Verify(x => x.map(_privateKey), Times.Once);
        }

        [TestMethod]
        public void GetHasarKeys()
        {
            _keyRepository.Setup(x => x.GetHasarKeys()).Returns(_privateKey);
            _keyPairMapper.Setup(x => x.map(_privateKey)).Returns(_jwkPrivateKey);

            var expected = _jwkPrivateKey;

            var sut = CreateSut();
            var actual = sut.GetHasarKeys();

            Assert.AreEqual(expected, actual);

            _keyRepository.Verify(x => x.GetHasarKeys(), Times.Once);
            _keyPairMapper.Verify(x => x.map(_privateKey), Times.Once);
        }

        [TestMethod]
        public void GetTbkPublicKey()
        {
            _keyRepository.Setup(x => x.GetTbkPublicKey()).Returns(_publicKey);
            _publicKeyMapper.Setup(x => x.map(_publicKey)).Returns(_jwkPublicKey);

            var expected = _jwkPublicKey;

            var sut = CreateSut();
            var actual = sut.GetTbkPublicKey();

            Assert.AreEqual(expected, actual);

            _keyRepository.Verify(x => x.GetTbkPublicKey(), Times.Once);
            _publicKeyMapper.Verify(x => x.map(_publicKey), Times.Once);
        }

        [TestMethod]
        public void HasarEncryptDecrypt()
        {
            var data = "unit-test";

            _keyRepository.Setup(x => x.GetHasarPublicKey()).Returns(_publicKey);
            _keyRepository.Setup(x => x.GetJsonHasarPublicKey()).Returns(_jsonPublicKey);
            _keyRepository.Setup(x => x.GetHasarKeys()).Returns(_privateKey);

            _publicKeyMapper.Setup(x => x.map(_publicKey)).Returns(_jwkPublicKey);
            _keyPairMapper.Setup(x => x.map(_privateKey)).Returns(_jwkPrivateKey);

            var sut = CreateSut();
            var encryptedData = sut.HasarEncrypt(data);
            var actual = sut.HasarDecrypt(encryptedData);

            Assert.IsNotNull(actual);
            Assert.AreEqual(data, actual);

            _keyRepository.Verify(x => x.GetHasarPublicKey(), Times.Once);
            _keyRepository.Verify(x => x.GetJsonHasarPublicKey(), Times.Once);
            _keyRepository.Verify(x => x.GetHasarKeys(), Times.Once);

            _publicKeyMapper.Verify(x => x.map(_publicKey), Times.Once);
            _keyPairMapper.Verify(x => x.map(_privateKey), Times.Once);
        }

        [TestMethod]
        public void TbkEncryptDecrypt()
        {
            var data = "unit-test";

            _keyRepository.Setup(x => x.GetTbkPublicKey()).Returns(_publicKey);
            _keyRepository.Setup(x => x.GetJsonHasarPublicKey()).Returns(_jsonPublicKey);
            _keyRepository.Setup(x => x.GetTbkKeys()).Returns(_privateKey);

            _publicKeyMapper.Setup(x => x.map(_publicKey)).Returns(_jwkPublicKey);
            _keyPairMapper.Setup(x => x.map(_privateKey)).Returns(_jwkPrivateKey);

            var sut = CreateSut();
            var encryptedData = sut.TbkEncrypt(data);
            var actual = sut.TbkDecrypt(encryptedData);

            Assert.IsNotNull(actual);
            Assert.AreEqual(data, actual);

            _keyRepository.Verify(x => x.GetTbkPublicKey(), Times.Once);
            _keyRepository.Verify(x => x.GetJsonHasarPublicKey(), Times.Once);
            _keyRepository.Verify(x => x.GetTbkKeys(), Times.Once);

            _publicKeyMapper.Verify(x => x.map(_publicKey), Times.Once);
            _keyPairMapper.Verify(x => x.map(_privateKey), Times.Once);
        }
    }
}
