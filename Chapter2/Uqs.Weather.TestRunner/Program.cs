using Microsoft.Extensions.Logging;
using Uqs.Weather.Controllers;

var logger = new Logger<WeatherForecastController>(null);
//fails
var controller = new WeatherForecastController(logger,
    null!);
double f1 = controller.ConvertCToF(-1.0);
if (f1 != 30.20d) throw new Exception("Invalid");
double f2 = controller.ConvertCToF(1.2);
if (f2 != 34.16d) throw new Exception("Invalid");
Console.WriteLine("Test Passed");