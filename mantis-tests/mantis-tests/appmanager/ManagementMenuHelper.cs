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
	public class ManagementMenuHelper : HelperBase
	{
		public string baseURL;

		public ManagementMenuHelper(ApplicationManager manager, string baseURL) : base(manager)
		{
			this.baseURL = baseURL;
		}

		public void GoToManagementPage()
		{
			driver.FindElement(By.CssSelector("i.menu-icon.fa.fa-gears")).Click();
		}

		public void GoToProjectManagementPage()
		{
			driver.FindElement(By.LinkText("Управление проектами")).Click();
		}
		public void GoToProjects()
		{
			GoToManagementPage();
			GoToProjectManagementPage();
		}
	}
}
