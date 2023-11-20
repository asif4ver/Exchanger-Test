# TestApp1

## Demo

1. Build the Solution, and Run the TestApp1.
2. It will open a swagger enabled UI for you.
3. You shall see the /Exchange/convert api being shown.
4. You can pass all 3 parameters like sourceCur, targetCur and conversionVal.
5. Once it goes thru validation, you would see successful exchange response.
6. For Dynamic configuration, one can update the exchangeRates.json with one of the exchangeKey value. And it will be picked up while another conversion request goes thru swaggerUI.

## Unit Test

1. Validation is covered.
2. Exchange converter is done using mock data.
