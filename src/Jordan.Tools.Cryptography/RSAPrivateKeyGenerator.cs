using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Jordan.Tools.Cryptography
{
    public class RSAPrivateKeyGenerator
    {
        /// <summary>
        /// Generates an RSA private key compatible with OpenSSL.
        /// </summary>
        /// <param name="keySize">Size (in bits) of the key to generate.</param>
        /// <returns>RSA private key</returns>
        public string GenerateRSAPrivateKey(int keySize = 2048)
        {
            var random = new SecureRandom();
            var keyGenerationParameters = new KeyGenerationParameters(random, keySize);
            RsaKeyPairGenerator generator = new RsaKeyPairGenerator();
            generator.Init(keyGenerationParameters);

            var keyPair = generator.GenerateKeyPair();
            var privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(keyPair.Private);
            var serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetDerEncoded();
            var serializedPrivate = Convert.ToBase64String(serializedPrivateBytes);
            serializedPrivate = Regex.Replace(serializedPrivate, ".{64}", "$0\n");

            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("-----BEGIN PRIVATE KEY-----");
            strBuilder.AppendLine(serializedPrivate);
            strBuilder.AppendLine("-----END PRIVATE KEY-----");

            var result = strBuilder.ToString();
            return result;
        }

        public void GenerateCSR()
        {
            // example from https://forum.rebex.net/4284/pkcs10-certificate-request-example-provided-bouncy-working

            var name = new X509Name("O=Cuscal Limited, L=Sydney, ST=NSW, C=AU, CN=*.banking.cuscal.com.au");
            
            //Key generation 2048bits
            var rkpg = new RsaKeyPairGenerator();
            rkpg.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
            AsymmetricCipherKeyPair ackp = rkpg.GenerateKeyPair(); //BAPI.EncryptionKey;

            //Key Usage Extension
            var ku = new KeyUsage(KeyUsage.KeyEncipherment | KeyUsage.DataEncipherment);
            var extgen = new X509ExtensionsGenerator();
            extgen.AddExtension(X509Extensions.KeyUsage, false, ku);

            // https://coderanch.com/t/744982/engineering/Certificate-Generation-Bouncy-Castle
            // for example of how to set extended key usage

            var eku = new ExtendedKeyUsage(new KeyPurposeID[] { KeyPurposeID.IdKPServerAuth });
            extgen.AddExtension(X509Extensions.ExtendedKeyUsage, false, eku);
            var attribute = new AttributeX509(PkcsObjectIdentifiers.Pkcs9AtExtensionRequest, new DerSet(extgen.Generate()));

            //PKCS #10 Certificate Signing Request
            Pkcs10CertificationRequest csr = new Pkcs10CertificationRequest("SHA1WITHRSA", name, ackp.Public, new DerSet(attribute), ackp.Private);

            var xx = Convert.ToBase64String(csr.GetDerEncoded());
            xx = Regex.Replace(xx, ".{64}", "$0\n");

            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("-----BEGIN CERTIFICATE REQUEST-----");
            strBuilder.AppendLine(xx);
            strBuilder.AppendLine("-----END CERTIFICATE REQUEST-----");
            Console.WriteLine(strBuilder.ToString());
        }
    }
}
