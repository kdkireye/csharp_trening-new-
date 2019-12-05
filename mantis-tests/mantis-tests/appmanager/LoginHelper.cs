using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
	public class LoginHelper : HelperBase
	{
		public LoginHelper(ApplicationManager manager) : base(manager) { }

		public void Login(AccountData account)
		{
			if (IsLoggedIn())
			{
				if (IsLoggedIn(account))
				{
					return;
				}
				Logout();
			}
			Type(By.Name("username"), account.Name);
			driver.FindElement(By.CssSelector("input.width-40.pull-right.btn.btn-success.btn-inverse.bigger-110")).Click();
			Type(By.Name("password"), account.Password);
			driver.FindElement(By.CssSelector("input.width-40.pull-right.btn.btn-success.btn-inverse.bigger-110")).Click();
		}

		public void Logout()
		{
			if (IsLoggedIn())
			{
				driver.FindElement(By.CssSelector("span.user-info")).Click();
				//driver.FindElement(By.CssSelector("li.divider")).Click();
				driver.FindElement(By.LinkText("Выход")).Click();
			}
		}

		public bool IsLoggedIn()
		{
			return IsElementPresent(By.CssSelector("div.navbar-buttons.navbar-header.navbar-collapse.collapse"));
		}
		public bool IsLoggedIn(AccountData account)
		{
			return IsLoggedIn()
				&& GetLoggetUserName() == account.Name;
		}

		public string GetLoggetUserName()
		{
			string text = driver.FindElement(By.CssSelector("span.user-info")).Text;
			return text.Substring(1, text.Length - 2);

		}
	}
}

