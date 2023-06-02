using tbk_crypto.Commands;

namespace tbk_crypto.test.Commands
{
    [TestClass]
    public class CommandLineProcessorTests
    {
        private CommandLineProcessor CreateSut()
        {
            return new CommandLineProcessor();
        }

        [TestMethod]
        public void ReadCommand_Encrypt()
        {
            string command = "--encrypt";
            string data = "unit-test";

            var args = new[] { "tbk_crypto.dll", command, data };

            var expectedCommand = SupportedCommands.Encrypt;
            var expectedArgument = data;

            var sut = CreateSut();
            var actual = sut.ReadCommand(args);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCommand, actual.Command);
            Assert.AreEqual(expectedArgument, actual.Argument);
        }

        [TestMethod]
        public void ReadCommand_Encrypt_Short()
        {
            string command = "-e";
            string data = "unit-test";

            var args = new[] { "tbk_crypto.dll", command, data };

            var expectedCommand = SupportedCommands.Encrypt;
            var expectedArgument = data;

            var sut = CreateSut();
            var actual = sut.ReadCommand(args);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCommand, actual.Command);
            Assert.AreEqual(expectedArgument, actual.Argument);
        }

        [TestMethod]
        public void ReadCommand_Decrypt()
        {
            string command = "--decrypt";
            string data = "unit-test";

            var args = new[] { "tbk_crypto.dll", command, data };

            var expectedCommand = SupportedCommands.Decrypt;
            var expectedArgument = data;

            var sut = CreateSut();
            var actual = sut.ReadCommand(args);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCommand, actual.Command);
            Assert.AreEqual(expectedArgument, actual.Argument);
        }

        [TestMethod]
        public void ReadCommand_Decrypt_Short()
        {
            string command = "-d";
            string data = "unit-test";

            var args = new[] { "tbk_crypto.dll", command, data };

            var expectedCommand = SupportedCommands.Decrypt;
            var expectedArgument = data;

            var sut = CreateSut();
            var actual = sut.ReadCommand(args);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCommand, actual.Command);
            Assert.AreEqual(expectedArgument, actual.Argument);
        }

        [TestMethod]
        public void ReadCommand_FullTest()
        {
            string data = "unit-test";

            var args = new[] { "tbk_crypto.dll", data };

            var expectedCommand = SupportedCommands.FullTest;
            var expectedArgument = data;

            var sut = CreateSut();
            var actual = sut.ReadCommand(args);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCommand, actual.Command);
            Assert.AreEqual(expectedArgument, actual.Argument);
        }

        [TestMethod]
        public void ReadCommand_FullTest_Default()
        {
            var args = new[] { "tbk_crypto.dll" };

            var expectedCommand = SupportedCommands.FullTest;
            var expectedArgument = "test";

            var sut = CreateSut();
            var actual = sut.ReadCommand(args);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCommand, actual.Command);
            Assert.AreEqual(expectedArgument, actual.Argument);
        }

        [TestMethod]
        public void ReadCommand_InvalidCommand_Error()
        {
            string command = "--invalid-command";
            string data = "unit-test";

            var args = new[] { "tbk_crypto.dll", command, data };

            var expectedCommand = SupportedCommands.Error;
            var expectedArgument = data;

            var sut = CreateSut();
            var actual = sut.ReadCommand(args);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCommand, actual.Command);
            Assert.IsNull(actual.Argument);
        }

        [TestMethod]
        public void ReadCommand_TooManyArgs_Error()
        {
            string command = "-e";
            string data = "unit-test";

            var args = new[] { "tbk_crypto.dll", command, data, "invalid-argument" };

            var expectedCommand = SupportedCommands.Error;
            var expectedArgument = data;

            var sut = CreateSut();
            var actual = sut.ReadCommand(args);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCommand, actual.Command);
            Assert.IsNull(actual.Argument);
        }
    }
}
