# QuadrigaCX.Api
A .Net Standard client for the QuadrigaCX cryptocurrency API. 



[![nuget](https://img.shields.io/nuget/v/QuadrigaCX.Api.svg)](https://www.nuget.org/packages/QuadrigaCX.Api/)

**This is a beta version, meaning the API has not been tested on any production application. USE AT YOUR OWN RISK!**

An account is not required to access the public API methods. 
However, if you do create an account, please use my referral code (c7flx49lbhc3b1awgl8pig7l) when you [register](https://www.quadrigacx.com/?ref=c7flx49lbhc3b1awgl8pig7l). 
It's an easy way to give back at no cost to you.

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

All functionality has been added but not all tested thoroughly.  
There are some discrepancies between what is written in the [QuadrigaCX API documentation](https://www.quadrigacx.com/api_info) and what has been found through experimentation.


The following methods have been tested in the sense that they successfully call the 
[QuadrigaCX API](https://www.quadrigacx.com/api_info) and get a response that is not an error.

The untested methods are expected to succeed, but may raise an error if the response isn't what was expected.

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
* Buy Order - Market Order (API docs incorrect)
* Sell Order - Limit Order
* Sell Order - Market Order (API docs incorrect)
* Bitcoin Deposit
* Bitcoin Cash Deposit
* Bitcoin Gold Deposit
* Litecoin Deposit
* Ether Deposit

### Untested

* Bitcoin Withdraw
* Bitcoin Cash Withdraw
* Bitcoin Gold Withdraw
* Litecoin Withdraw
* Ether Withdraw

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

* [EzBtc.Api](https://github.com/RobJohnston/EzBtc.Api)
* [Coinsquare.Api](https://github.com/RobJohnston/Coinsquare.Api)
* [AnxPro.Api](https://github.com/RobJohnston/AnxPro.Api)

## Donation addresses

* Bitcoin:  361kH8JtiP41G5qfwYj3z4vuTwyPiWdNUW
* Bitcoin Cash:  16z7wZy9aku3P3UeihjLmUDbt3aU6DaHtS
* Bitcoin Gold:  GcqW8X1od3wH5Mwn3MUTf8j3N4iHCaw1PW
* Ether:  0xb8f65ea3e9fc2c48e02a5e0ab2fab006998bc9fa
* Litecoin:  LMbcKyXLxVGwMFPT1hRaBXb6AoiVgXrYhQ
