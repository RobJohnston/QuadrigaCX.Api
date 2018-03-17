# QuadrigaCX.Api
A .Net Standard client for the QuadrigaCX cryptocurrency API. 



[![nuget](https://img.shields.io/nuget/v/QuadrigaCX.Api.svg)](https://www.nuget.org/packages/QuadrigaCX.Api/)

**This is an alpha version, meaning the API has not been tested on any production application. USE AT YOUR OWN RISK!**

The trading, deposit, and withdrawal functionality have been added but not tested at all.

Contribute to this project by sending some XɃT my way:  3HwDqamKd6pcjzPF7QnLU1XyWo3PGfcGib

[![Sign-up with QuadrigaCX](https://github.com/RobJohnston/QuadrigaCX.Api/blob/master/QCX%20300x250%20White%20CDN%20Sign%20Up.jpg)](https://www.quadrigacx.com/?ref=c7flx49lbhc3b1awgl8pig7l)


## Example usage

```cs
using QuadrigaCX.Api;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello QuadrigaCX!");

            using (QuadrigaClient client = new QuadrigaClient())
            {
                var res = client.GetTickerInformation("btc_cad").Result;
                Console.WriteLine(string.Format("Bid = {0}, Ask = {1}, Spread = {2}", res.Bid, res.Ask, res.Ask - res.Bid));
            }

            Console.ReadKey();
        }
    }
}


//Hello QuadrigaCX!
//Bid = 10914.00, Ask = 10998.00, Spread = 84.00
```