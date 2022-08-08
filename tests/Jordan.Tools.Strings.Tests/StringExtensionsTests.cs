namespace Jordan.Tools.Strings.Tests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("hello world", "aGVsbG8gd29ybGQ=")]
        [InlineData("goodbye", "Z29vZGJ5ZQ==")]
        [InlineData("C# rocks", "QyMgcm9ja3M=")]
        public void Expect_Base64Encode_Returns_B64Encoded_String(string str, string expected)
        {
            var result = str.Base64Encode();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("aGVsbG8gd29ybGQ=", "hello world")]
        [InlineData("Z29vZGJ5ZQ==", "goodbye")]
        [InlineData("QyMgcm9ja3M=", "C# rocks")]
        public void Expect_Base64Decode_Returns_B64Decoded_String(string b64str, string expected)
        {
            var result = b64str.Base64Decode();

            Assert.Equal(expected, result);
        }
    }
}