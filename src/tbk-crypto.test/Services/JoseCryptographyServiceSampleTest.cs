using Moq;
using Newtonsoft.Json.Linq;
using tbk_crypto.Entities;
using tbk_crypto.Infrastructure;
using tbk_crypto.Services;

namespace tbk_crypto.test.Services
{
    [TestClass]
    public class JoseCryptographyServiceSampleTest
    {
        private readonly Mock<IKeyRepository> _keyRepository = new();

        private readonly string _samplePrivateKey = "{\r\n    \"p\": \"7iyVM5aSZECD8eNmHRvv1yxgJwrwICpimL1oenF4Z1Nk0epwG1v70H4Zu-M_aAJy2WvQeJNwvtK0al4K7gpBX2ATs-XPrZu0_Swv1v0Q6dB4uZ-xJNndCqFixslHAZhJ7V6rv9qHg8wq9GnSJ_ZXUcJipUcwQJEwL4FuY5BUVck\",\r\n    \"kty\": \"RSA\",\r\n    \"q\": \"vl1Y6miSbgD5gifblp5b58urLBLMhPvhbbOw0Ec56WVNFdxqURnZRNZwl1F_270_yLp_xV-ltyN-WENmJyDtPXnVFtWm6XFNhViOusZsKtczeUFDhM8AJ6Q2NaRb4stukpMPZ0RfFXVeQ9-TzUX4OyXFCfUVEvBzbANAf5NkJpk\",\r\n    \"d\": \"S8VN5KFF8yi5jlkwyX_y0L0UUxU_lWzIOpPrALhYGWThcqGIWAx6xXIgiwZuVbsNwY0pq18XwjnfX51GM1XG-pk3CbYYV9B4z5WRbC33lNpKEH_pu3cDWOZi33bPgbOUGeVgoHJFUPL6EJbH7_cHz3dCgM_JMDTXizUztm1v0dZHl-7Au-erO5S2EfGwRSLdTGUxMLROBtJZ8aO55LbtwUPhTro9O0VWPP1lms6qLuXmPhUmr9nOXeLlu2TAi79_PBeFHkxGLEKoPnKA052jvOykQqDkCSiYOCnq3OlC6qrdzjg8tP9fvY0zLipd-W-e8Vm2GUMHVbhK3oVBnf3YgQ\",\r\n    \"e\": \"AQAB\",\r\n    \"use\": \"enc\",\r\n    \"kid\": \"hwbt6poeBVLf5rIj6I_MdFaHZlDwKOjgl7_8WIo69WE\",\r\n    \"qi\": \"iI42-5I144W5_m577Z90zF_QACNHQhKDxb0EHpLry6rRytmLSoNap7mXzIX-QYJbCHW5rTQGWadUZk6fieA-hCka0BV67GYzyDehP2j0aSwtzdWYW4BwX2-rrB4gUpmrmIsHln90aE6yFn54XcJ6x0wcCD622A8pXN-kNLRobP8\",\r\n    \"dp\": \"188v1lHODebjZ68r3J5uwSLlcRsZH5woQMwHOkNM3RsUVJy_vhqn3nAUc0njLyRg2p81g7vdQhr7_RF_h9dlCr2svKo0dtOKuIhsRLqDs8kBSvjm80QxZKt9LDA_FbPe8KWCrFGfzw2VsmecVDeDQYMIDUim8SpdZf46fx7NO_k\",\r\n    \"alg\": \"RSA-OAEP-256\",\r\n    \"dq\": \"U0lB-qSv1AMHJuPLVfWI3jwkcs7yvgf5k-Z3KFjRRIT2mebqePsnLM_pSLr2hqwg-PBnsHHtbdfQkktCqUHlqezgxvHI6f-RXBLnt13Su1eWBKoCIVvzhGR69zMmpG4-vVAsABFQzHGiQW3TG49OUIpwz69zferHhO1TTb2tppk\",\r\n    \"n\": \"sRvvp-bSzuJeeT27eRhO5LxHqEnx0UFe-CGBRmPSXREQOsEeRdZas4y3-Jz7vyZcjUsQRn6A-IJ4f8vA-AUPpy47vW5VQmwlnjdW_iZrN6h71acW8sSDZ6WIhHGS10Bi4M45uKBPgD_LcOpBi5hsBJfgOgSCrNcJ19Uvv7tlXB2W-X1FTNkH05hijlTXasemfhV-Api22sdKrsyqLOX60uVjX09BuaidDGgEpj3EZKGbOoXTm2c7GpqWpIhBWLBPkTOCKWN09mkMsNn0Q6E7PtcRZlgU0LJnw9rc0381bk9gPCUOcXWNTbTVao5VirzdItCqiHKLs75iJl3YtKcbIQ\"\r\n}";
        private readonly string _sampleToken = "eyJhbGciOiJSU0EtT0FFUC0yNTYiLCJlbmMiOiJBMTI4R0NNIiwidHlwIjoiSldUIiwiYWFhIjo5Mzl9.GEcMLERUDnyDvT5vU5dpJF0trOzwqJUZt_-NmEnYE05iw2qe4KkhX83l_zemuxuCsNqZ22uKIHy0jJJVeV_R9Pup_xj_vBujBH_oog76LIQHJbWBb6GwkUKLF89e4D3A3jAZ21j6xF9aDAuJoRw83eFtdzKALZorMTiF1POIxX1_YU7Ee2RbA--m3sKxjHsnxY15YaWJ01FKFIH9OsysJ8oLfoIWCoTZ59zkTqR2VmLmITtAsHeTMh3H8ptaI6AcRMvxe0qSnf3mlUONQvkR72676GR-r8Gb5J1shZCbahyFxThmsL4pIeMgOhxYxxCuuE0B9jJ89V2VndsKYiGfTw.qimwGAI_kIFQj0B0._wB5KbDDF3F-kEiMeF0Gl0vDdh07Dv9zHgGoQ0Gj3pcF46KXxZjryukReZHr5chCzF9BWA1QpFCyIj9hK1xR4I8GUG-QpeTnrjNAE3TiZNPSATRBQfjCP24_Ugo.CotF-zvE2iPoj_Oel5knsA";

        private KeyPair GetChallengePrivateKey()
        {
            Mock<IFileReader> fileReader = new();
            fileReader.Setup(x => x.ReadFile("keys.json")).Returns(_samplePrivateKey);

            var repository = new KeyRepository(fileReader.Object);
            return repository.GetKeys();
        }

        private JoseCryptographyService CreateSut()
        {
            return new JoseCryptographyService(_keyRepository.Object);
        }

        [TestMethod]
        public void ChallengeDecryptJweNoParams()
        {
            /*
             {"iss":"DinoChiesa.github.io","sub":"olaf","aud":"tamara","iat":1686102717,"exp":1686103317}
             */
            var privateKey = GetChallengePrivateKey();
            _keyRepository.Setup(x => x.GetKeys()).Returns(privateKey);

            var sut = CreateSut();
            var actual = sut.PrivateDecryptJweNoParams(_sampleToken);

            Assert.IsNotNull(actual);
            
            dynamic plainText = JObject.Parse(actual.Plaintext);
            Assert.AreEqual("DinoChiesa.github.io", (string)plainText.iss);
            Assert.AreEqual("olaf", (string)plainText.sub);
            Assert.AreEqual("tamara", (string)plainText.aud);
            Assert.AreEqual(1686102717, (int)plainText.iat);
            Assert.AreEqual(1686103317, (int)plainText.exp);

            _keyRepository.Verify(x => x.GetKeys(), Times.Once);
        }
    }
}
