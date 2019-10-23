using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace WebAdressbookTests
{
    public class HelperBase
    {
        protected ApplicationManager manager;

        protected IWebDriver driver;

        public HelperBase(ApplicationManager manager) 
        {
            this.manager = manager;
            driver = manager.Driver;
        }

        public ApplicationManager GetManager()
        {
            return this.manager;
        }

        public void Type(By locator, string text)
        {
            if (text != null)
            {
                driver.FindElement(locator).Click();
                driver.FindElement(locator).Clear();
                driver.FindElement(locator).SendKeys(text);
            }

        }
        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool HasElementsWithProperty(By by)
        {
            try
            {
               var elements = driver.FindElements(by);

                return elements.Count > 0;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}