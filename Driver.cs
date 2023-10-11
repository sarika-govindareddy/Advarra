using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advarra
{
    public class Driver
    {
        public static IWebDriver Instance;

        public enum Browsers
        {

            Chrome,
            Edge
        }

        //Initiate selenium webdriver with appropriate browsers
        public static void Initialize(Browsers browser)
        {

            switch (browser)
            {

                case Browsers.Chrome:

                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--no-sandbox");
                    chromeOptions.AddArgument("--ignore-certificate-errors");
                    
                    Instance = new ChromeDriver(chromeOptions);
                    break;


                case Browsers.Edge:
                    EdgeOptions edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("--no-sandbox");
                    edgeOptions.AddArgument("--ignore-certificate-errors");
                    

                    Instance = new EdgeDriver(edgeOptions);
                    break;
            }
            //Maximize browser window after iniatializing
            Instance.Manage().Window.Maximize();
            //Adding Default 10seconds implicit wait times for webdriver
            Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }
    }
}
