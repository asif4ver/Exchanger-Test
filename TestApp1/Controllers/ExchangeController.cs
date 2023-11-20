using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq;

namespace TestApp1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeController : ControllerBase
    {
        private readonly IList<Converter> _exchangeRates;
        private readonly ILogger<ExchangeController> _logger;
        
        public ExchangeController(IOptionsSnapshot<List<Converter>> options, ILogger<ExchangeController> logger)
        {
            _logger = logger;
            _exchangeRates = options.Value.ToList();
        }


        [HttpGet]
        [Route("convert")]
        public IActionResult GetExchangeRates(string sourceCur, string targetCur, decimal conversionVal)
        {

            _logger.LogInformation("CurrencyExchange convert api is called!");

            var currencyErrMsg = "Invalid currency request for {0}, Valid ones are USD, INR, EUR only!";
            var conversionErrMsg = "Invalid conversion value {0}, Value should be greater than 0!";

            if (CurrencyConverter.ValidateCurrency(sourceCur) == false)
            {
                currencyErrMsg = string.Format(currencyErrMsg, sourceCur);
                _logger.LogError(currencyErrMsg);
                return BadRequest(new ExchangeResponse { message = currencyErrMsg});
            }

            if (CurrencyConverter.ValidateCurrency(targetCur) == false)
            {
                currencyErrMsg = string.Format(currencyErrMsg, targetCur);
                _logger.LogError(currencyErrMsg);
                return BadRequest(new ExchangeResponse { message = currencyErrMsg });
            }

            if (sourceCur == targetCur)
            {
                var errMsg = "Same currency conversion is not supported!";
                _logger.LogError(errMsg);
                return BadRequest(new ExchangeResponse { message = errMsg });
            }

            if (CurrencyConverter.ValidateConversionVal(conversionVal) == false)
            {
                conversionErrMsg = string.Format(conversionErrMsg, conversionVal);
                _logger.LogError(conversionErrMsg);
                return BadRequest(new ExchangeResponse { message = conversionErrMsg });
            }

            //ExchangeRates rates = new ExchangeRates();

            //using (StreamReader r = new StreamReader("exchangeRates.json"))
            //{
            //    string json = r.ReadToEnd();
            //    rates = JsonConvert.DeserializeObject<ExchangeRates>(json);
            //}

            string myKey = $"{sourceCur}_TO_{targetCur}";
            Converter val = _exchangeRates.Where(e => e.Key == myKey).First();

            var msgForEchange = $"CurrencyEchange Rate retrieval for {myKey} is succesful!";
            _logger.LogInformation(msgForEchange);
            
            decimal currencyRate = val.Value;

            var response = new ExchangeResponse
            {
                exchangeRate = currencyRate,
                convertedAmt = currencyRate * conversionVal,
                message = msgForEchange
            };
            
            return Ok(response);
        }
    }
}