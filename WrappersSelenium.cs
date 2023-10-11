using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advarra
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;


    namespace Advarra
    {
        //This class holds all Selenium Basic action functionalities 
        public class WrappersSelenium
        {

            /// <summary>
            /// This method finds the UI element taking By as parameter. 
            /// If no element is found, the method will return to null.
            /// </summary>
            /// <param name="by"></param>
            /// <returns></returns>
            public IWebElement FindElement(By by)
            {
                IWebElement findElement = null;
                try
                {
                    findElement = Driver.Instance.FindElement(by);

                }
                catch (Exception e)
                {
                    Assert.Fail($"The element {by} connot be found  and failing with exception" + e);
                }
                Thread.Sleep(1000);
                return findElement;
            }

            /// <summary>
            /// This method finds the UI elements.
            /// If no any elements is found, the method will return to null.
            /// </summary>
            /// <param name="by"></param>
            /// <returns></returns>
            public List<IWebElement> FindElements(By by)
            {
                IReadOnlyCollection<IWebElement> findElements = null;
                try
                {
                    findElements = Driver.Instance.FindElements(by);
                }
                catch (Exception e)
                {
                    Assert.Fail($"The elements {by} connot be found  and failing with exception" + e);
                }

                List<IWebElement> list = findElements.ToList<IWebElement>();
                return list;
            }


            /// <summary>
            /// This method finds the UI element counts
            /// If no any elements is found, the method will return to null.
            /// </summary>
            /// <param name="by"></param>
            /// <returns></returns>
            public int FindElementsCount(By by)
            {
                ReadOnlyCollection<IWebElement> findElements = null;
                try
                {
                    findElements = Driver.Instance.FindElements(by);
                }
                catch (Exception e)
                {
                    Assert.Fail($"The element {by} connot be found  and failing with exception" + e);
                }
                Thread.Sleep(1000);
                return findElements.Count();
            }

            /// <summary>
            /// This method will wait element present(default waiting time is 10 seconds, which is 5 times of 2 seconds).
            /// </summary>
            /// <param name="by"></param>
            /// <returns></returns>
            public IWebElement WaitElementToBeVisible(By by)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(20));
                    IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(by));
                    return element;
                }
                catch (Exception ex)
                {
                    Assert.Fail($"The element is not visible {by} and failing with exception" + ex);
                    throw;
                }
            }

            /// <summary>
            /// This method will wait element present(default waiting time is 10 seconds, which is 5 times of 2 seconds).
            /// </summary>
            /// <param name="title"></param>
            /// <returns></returns>
            public void WaitForPageToLoad(string title)
            {
                WebDriverWait wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(60));
                wait.Until(ExpectedConditions.TitleContains(title));

            }

            /// <summary>
            /// This method will wait element Clickable(default waiting time is 10 seconds, which is 5 times of 2 seconds).
            /// </summary>
            /// <param name="by"></param>
            /// <returns></returns>
            public IWebElement WaitElementToBeClickable(By by)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(20));
                    IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
                    return element;
                }
                catch (Exception ex)
                {
                    Assert.Fail($"The element is not visible {by} and failing with exception" + ex);
                    throw;
                }
            }

            /// <summary>
            /// This method sends keys to the specific element taking the text as parameter.
            /// </summary>
            /// <param name="by"></param>
            /// <param name="text"></param>
            public void SendKey(By by, string text)
            {
                IWebElement element = WaitElementToBeVisible(by);
                element.Clear();
                element.SendKeys(text);
                Thread.Sleep(1000);
            }


            /// <summary>
            /// This method Clears the element
            /// </summary>
            /// <param name="by"></param>
            public void ClearText(By by)
            {

                IWebElement element = WaitElementToBeVisible(by);
                element.SendKeys(Keys.Control + "a");
                element.SendKeys(Keys.Delete);
                Thread.Sleep(1000);

            }

            /// <summary>
            /// This method clicks the element.
            /// </summary>
            /// <param name="by"></param>
            public void Click(By by)
            {
                IWebElement element = WaitElementToBeClickable(by);
                if (element == null)
                    Assert.Inconclusive("Can't find element by:" + by.ToString());
                else
                    element.Click();
                Thread.Sleep(1000);
            }

            /// <summary>
            /// This method validates if the element is visible and returns true or falls.
            /// </summary>
            /// <param name="by"></param>
            public bool IsElementPresent(By by)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(20));
                    IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(by));
                    return true;
                }
                catch
                {
                    
                    return false;
                }

            }

            /// <summary>
            /// This method validates if the element is enabled.
            /// </summary>
            /// <param name="by"></param>
            public void IsElementEnabled(By by)
            {
                if (Driver.Instance.FindElement(by).Enabled)
                {
                    Assert.IsTrue(true, "The element with xpath'" + by.ToString() + "' is enabled as expect");
                }
                else
                    Assert.IsTrue(false, "The element with xpath'" + by.ToString() + "' is disabled!");
            }

            /// <summary>
            /// This method validates if the element is enabled.
            /// </summary>
            /// <param name="by"></param>
            public void IsDisabled(By by)
            {
                if (Driver.Instance.FindElement(by).Enabled)
                {
                    Assert.IsTrue(false, "The element with xpath'" + by.ToString() + "' is enabled!");
                }
                else
                    Assert.IsTrue(true, "The element with xpath'" + by.ToString() + "' is disabled!");
            }

            /// <summary>
            /// This method validates if the element is disabled.
            /// </summary>
            /// <param name="by"></param>
            public void IsElementDisabled(By by)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(3));
                    IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(by));
                    Assert.IsTrue(false, "The element with xpath'" + by.ToString() + "' is enabled!");
                }

                catch
                {
                    Assert.IsTrue(true, "The element with xpath'" + by.ToString() + "' is disabled");
                }

            }

            /// <summary>
            /// This method validates if the element is displayed.
            /// </summary>
            /// <param name="by"></param>
            public void IsElementDisplayed(By by)
            {
                IWebElement element = WaitElementToBeVisible(by);
                if (element != null && element.Displayed)
                {
                    Assert.IsTrue(true, "The element with xpath'" + by.ToString() + "' is displayed as expect");
                }
                else
                    Assert.IsTrue(false, "The element with xpath'" + by.ToString() + "' is not displayed!");
            }


            /// <summary>
            /// This method will go back to previous page
            /// </summary>
            public void NavigateBack()
            {
                Thread.Sleep(1000);
                Driver.Instance.Navigate().Back();
                Thread.Sleep(1000);

            }

            /// <summary>
            /// This method verifies if the element value contains the expected text.
            /// </summary>
            /// <param name="e"></param>
            /// <param name="expText"></param>
            public void VerifyElementValueContains(IWebElement e, string expText)
            {
                if (e.GetAttribute("value").Contains(expText))
                {
                    Assert.IsTrue(true, "The element'" + e.TagName + "'  value contains expect text'" + expText + "'.");
                }
                else
                    Assert.IsTrue(false, "The element'" + e.TagName + "'  value NOT contains expect text'" + expText + "'.");
            }

            
            

            /// <summary>
            /// This method gets the element text.
            /// </summary>
            /// <param name="by"></param>
            /// <returns></returns>
            public string GetElementText(By by)
            {
                string value = null;
                IWebElement element = WaitElementToBeVisible(by);
                if (element != null && element.Text != null)
                {
                    value = element.Text;
                }
                return value;
            }



            
            /// <summary>
            /// This method is used to Select a drop down from list
            /// </summary>
            /// <param name="by"></param>
            /// <param name="text"></param>
            /// <returns></returns>
            public void SelectDropDownByText(By by, string text)
            {
                SelectElement select = new SelectElement(Driver.Instance.FindElement(by));
                select.SelectByText(text);
                Thread.Sleep(1000);
            }

            /// <summary>
            /// This method is used to Select a drop down from list by Text
            /// </summary>
            /// <param name="by" ></ param >
            /// <param name="value"></ param >
            public void SelectDropDownByValue(By by, string value)
            {
                SelectElement select = new SelectElement(Driver.Instance.FindElement(by));
                select.SelectByValue(value);
                Thread.Sleep(1000);

            }

            /// <summary>
            /// This method is used to Select a drop down from list by Text
            /// </summary>
            /// <param name="by" ></ param >
            /// <param name="index"></ param >
            public void SelectDropDownByIndex(By by, int index)
            {
                SelectElement select = new SelectElement(Driver.Instance.FindElement(by));
                select.SelectByIndex(index);
                Thread.Sleep(1000);

            }

            /// <summary>
            /// This method is used to Dismiss Alerts
            /// </summary>
            public void DismissAlert()
            {
                IAlert alert = Driver.Instance.SwitchTo().Alert();
                alert.Dismiss();
            }

           



        }
    }

}
