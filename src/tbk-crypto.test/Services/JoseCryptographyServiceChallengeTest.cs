using Jose;
using Moq;
using System.Data;
using System.Xml.Serialization;
using tbk_crypto.Entities;
using tbk_crypto.Infrastructure;
using tbk_crypto.Services;

namespace tbk_crypto.test.Services
{
    [TestClass]
    public partial class JoseCryptographyServiceChallengeTest
    {
        private readonly Mock<IKeyRepository> _keyRepository = new();

        private readonly string _challengeJsonPrivateKey = "{\"p\": \"6F3hTSXzLE2HubDT3upfePo31p_A4QWXH4It2JzmQiIMWzA4TMJs64Jj2kppb_7WHJ2WOC28E9zZqiTKs5gpxCVSIEjS0HB8cGuTWsQVp6CvVRBDtUipAS9qWVf2rMJUUCSfwjgOD8XldgLMGbassB83oSJdIkg7lcWH2N-Ks1c\",\"kty\": \"RSA\",\"q\": \"2l098hM9t6EXAjrRhfkno02DhU9VOGzr7IycNYKatD6aDrp2Acj_q6SatV6CbwgKkQv02ajsPdw-90pIjvbzfYQVvs4-jP2pMk36JF1OTxf0XZOVDLwVPA1UNZmneSbadFMrilDxZ1ziVmsJFq6D0w9gH_YsGU2uwXUxvrW4kq8\",\"d\": \"fJu9tr7qxTQ147T_iwzqQ9VTnlc8SiMFiU9I77ONEsZ78YLG33s5firVmpL2uO9NtehpHjsbZjwr6fD2jt1TtroF5uTgVcNd7z6ReJ_tYB5SZthR8LC-YbgVvImETu0NSYS_9wuardRRq_0ma7AJILjhl8VJx5ZV6-SEERBQlSumX0HyoHxNRwvtNFVAaVnB0zhey8EoGqRgx0PMDEiulzQQkeF-I9wtiqEDNY_Lb7RWcfgkC5HoYCnT50zdZO0AQtHCadMSgxAPYnT2vpm7wnXBhX2WtDytikano3d9sipdJ0JMR9AybwSgsl0KLG0F9c5yytXK0Hg6eR9NPHrNqQ\",\"e\": \"AQAB\",\"use\": \"enc\",\"qi\": \"Eb3G0mhYo2_nMsE8M6-kB8lTA_Fc2qioe5Wg022dri5gSkYrS3SFk0-reet5jx8vmgUDMZYbdto4cGB_jPZzuRVEulSpo2GpKj4jlLiiZ9_91oeQxDOB4kT8VMjR-RWLOeMzcHtXqOVp_YjesBqbuhsPpq2XqVCIjskGOJz7GZM\",\"dp\": \"tJ50yMEc-mzxeVeH-Rh5SdJFoSDgjokH5UgNVvjuyq0oqaEbwh7HB3F9KGr69kFCMVAVOmnibhYXY3fh0CQRIxRXeBzjMX6eW8xi6ATPSFNQtnBEER__rpoqsnP7nGIZo4o0XIWx-HS9XjPiboJeTTCWzzuk5Ub8efjnFZ79c1U\",\"alg\": \"RSA-OAEP-256\",\"dq\": \"lO_b_GybP9Aj_WVY22QhYQTRm--MVLwV_nW5ZdX65s18DmenbXhGyi-vRd18YXa-nkflbUAGQfsjB1AnRTuyv_2DR20sxMWOzbzZzEvHybKw_dJXayQt55O_x_9RdxTFE2pW3DRxMurrhb3jqRDG4QIvsh0Z3vhZJaJll4oipgk\",\"n\": \"xjSUMKQ9Ns6sWy3pW-wZzV8fQDN2XyiSHSIM5HpoGXjEF2dasNfhcHM-IRVFGoS7eZUZuh1DJi8o6LSiyYI78jegEo7G0RzXGHI6xdCkbcZIIZGVECinbRu4ZuMwIYKxEeT-a6lF7utuUAtyin9A1Y0E0CnDUGNlrPsQ_zgw2IEUoLSDq_TLP7YCt2H42xdXNo8-rrqQd8V2rPVYWPgDteaFZDzzoLNRnkF3AlMOKGQHPCfbH1pK2gDIzu0dXW9trPKMHYBpNiTbBTuWihZf2fCOXLYgT3Koy5bRUKBBYke0EAEA-ES7kG1Bt57DkodzgTtqg_FQzKYGdwrNU6A2eQ\"}";

