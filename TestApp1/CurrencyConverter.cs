namespace TestApp1
{
    public static class CurrencyConverter
    {
        /// <summary>
        /// Validates Currency source or target
        /// </summary>
        /// <param name="currency">The currency</param>
        /// <returns>True or False</returns>
        public static bool ValidateCurrency(string currency)
        {
            return Enum.IsDefined(typeof(Currencies), currency);
        }

        /// <summary>
        /// Validates Conversion Value
        /// </summary>
        /// <param name="conversion">The conversion</param>
        /// <returns>True or False</returns>
        public static bool ValidateConversionVal(decimal conversion)
        {
            return !(conversion <= 0 );
        }
    }
}
