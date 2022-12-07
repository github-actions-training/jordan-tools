using Jordan.Tools.Cryptography;

namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var crypt = new RSAPrivateKeyGenerator();

            //crypt.GenerateRSAPrivateKey();
            crypt.GenerateCSR();
        }
    }
}