        private readonly KeyPair _challengePrivateKey = new()
        {
            P = "6F3hTSXzLE2HubDT3upfePo31p_A4QWXH4It2JzmQiIMWzA4TMJs64Jj2kppb_7WHJ2WOC28E9zZqiTKs5gpxCVSIEjS0HB8cGuTWsQVp6CvVRBDtUipAS9qWVf2rMJUUCSfwjgOD8XldgLMGbassB83oSJdIkg7lcWH2N-Ks1c",
            Kty = "RSA",
            Q = "2l098hM9t6EXAjrRhfkno02DhU9VOGzr7IycNYKatD6aDrp2Acj_q6SatV6CbwgKkQv02ajsPdw-90pIjvbzfYQVvs4-jP2pMk36JF1OTxf0XZOVDLwVPA1UNZmneSbadFMrilDxZ1ziVmsJFq6D0w9gH_YsGU2uwXUxvrW4kq8",
            D = "fJu9tr7qxTQ147T_iwzqQ9VTnlc8SiMFiU9I77ONEsZ78YLG33s5firVmpL2uO9NtehpHjsbZjwr6fD2jt1TtroF5uTgVcNd7z6ReJ_tYB5SZthR8LC-YbgVvImETu0NSYS_9wuardRRq_0ma7AJILjhl8VJx5ZV6-SEERBQlSumX0HyoHxNRwvtNFVAaVnB0zhey8EoGqRgx0PMDEiulzQQkeF-I9wtiqEDNY_Lb7RWcfgkC5HoYCnT50zdZO0AQtHCadMSgxAPYnT2vpm7wnXBhX2WtDytikano3d9sipdJ0JMR9AybwSgsl0KLG0F9c5yytXK0Hg6eR9NPHrNqQ",
            E = "AQAB",
            Use = "enc",
            KeyId = "",
            QI = "Eb3G0mhYo2_nMsE8M6-kB8lTA_Fc2qioe5Wg022dri5gSkYrS3SFk0-reet5jx8vmgUDMZYbdto4cGB_jPZzuRVEulSpo2GpKj4jlLiiZ9_91oeQxDOB4kT8VMjR-RWLOeMzcHtXqOVp_YjesBqbuhsPpq2XqVCIjskGOJz7GZM",
            DP = "tJ50yMEc-mzxeVeH-Rh5SdJFoSDgjokH5UgNVvjuyq0oqaEbwh7HB3F9KGr69kFCMVAVOmnibhYXY3fh0CQRIxRXeBzjMX6eW8xi6ATPSFNQtnBEER__rpoqsnP7nGIZo4o0XIWx-HS9XjPiboJeTTCWzzuk5Ub8efjnFZ79c1U",
            Alg = "RSA-OAEP-256",
            DQ = "lO_b_GybP9Aj_WVY22QhYQTRm--MVLwV_nW5ZdX65s18DmenbXhGyi-vRd18YXa-nkflbUAGQfsjB1AnRTuyv_2DR20sxMWOzbzZzEvHybKw_dJXayQt55O_x_9RdxTFE2pW3DRxMurrhb3jqRDG4QIvsh0Z3vhZJaJll4oipgk",
            N = "xjSUMKQ9Ns6sWy3pW-wZzV8fQDN2XyiSHSIM5HpoGXjEF2dasNfhcHM-IRVFGoS7eZUZuh1DJi8o6LSiyYI78jegEo7G0RzXGHI6xdCkbcZIIZGVECinbRu4ZuMwIYKxEeT-a6lF7utuUAtyin9A1Y0E0CnDUGNlrPsQ_zgw2IEUoLSDq_TLP7YCt2H42xdXNo8-rrqQd8V2rPVYWPgDteaFZDzzoLNRnkF3AlMOKGQHPCfbH1pK2gDIzu0dXW9trPKMHYBpNiTbBTuWihZf2fCOXLYgT3Koy5bRUKBBYke0EAEA-ES7kG1Bt57DkodzgTtqg_FQzKYGdwrNU6A2eQ"
        };

