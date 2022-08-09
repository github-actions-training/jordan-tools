namespace Jordan.Tools.Ints.Tests
{
    public class IntExtensionsTests
    {
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        public void Expect_IsPrimeNumber_Returns_True_When_Number_Is_Prime(int number)
        {
            var result = number.IsPrimeNumber();

            Assert.True(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        public void Expect_IsPrimeNumber_Returns_False_When_Number_Is_Not_Prime(int number)
        {
            var result = number.IsPrimeNumber();

            Assert.False(result);
        }
    }
}