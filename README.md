# QuadrigaCX.Api
A .Net Standard client for the QuadrigaCX cryptocurrency API. 



[![nuget](https://img.shields.io/nuget/v/QuadrigaCX.Api.svg)](https://www.nuget.org/packages/QuadrigaCX.Api/)

**This is an alpha version, meaning the API has not been tested on any production application. USE AT YOUR OWN RISK!**

The trading, deposit, and withdrawal functionality have been added but not tested at all.

Contribute to this project by sending some XɃT my way:  3HwDqamKd6pcjzPF7QnLU1XyWo3PGfcGib

[![Sign-up with QuadrigaCX](https://github.com/RobJohnston/QuadrigaCX.Api/blob/master/QCX%20300x250%20White%20CDN%20Sign%20Up.jpg)](https://www.quadrigacx.com/?ref=c7flx49lbhc3b1awgl8pig7l)

## Installation via NuGet
```
Install-Package QuadrigaCX.Api
```

## Example usage

```csharp
using QuadrigaCX.Api;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello QuadrigaCX!");

            using (var client = new QuadrigaClient())
            {
                try
                {
                    var ticker = client.GetTickerInformationAsync("btc_cad").GetAwaiter().GetResult();
                    Console.WriteLine(string.Format("Bid = {0}, Ask = {1}", ticker.Bid, ticker.Ask));
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

## Tested and untested methods

The following methods have been tested in the sense that they successfully call the 
[QuadrigaCX API](https://www.quadrigacx.com/api_info) and get a response that is not an error.

### Tested

* Current Trading Information
* Order Book
* Transactions
* Account balance
* User Transactions
* Open Orders
* Lookup Order
* Cancel Order
* Buy Order - Limit Order
* Bitcoin Deposit
* Bitcoin Cash Deposit
* Bitcoin Gold Deposit
* Litecoin Deposit
* Ether Deposit

### Untested

* Buy Order - Market Order
* Sell Order - Limit Order
* Sell Order - Market Order
* Bitcoin Withdraw
* Bitcoin Cash Withdraw
* Bitcoin Gold Withdraw
* Litecoin Withdraw
* Ether Withdraw

## Errors

The following error have been found through experimentation because they are not documented by QuadrigaCX.
Errors are listed in numerical order of their code with a sample message beside them, and are thrown as a `QuadrigaException`.

* 21 - Incorrect Total: $10.00USD exceeds available USD balance
* 23 - Incorrect price $1.00CAD is below the minimum of $10.00CAD
* 101 - Invalid API Code or Invalid Signature
* 105 - Invalid or missing payload
* 106 - Cannot perform request - not found
* 300 - Permission denied