        private readonly bool useJsonPrivateKey = true;

        private KeyPair GetChallengePrivateKey() {
            if (useJsonPrivateKey)
            {
                return GetChallengePrivateKeyFromJson();
            }
            else
            {
                return _challengePrivateKey;
            }
        }

        private KeyPair GetChallengePrivateKeyFromJson()
        {
            Mock<IFileReader> fileReader = new();
            fileReader.Setup(x => x.ReadFile("keys.json")).Returns(_challengeJsonPrivateKey);

            var repository = new KeyRepository(fileReader.Object);
            return repository.GetKeys();
        }

        private readonly string encryptedChallenge = "eyJhbGciOiJSU0EtT0FFUC0yNTYiLCJhcHAta2V5Ijoie1wia3R5XCI6XCJSU0FcIixcImtpZFwiOlwicWYtdlhQeVV6NjEtUnNBeHhyOExJem45eElTYlNoc3paci1ZYm85YTJra1wiLFwidXNlXCI6XCJlbmNcIixcImFsZ1wiOlwiUlNBLU9BRVAtMjU2XCIsXCJlXCI6XCJBUUFCXCIsXCJuXCI6XCJ4alNVTUtROU5zNnNXeTNwVy13WnpWOGZRRE4yWHlpU0hTSU01SHBvR1hqRUYyZGFzTmZoY0hNLUlSVkZHb1M3ZVpVWnVoMURKaThvNkxTaXlZSTc4amVnRW83RzBSelhHSEk2eGRDa2JjWklJWkdWRUNpbmJSdTRadU13SVlLeEVlVC1hNmxGN3V0dVVBdHlpbjlBMVkwRTBDbkRVR05sclBzUV96Z3cySUVVb0xTRHFfVExQN1lDdDJINDJ4ZFhObzgtcnJxUWQ4VjJyUFZZV1BnRHRlYUZaRHp6b0xOUm5rRjNBbE1PS0dRSFBDZmJIMXBLMmdESXp1MGRYVzl0clBLTUhZQnBOaVRiQlR1V2loWmYyZkNPWExZZ1QzS295NWJSVUtCQllrZTBFQUVBLUVTN2tHMUJ0NTdEa29kemdUdHFnX0ZRektZR2R3ck5VNkEyZVFcIn0iLCJlbmMiOiJBMjU2R0NNIiwia2lkIjoid1VZU08tQ0ZwZHNxaUVqSGxvaVRteEhtMHE4dk1YdzV6VlRrNXNKU0JyRSJ9.Cl-3TYocYmPTOlFp6qQtZ2RCRDW2eZcYBI3rd805vnUYBc8Py_wh8jw3idV22qRLDkK2wIDQu-SbLJ7bnndiFQPgOEfOJa6N5NeRKYQalS0o2EEtlSZNrsi71Pqr359EYS4hIIMX4KlGYjrjUmkuqcE4TtGdq0AxHqwdwFlnVoV7rhECEAArzSemr4WFG-VEi2yhj7w9s4WcpGhyZR45I5t8dHIe0lKdZj6C8opMgm76tLvRnXqEOU335_KTdXAxjC0KE7pmnH4vzL7y8bv1wy_FPNGRt8gbjGChQJhKl2Gaw9F28eLDo2wZC1uaLtarujBxRWzAZ2xusx8ANTj4cA.CZTogPtd_CX6NC7j.Fig7dKgb-xrc5HTlDF6kPg.OsAUN3-dMRs8veLwlsDvVA";

        private JoseCryptographyService CreateSut()
        {
            return new JoseCryptographyService(_keyRepository.Object);
        }

