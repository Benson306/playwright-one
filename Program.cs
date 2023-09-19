using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
class Program{
    public static async Task Main()
    {
        using var playwright = await Playwright.CreateAsync();
        
        // Launch new chromium browser instance; headless means run in background
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false}); 

        // Incognito
        var context = await browser.NewContextAsync(); 

        //Open a new tab
        var page = await context.NewPageAsync(); 

        //Navigate to the page and wait until its fully loaded
        await page.GotoAsync("https://www.google.com", new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

        //Wait for an arbitrary time (e.g, 3 seconds) to make sure everything is ready
        await Task.Delay(3000);

        if(page.Url == "https://www.google.com/")
        {
            Console.WriteLine("URL is as expected");
        }
        else
        {
            Console.WriteLine($"Expected 'https://www.google.com' but got {page.Url}");
        }

        //Wait for an arbitrary time (e.g, 3 seconds) before closing the browser
        await Task.Delay(5000);

        // Close tab
        await context.CloseAsync();

        // Close browser
        await browser.CloseAsync();
    }
}