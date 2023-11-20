namespace TestApp1
{
    //public class ExchangeRates
    //{
    //    public List<Converter>? Converters { get; set; }
    //}

    /// <summary>
    /// Converter class used to read from exchangeRates.json file thru IOptions
    /// </summary>
    public class Converter
    {
        public string? Key { get; set; }
        public decimal Value { get; set; }
    }

    /// <summary>
    /// Exchange response class
    /// </summary>
    public class ExchangeResponse
    {
        public decimal exchangeRate { get; set; }

        public decimal convertedAmt { get; set; }

        public string? message { get; set; }
    }

    //public class Testing
    //{
    //    public string Key1 { get; private set; }
    //    public string Key2 { get; private set; }
    //}

    /// <summary>
    /// Currency supported for exchange requests.
    /// </summary>
    public enum Currencies
    {
        USD,
        INR,
        EUR
    }
}
