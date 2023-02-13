using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SitecoreQAAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declaring driver variable
            //chrome driver is installed as part of a NuGet package, otherwise, we can explicitly set the path for it too:
            /*
            ChromeOptions options = new ChromeOptions();
            options.BinaryLocation = @"C:\Driver\chromedriver.exe";
            */
            IWebDriver driver = new ChromeDriver();

            //Navigating to URL
            driver.Navigate().GoToUrl("https://www.amazon.com/");

            //Finding the searchbox by id, then typing something into it
            IWebElement searchBox = driver.FindElement(By.Id("twotabsearchtextbox"));
            searchBox.SendKeys("laptop");

            //Find the search button by id, then clicking it
            IWebElement searchButton = driver.FindElement(By.Id("nav-search-submit-button"));
            searchButton.Click();

            //Find the 1st result by class name, then clicking into it
            IWebElement result = driver.FindElement(By.XPath("//span[@class='a-size-medium a-color-base a-text-normal']"));
            result.Click();

            //Find the price of the product by class name
            IWebElement resultPrice = driver.FindElement(By.XPath("//span[@class='a-price-whole']"));

            //Converting the price from string to decimal
            string priceString = resultPrice.Text;
            Console.WriteLine(priceString);
            decimal finalPrice = decimal.Parse(priceString);


            //assert that the laptop price is more than $100
            if (finalPrice <= 100)
            {
                throw new Exception("Laptop price is not more than $100, it is $" + finalPrice);
            }

            //If it is $100, we check how many cents it is
            if (finalPrice == 100)
            {
                IWebElement cents = driver.FindElement(By.XPath("//span[@class='a-price-fraction']"));
                string centString = cents.Text;
                //if it is 0 cents, then it is $100, which is not more than $100, otherwise, it is more than $100
                if (decimal.Parse(centString) == 0)
                {
                    throw new Exception("Laptop price is not more than $100, it is $" + finalPrice);
                }
            }

            Console.WriteLine("Checkpoint reached, test passed");

            driver.Quit();

        }
    }
}