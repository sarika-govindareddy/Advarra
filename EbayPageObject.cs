using Advarra.Advarra;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advarra
{

    public class EbayPageObject
    {
        //initialize common functions class
        public static WrappersSelenium wrapper = new WrappersSelenium();

        //Ebay Page Elements
        public static By SearchBox = By.XPath("//input[@aria-label='Search for anything']");
        public static By SearchButton = By.XPath("//input[@value='Search']");
        public static By SearchResults = By.XPath("//div[@class='s-item__info clearfix']/a/div[@class='s-item__title']/span");
        public static By AddToCartButton = By.XPath("//div[@class='vim x-atc-action overlay-placeholder']/a[1]");
        public static By ColorSelectionDropDown = By.XPath("//select[@id='x-msku__select-box-1000']");
        public static By ViewCartButton = By.XPath("");
        public static By Price = By.XPath("//div[@class='x-price-primary']/span[@class='ux-textspans']");
        public static By ItemAddedHeader = By.XPath("//h2[contains(text(),'1 item added to cart')]");
        public static By GoToCartButton = By.XPath("//span[contains(text(),'Go to cart')]");
        public static By PriceInCart = By.XPath("//div[@class='val-col total-row']/span/span");
        public static By LineItemInCart = By.XPath("//div[@class='cart-bucket-lineitem']");
        ///<summary>
        ///This method navigates to the web url to test
        /// </summary>
        public static void NavigateToEbay(string url)
        {
            Driver.Instance.Navigate().GoToUrl(url);
            wrapper.WaitElementToBeVisible(SearchBox);
        }

        /// <summary>
        /// This Method is used to search by text and wait for the page with the title to load
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string EnterSearchTextAndClickSearch(string text,string title)
        {
            wrapper.SendKey(SearchBox,text);
            wrapper.Click(SearchButton);
            wrapper.WaitForPageToLoad(title);
            return text.ToLower();
        }


        ///<summary>
        ///This method is to validate all the entries on first page
        /// </summary>
        public static bool ValidateSearchEntryOnFirstPage(string text)
        {
            List<IWebElement> list = wrapper.FindElements(SearchResults);
            bool flag = false;

            string[] str = text.Split(' ');
            

            foreach (IWebElement e in list)
            {
               
                string s = e.Text.ToLower();
                

                string[] st = s.Split(' ');
                for(int i=0;i<st.Length-1;i++)
                {

                    {
                        if (str.Any(x => st[i].Contains(x)))
                        {
                            continue;
                        }
                    }
                    flag = true;
                }
                s = "";
            }

            return flag;
            

        }


        ///<summary>
        ///This method is used to select the top entry and switch to new window
        /// </summary>
        public static void SelectTopEntry()
        {
            List<IWebElement> list = wrapper.FindElements(SearchResults);
            list[1].Click();
            //switch to new window tab
            Driver.Instance.SwitchTo().Window(Driver.Instance.WindowHandles[1]);
            wrapper.WaitElementToBeClickable(AddToCartButton);
        }

        ///<summary>
        ///This method is used to add item to the cart and validate the item is added to the cart
        /// </summary>
        public static string AddItemToTheCart(string color)
        {
            wrapper.SelectDropDownByText(ColorSelectionDropDown, color);
            string price = wrapper.GetElementText(Price);
            wrapper.Click(AddToCartButton);
            wrapper.WaitElementToBeVisible(ItemAddedHeader);
            wrapper.WaitElementToBeClickable(GoToCartButton);
            wrapper.Click(GoToCartButton);
            wrapper.WaitForPageToLoad("eBay shopping cart");
            Assert.IsTrue(wrapper.IsElementPresent(LineItemInCart) == true, "Item is added to the cart");
            return price;
        }

        ///<summary>
        ///This method is used to get price in cart
        /// </summary>
        public static string GetPriceInCart()
        {
           string price = wrapper.GetElementText(PriceInCart);
            return price;
        }



    }
}
