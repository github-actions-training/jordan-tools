namespace Jordan.Tools.Ints
{
    public static class IntExtensions
    {
        public static bool IsPrimeNumber(this int number)
        {
            var isPrime = true;
            if (number > 1)
            {
                var x = number / 2;
                for (int i = 2; i <= x; i++)
                {
                    if (number % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
            }
            else
            {
                isPrime = false;
            }
            return isPrime;
        }
    }
}