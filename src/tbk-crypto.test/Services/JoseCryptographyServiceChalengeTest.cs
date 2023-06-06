using Moq;
using tbk_crypto.Entities;

namespace tbk_crypto.test.Services
{
    public partial class JoseCryptographyServiceTest
    {
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

        //[TestMethod]
        public void ChallengeDecrypt()
        {
            //var data = "???";
            var encryptedData = "eyJhbGciOiJSU0EtT0FFUC0yNTYiLCJhcHAta2V5Ijoie1wia3R5XCI6XCJSU0FcIixcImtpZFwiOlwicWYtdlhQeVV6NjEtUnNBeHhyOExJem45eElTYlNoc3paci1ZYm85YTJra1wiLFwidXNlXCI6XCJlbmNcIixcImFsZ1wiOlwiUlNBLU9BRVAtMjU2XCIsXCJlXCI6XCJBUUFCXCIsXCJuXCI6XCJ4alNVTUtROU5zNnNXeTNwVy13WnpWOGZRRE4yWHlpU0hTSU01SHBvR1hqRUYyZGFzTmZoY0hNLUlSVkZHb1M3ZVpVWnVoMURKaThvNkxTaXlZSTc4amVnRW83RzBSelhHSEk2eGRDa2JjWklJWkdWRUNpbmJSdTRadU13SVlLeEVlVC1hNmxGN3V0dVVBdHlpbjlBMVkwRTBDbkRVR05sclBzUV96Z3cySUVVb0xTRHFfVExQN1lDdDJINDJ4ZFhObzgtcnJxUWQ4VjJyUFZZV1BnRHRlYUZaRHp6b0xOUm5rRjNBbE1PS0dRSFBDZmJIMXBLMmdESXp1MGRYVzl0clBLTUhZQnBOaVRiQlR1V2loWmYyZkNPWExZZ1QzS295NWJSVUtCQllrZTBFQUVBLUVTN2tHMUJ0NTdEa29kemdUdHFnX0ZRektZR2R3ck5VNkEyZVFcIn0iLCJlbmMiOiJBMjU2R0NNIiwia2lkIjoid1VZU08tQ0ZwZHNxaUVqSGxvaVRteEhtMHE4dk1YdzV6VlRrNXNKU0JyRSJ9.Cl-3TYocYmPTOlFp6qQtZ2RCRDW2eZcYBI3rd805vnUYBc8Py_wh8jw3idV22qRLDkK2wIDQu-SbLJ7bnndiFQPgOEfOJa6N5NeRKYQalS0o2EEtlSZNrsi71Pqr359EYS4hIIMX4KlGYjrjUmkuqcE4TtGdq0AxHqwdwFlnVoV7rhECEAArzSemr4WFG-VEi2yhj7w9s4WcpGhyZR45I5t8dHIe0lKdZj6C8opMgm76tLvRnXqEOU335_KTdXAxjC0KE7pmnH4vzL7y8bv1wy_FPNGRt8gbjGChQJhKl2Gaw9F28eLDo2wZC1uaLtarujBxRWzAZ2xusx8ANTj4cA.CZTogPtd_CX6NC7j.Fig7dKgb-xrc5HTlDF6kPg.OsAUN3-dMRs8veLwlsDvVA";

            _keyRepository.Setup(x => x.GetKeys()).Returns(_challengePrivateKey);

            var sut = CreateSut();
            var actual = sut.PrivateDecrypt(encryptedData);

            Assert.IsNotNull(actual);
            //Assert.AreEqual(data, actual);

            _keyRepository.Verify(x => x.GetKeys(), Times.Once);
        }
    }
}
