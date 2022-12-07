namespace Jordan.Tools.Cryptography.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var crypt = new RSAPrivateKeyGenerator();

            crypt.GenerateRSAPrivateKey();
        }
    }
}