using tbk_crypto.Entities;
using tbk_crypto.Mappers;

namespace tbk_crypto.test.Mappers
{
    [TestClass]
    public class KeyPairMapperTest
    {
        public KeyPairMapper CreateSut()
        {
            return new KeyPairMapper();
        }

        [TestMethod]
        public void map()
        {
            KeyPair privateKey = new()
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

            var sut = CreateSut();
            var actual = sut.map(privateKey);

            Assert.IsNotNull(actual);
            Assert.AreEqual(privateKey.Alg, actual.Alg);
            Assert.AreEqual(privateKey.D, actual.D);
            Assert.AreEqual(privateKey.DP, actual.DP);
            Assert.AreEqual(privateKey.DQ, actual.DQ);
            Assert.AreEqual(privateKey.E, actual.E);
            Assert.AreEqual(privateKey.KeyId, actual.KeyId);
            Assert.AreEqual(privateKey.Kty, actual.Kty);
            Assert.AreEqual(privateKey.N, actual.N);
            Assert.AreEqual(privateKey.P, actual.P);
            Assert.AreEqual(privateKey.Q, actual.Q);
            Assert.AreEqual(privateKey.QI, actual.QI);
            Assert.AreEqual(privateKey.Use, actual.Use);
        }
    }
}
