using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TestApp1;
using TestApp1.Controllers;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {

        [Fact]
        public void Test_ValidateCurrency_Pass()
        {
            var response = CurrencyConverter.ValidateCurrency("EUR");

            Assert.True(response);
        }

        [Fact]
        public void Test_ValidateCurrency_Fail()
        {
            var response = CurrencyConverter.ValidateCurrency("AED");

            Assert.False(response);
        }

        [Fact]
        public void Test_ValidateConversionVal_Pass()
        {
            var response = CurrencyConverter.ValidateConversionVal(10);

            Assert.True(response);
        }

        [Fact]
        public void Test_ValidateConversionVal_Fail()
        {
            var response = CurrencyConverter.ValidateConversionVal(0);

            Assert.False(response);
        }

        [Fact]
        public void Test_ConvertApi_Pass()
        {
            // Arrange
            var logger = Mock.Of<ILogger<ExchangeController>>();

            var forecastController = new ExchangeController(GetOptionsSnapShotObj().Object, logger);
            var response = forecastController.GetExchangeRates("USD", "INR", 10);
            var result = Assert.IsType<OkObjectResult>(response);

            // Assert
            Assert.Equal("CurrencyEchange Rate retrieval for USD_TO_INR is succesful!", ((ExchangeResponse)result.Value).message);
            Assert.Equal(10, ((ExchangeResponse)result.Value).exchangeRate);
            Assert.Equal(100, ((ExchangeResponse)result.Value).convertedAmt);
        }

        [Fact]
        public void Test_ConvertApi_WrongSourceCur_ErrorMessage()
        {
            // Arrange
            var logger = Mock.Of<ILogger<ExchangeController>>();

            var forecastController = new ExchangeController(GetOptionsSnapShotObj().Object, logger);
            var response = forecastController.GetExchangeRates("AED", "USD", 10);
            var result = Assert.IsType<BadRequestObjectResult>(response);

            // Assert
            var msg = "Invalid currency request for AED, Valid ones are USD, INR, EUR only!";
            Assert.Equal(msg, ((ExchangeResponse)result.Value).message);
        }

        [Fact]
        public void Test_ConvertApi_WrongTargetCur_ErrorMessage()
        {
            // Arrange
            var logger = Mock.Of<ILogger<ExchangeController>>();

            var forecastController = new ExchangeController(GetOptionsSnapShotObj().Object, logger);
            var response = forecastController.GetExchangeRates("USD", "AED", 10);
            var result = Assert.IsType<BadRequestObjectResult>(response);
            

            // Assert
            var msg = "Invalid currency request for AED, Valid ones are USD, INR, EUR only!";
            Assert.Equal(msg, ((ExchangeResponse)result.Value).message);
        }

        [Fact]
        public void Test_ConvertApi_SameInpurCur_ErrorMessage()
        {
            // Arrange
            var logger = Mock.Of<ILogger<ExchangeController>>();

            var forecastController = new ExchangeController(GetOptionsSnapShotObj().Object, logger);
            var response = forecastController.GetExchangeRates("USD", "USD", 10);
            var result = Assert.IsType<BadRequestObjectResult>(response);


            // Assert
            var msg = "Same currency conversion is not supported!";
            Assert.Equal(msg, ((ExchangeResponse)result.Value).message);
        }

        [Fact]
        public void Test_ConvertApi_ConversionRate_ErrorMessage()
        {
            // Arrange
            var logger = Mock.Of<ILogger<ExchangeController>>();

            var forecastController = new ExchangeController(GetOptionsSnapShotObj().Object, logger);
            var response = forecastController.GetExchangeRates("USD", "INR", -1);
            var result = Assert.IsType<BadRequestObjectResult>(response);


            // Assert
            var msg = "Invalid conversion value -1, Value should be greater than 0!";
            Assert.Equal(msg, ((ExchangeResponse)result.Value).message);
        }


        private Mock<IOptionsSnapshot<List<Converter>>> GetOptionsSnapShotObj()
        {
            var response = new List<Converter> { new Converter { Key = "USD_TO_INR", Value = 10 } };

            var mock = new Mock<IOptionsSnapshot<List<Converter>>>();
            mock.SetupGet(m => m.Value).Returns(response);

            return mock;
        }

        //private Mock<ExchangeController> GetMockControlelr()
        //{
        //    var logger = Mock.Of<ILogger<ExchangeController>>();

        //    var mock = new Mock<ExchangeController>(GetOptionsSnapShotObj().Object, logger);
        //    mock.Setup(m => m.GetExchangeRates(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>()))
        //        .Returns(IActionResult(new ExchangeResponse {  message = "Test"}));

        //    return new Mock<ExchangeController>();
        //}
    }
}