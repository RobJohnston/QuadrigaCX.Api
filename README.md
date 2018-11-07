# QuadrigaCX.Api
A .Net Standard client for the QuadrigaCX API.

## Features
- **Full** coverage of the  [QuadrigaCX API v2](https://www.quadrigacx.com/api_info).
- Simple API abstraction uses similar method names and parameters as the QuadrigaCX API documentation. 
- Exceptions returned from the server include the error code and message to ease troubleshooting.
- Built using the [Task-based Asynchronous Pattern](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap), the recommended asynchronous design pattern for new development.
- Compatible with **.NET Standard 2.0**.

## Getting started

An account is not required to access the public API methods. 
However, if you do create an account, please use my referral code (c7flx49lbhc3b1awgl8pig7l) when you [register](https://www.quadrigacx.com/?ref=c7flx49lbhc3b1awgl8pig7l). 
It's an easy way to give back to this project at no cost to you.

[![Sign-up with QuadrigaCX](https://github.com/RobJohnston/QuadrigaCX.Api/blob/master/QCX%20300x250%20White%20CDN%20Sign%20Up.jpg)](https://www.quadrigacx.com/?ref=c7flx49lbhc3b1awgl8pig7l)

## Installation via NuGet

[![nuget](https://img.shields.io/nuget/v/QuadrigaCX.Api.svg)](https://www.nuget.org/packages/QuadrigaCX.Api/)
![Downloads](https://img.shields.io/nuget/dt/QuadrigaCx.Api.svg)

```
Install-Package QuadrigaCX.Api
```

## Example usage

```csharp
using System;
using System.Threading.Tasks;
using QuadrigaCX.Api;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello QuadrigaCX!\n");

            using (var client = new QuadrigaClient())
            {
                try
                {
                    var t = Task.Run(() => client.GetTickerInformationAsync("btc_cad"));
                    var ticker = t.Result;

                    Console.WriteLine(string.Format("Bid = {0}, Ask = {1}, Volume = {2}", 
                        ticker.Bid, ticker.Ask, ticker.Volume));
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

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
```

### Output
```
Hello QuadrigaCX!

Bid = 9050.00, Ask = 9099.98, Volume = 207.56169241

Press any key to exit.
```

## API Coverage


| PUBLIC                 |    Covered     |
|------------------------|:--------------:|
| Ticker                 |  ✔            |
| Order Book             |  ✔            |
| Transactions           |  ✔            |

| AUTHENTICATED          |    Covered     |
|------------------------|:--------------:|
| Balance                |  ✔            |
| User Transactions      |  ✔            |
| Open Orders            |  ✔            |
| Lookup Order           |  ✔            |
| Cancel Order           |  ✔            |
| Buy Order - Limit      |  ✔            |
| Buy Order - Market     |  ✔            |
| Sell Order - Limit     |  ✔            |
| Sell Order - Market    |  ✔            |
| Bitcoin Deposit        |  ✔            |
| Bitcoin Withdraw       |  ✔            |
| Bitcoin Cash Deposit   |  ✔            |
| Bitcoin Cash Withdraw  |  ✔            |
| Bitcoin Gold Deposit   |  ✔            |
| Bitcoin Gold Withdraw  |  ✔            |
| Litecoin Deposit       |  ✔            |
| Litecoin Withdraw      |  ✔            |
| Ether Deposit          |  ✔            |
| Ether Withdraw         |  ✔            |

## Errors

The following error have been found through experimentation and are not documented by QuadrigaCX.
Errors are listed in numerical order of their code with a sample message beside them.  
They are thrown as a `QuadrigaException`.

* 21 - Incorrect Amount: $98,544.47CAD exceeds available CAD balance
* 23 - Incorrect price $1.00CAD is below the minimum of $10.00CAD
* 101 - Invalid API Code or Invalid Signature
* 105 - Invalid or missing payload
* 108 - Address rejected - does not match API withdrawal address
* 106 - Cannot perform request - not found
* 300 - Permission denied
* 301 - Your ability to withdraw is temporarily disabled due to a recent profile or major settings change. For your protection, this measure is in place for 24hrs from the time the change was made

## API rate limit

QuadrigaCX seems to operate an out-of-band support group on reddit. From a question posted 2018-01-01:

>"You're limited to 30 calls per minute on both the public and private APIs to allow you to get a feel for the API. 
> Once you have managed to trade efficiently, we can consider removing these limits."

Source:  https://www.reddit.com/r/QuadrigaCX/comments/88m649/api_limit/

## My related projects

* [CoinField.Api](https://github.com/RobJohnston/CoinField.Api)
* [Ndax.Api](https://github.com/RobJohnston/Ndax.Api) and [AlphaPoint.Api](https://github.com/RobJohnston/alphapoint.api/)
* [Coinsquare.Api](https://github.com/RobJohnston/Coinsquare.Api)
* [EzBtc.Api](https://github.com/RobJohnston/EzBtc.Api)
* [AnxPro.Api](https://github.com/RobJohnston/AnxPro.Api)

## Donation addresses

* Bitcoin:  361kH8JtiP41G5qfwYj3z4vuTwyPiWdNUW
* Bitcoin Cash:  16z7wZy9aku3P3UeihjLmUDbt3aU6DaHtS
* Bitcoin Gold:  GcqW8X1od3wH5Mwn3MUTf8j3N4iHCaw1PW
* Ether:  0xb8f65ea3e9fc2c48e02a5e0ab2fab006998bc9fa
* Litecoin:  LMbcKyXLxVGwMFPT1hRaBXb6AoiVgXrYhQ