        private void ChallengeDecryptJwsTest(JwsAlgorithm alg)
        {
            var privateKey = GetChallengePrivateKey();
            _keyRepository.Setup(x => x.GetKeys()).Returns(privateKey);

            var sut = CreateSut().Configure(
                privateDecryptJwsAlgorythm: alg, 
                useJwsAlgorythm: true);
            var actual = sut.PrivateDecrypt(encryptedChallenge);

            Assert.IsNotNull(actual);

            _keyRepository.Verify(x => x.GetKeys(), Times.Once);
        }

        private void ChallengeDecryptJweTest(JweAlgorithm alg, JweEncryption enc)
        {
            var privateKey = GetChallengePrivateKey();
            _keyRepository.Setup(x => x.GetKeys()).Returns(privateKey);

            var sut = CreateSut().Configure(
                privateDecryptJweAlgorythm: alg,
                privateDecryptJweEncryption: enc,
                useJwsAlgorythm: false);
            var actual = sut.PrivateDecrypt(encryptedChallenge);

            Assert.IsNotNull(actual);

            _keyRepository.Verify(x => x.GetKeys(), Times.Once);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jws_None()
        {
            ChallengeDecryptJwsTest(JwsAlgorithm.none);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jws_ES256()
        {
            ChallengeDecryptJwsTest(JwsAlgorithm.ES256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jws_ES384()
        {
            ChallengeDecryptJwsTest(JwsAlgorithm.ES384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jws_ES512()
        {
            ChallengeDecryptJwsTest(JwsAlgorithm.ES512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jws_HS256()
        {
            ChallengeDecryptJwsTest(JwsAlgorithm.HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jws_HS384()
        {
            ChallengeDecryptJwsTest(JwsAlgorithm.HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jws_HS512()
        {
            ChallengeDecryptJwsTest(JwsAlgorithm.HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jws_PS256()
        {
            ChallengeDecryptJwsTest(JwsAlgorithm.PS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jws_PS384()
        {
            ChallengeDecryptJwsTest(JwsAlgorithm.PS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jws_PS512()
        {
            ChallengeDecryptJwsTest(JwsAlgorithm.PS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jws_RS256()
        {
            ChallengeDecryptJwsTest(JwsAlgorithm.RS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jws_RS384()
        {
            ChallengeDecryptJwsTest(JwsAlgorithm.RS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jws_RS512()
        {
            ChallengeDecryptJwsTest(JwsAlgorithm.RS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A128GCMKW_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A128GCMKW, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A128GCMKW_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A128GCMKW, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A128GCMKW_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A128GCMKW, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A128GCMKW_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A128GCMKW, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A128GCMKW_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A128GCMKW, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A128GCMKW_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A128GCMKW, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A128KW_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A128KW, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A128KW_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A128KW, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A128KW_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A128KW, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A128KW_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A128KW, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A128KW_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A128KW, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A128KW_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A128KW, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A192GCMKW_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A192GCMKW, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A192GCMKW_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A192GCMKW, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A192GCMKW_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A192GCMKW, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A192GCMKW_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A192GCMKW, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A192GCMKW_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A192GCMKW, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A192GCMKW_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A192GCMKW, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A192KW_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A192KW, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A192KW_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A192KW, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A192KW_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A192KW, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A192KW_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A192KW, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A192KW_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A192KW, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A192KW_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A192KW, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A256GCMKW_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A256GCMKW, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A256GCMKW_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A256GCMKW, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A256GCMKW_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A256GCMKW, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A256GCMKW_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A256GCMKW, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A256GCMKW_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A256GCMKW, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A256GCMKW_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A256GCMKW, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A256KW_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A256KW, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A256KW_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A256KW, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A256KW_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A256KW, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A256KW_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A256KW, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A256KW_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A256KW, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_A256KW_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.A256KW, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_DIR_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.DIR, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_DIR_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.DIR, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_DIR_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.DIR, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_DIR_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.DIR, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_DIR_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.DIR, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_DIR_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.DIR, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHES_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHES_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHES_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHES_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHES_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHES_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA128KW_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A128KW, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA128KW_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A128KW, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA128KW_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A128KW, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA128KW_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A128KW, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA128KW_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A128KW, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA128KW_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A128KW, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA192KW_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A192KW, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA192KW_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A192KW, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA192KW_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A192KW, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA192KW_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A192KW, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA192KW_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A192KW, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA192KW_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A192KW, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA256KW_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A256KW, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA256KW_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A256KW, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA256KW_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A256KW, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA256KW_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A256KW, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA256KW_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A256KW, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_ECDHESA256KW_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.ECDH_ES_A256KW, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS256A128KW_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS256A128KW_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS256A128KW_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS256A128KW_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS256A128KW_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS256A128KW_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS384A192KW_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS384_A192KW, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS384A192KW_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS384_A192KW, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS384A192KW_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS384_A192KW, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS384A192KW_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS384_A192KW, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS384A192KW_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS384_A192KW, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS384A192KW_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS384_A192KW, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS512A256KW_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS512_A256KW, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS512A256KW_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS512_A256KW, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS512A256KW_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS512_A256KW, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS512A256KW_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS512_A256KW, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS512A256KW_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS512_A256KW, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_PBES2HS512A256KW_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.PBES2_HS512_A256KW, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSA15_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA1_5, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSA15_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA1_5, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSA15_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA1_5, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSA15_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA1_5, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSA15_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA1_5, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSA15_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA1_5, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSAOAEP_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA_OAEP, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSAOAEP_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA_OAEP, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSAOAEP_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA_OAEP, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSAOAEP_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA_OAEP, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSAOAEP_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA_OAEP, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSAOAEP_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA_OAEP, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSAOAEP256_A128CBCHS256()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA_OAEP_256, JweEncryption.A128CBC_HS256);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSAOAEP256_A128GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA_OAEP_256, JweEncryption.A128GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSAOAEP256_A192CBCHS384()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA_OAEP_256, JweEncryption.A192CBC_HS384);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSAOAEP256_A192GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA_OAEP_256, JweEncryption.A192GCM);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSAOAEP256_A256CBCHS512()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA_OAEP_256, JweEncryption.A256CBC_HS512);
        }

        [TestMethod]
        public void ChallengeDecrypt_Jwe_RSAOAEP256_A256GCM()
        {
            ChallengeDecryptJweTest(JweAlgorithm.RSA_OAEP_256, JweEncryption.A256GCM);
        }

        [TestMethod]
        public void ChallengeDecryptJwe()
        {
            var privateKey = GetChallengePrivateKey();
            _keyRepository.Setup(x => x.GetKeys()).Returns(privateKey);

            var sut = CreateSut().Configure(
                privateDecryptJweAlgorythm: JweAlgorithm.RSA_OAEP_256,
                privateDecryptJweEncryption: JweEncryption.A256GCM,
                useJwsAlgorythm: false);
            var actual = sut.PrivateDecryptJwe(encryptedChallenge);

            Assert.IsNotNull(actual);

            _keyRepository.Verify(x => x.GetKeys(), Times.Once);
        }

        [TestMethod]
        public void ChallengeDecryptJweSettings()
        {
            var privateKey = GetChallengePrivateKey();
            _keyRepository.Setup(x => x.GetKeys()).Returns(privateKey);

            var sut = CreateSut().Configure(
                privateDecryptJweAlgorythm: JweAlgorithm.RSA_OAEP_256,
                privateDecryptJweEncryption: JweEncryption.A256GCM,
                useJwsAlgorythm: false);
            var actual = sut.PrivateDecryptJweSetting(encryptedChallenge);

            Assert.IsNotNull(actual);

            _keyRepository.Verify(x => x.GetKeys(), Times.Once);
        }

        [TestMethod]
        public void ChallengeDecryptJweNoParams()
        {
            var privateKey = GetChallengePrivateKey();
            _keyRepository.Setup(x => x.GetKeys()).Returns(privateKey);

            var sut = CreateSut();
            var actual = sut.PrivateDecryptJweNoParams(encryptedChallenge);

            Assert.IsNotNull(actual);

            _keyRepository.Verify(x => x.GetKeys(), Times.Once);
        }
    }
}
