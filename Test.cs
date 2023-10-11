using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Advarra
{
    [TestFixture]
    public class Test
    {
        //Getting the Current working directory location to store screenshot when needed
        private static string CurrentWorkingDirectory => new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) ??
                throw new InvalidOperationException()).LocalPath;

    [SetUp]
    public void Setup()
    {
        //Initialize the browser
        Driver.Initialize(Driver.Browsers.Chrome);

    }

    [Test]
    public static void EbaySearchAndAddToCart()
    {
            try
            {
                EbayPageObject.NavigateToEbay("https://www.ebay.com/");
                string input = EbayPageObject.EnterSearchTextAndClickSearch("Iphone 14 pro max 128gb", "Iphone 14 pro max 128gb for sale | eBay");
                Assert.IsTrue(EbayPageObject.ValidateSearchEntryOnFirstPage(input));
                EbayPageObject.SelectTopEntry();
                string priceBeforeCart = EbayPageObject.AddItemToTheCart("Gold");
                string priceInCart = EbayPageObject.GetPriceInCart();

                Assert.IsTrue(priceBeforeCart.Contains(priceInCart));

            }
            catch (Exception e)
            {
                Assert.Fail("The Test is failing with the exception" + " " + e);
            }

    }

    [TearDown]
     public void TearDown()
        {
            //Take screenshot on failure
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                var screenshot = ((ITakesScreenshot)Driver.Instance).GetScreenshot();
                var filename = $"{TestContext.CurrentContext.Test.MethodName}{"_screenshot_"}{DateTime.Now.Ticks}{".jpg"}";
                var path = $"{CurrentWorkingDirectory}{filename}";
                screenshot.SaveAsFile(path, ScreenshotImageFormat.Jpeg);
                TestContext.AddTestAttachment(path);
            }

            //Dispose driver
            if(Driver.Instance != null)
            Driver.Instance.Dispose();
        }


    }
}
