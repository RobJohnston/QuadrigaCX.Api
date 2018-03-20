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
                try
                {
                    var res = client.GetTickerInformationAsync("btc_cad").GetAwaiter().GetResult();
                    Console.WriteLine(string.Format("Bid = {0}, Ask = {1}", res.Bid, res.Ask));
                }
                catch (QuadrigaException qex)
                {
                    Console.WriteLine(string.Format("Error: {0} - {1}", qex.Code, qex.Message));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.ReadKey();
        }
    }
}


//Hello QuadrigaCX!
//Bid = 10914.00, Ask = 10998.00
```