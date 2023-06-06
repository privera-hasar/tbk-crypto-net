namespace tbk_crypto.test
{
    [TestClass]
    public class FactoryTest
    {
        [TestMethod]
        public void GetEncryptCommand()
        {
            var actual = Factory.GetInstance().GetEncryptCommand();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void GetDecryptCommand()
        {
            var actual = Factory.GetInstance().GetDecryptCommand();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void GetFullTestCommand()
        {
            var actual = Factory.GetInstance().GetFullTestCommand();

            Assert.IsNotNull(actual);
        }
    }
}
