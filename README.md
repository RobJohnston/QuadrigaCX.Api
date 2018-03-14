# QuadrigaCX.Api
A .Net Standard client for the QuadrigaCX cryptocurrency API. 



[![nuget](https://img.shields.io/nuget/v/QuadrigaCX.Api.svg)](https://www.nuget.org/packages/QuadrigaCX.Api/)

**This is an alpha version, meaning the API has not been tested on any production application. USE AT YOUR OWN RISK!**

Also, the API does not *yet* include the trading, deposit, and withdrawal functionality.

Contribute to this project by sending some XɃT my way:  3HwDqamKd6pcjzPF7QnLU1XyWo3PGfcGib

[![Sign-up with QuadrigaCX](https://github.com/RobJohnston/QuadrigaCX.Api/blob/master/QCX%20300x250%20White%20CDN%20Sign%20Up.jpg)](https://www.quadrigacx.com/?ref=c7flx49lbhc3b1awgl8pig7l)


## Example usage

```cs
using QuadrigaCX.Api;
using QuadrigaCX.Api.Models;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello QuadrigaCX!");

            QuadrigaClient client = new QuadrigaClient();
            Task<QuadrigaResponse<TickerInfo>> ticker = client.GetTickerInformation("btc_cad");

            Console.WriteLine(ticker.Result.RawJson);

            decimal ask = ticker.Result.Ask;
            decimal bid = ticker.Result.Bid;
            Console.WriteLine(string.Format("Spread = {0}", ask - bid));

            Console.ReadKey();
        }
    }
}

//Hello QuadrigaCX!
//{"high":"12249.98","last":"12189.99","timestamp":"1520992505","volume":"378.10630916","vwap":"12005.54209284","low":"11921.00","ask":"12150.00","bid":"12000.00"}
//Spread = 150.00

```