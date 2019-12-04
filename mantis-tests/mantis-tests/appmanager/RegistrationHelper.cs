using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
	public class RegistrationHelper:HelperBase
	{
		public RegistrationHelper(ApplicationManager manager) : base(manager)
		{

		}

		public void Register(AccountData account)
		{
			OpenMainPage();
			OpenRegistrationForm();
			FillRegistrationForm(account);
			SubmitRegistration();
			String url = GetConfirmationUrl(account);
			FillPasswordForm(url);
			SubmitPasswordForm();
		}

		private void SubmitPasswordForm()
		{
			throw new NotImplementedException();
		}

		private void FillPasswordForm(string url)
		{
			throw new NotImplementedException();
		}

		private string GetConfirmationUrl(AccountData account)
		{
			String message = manager.Mail.GetLastMail(account);
			Match match = Regex.Match(message, @"http://\S*");
			return match.Value;
		}

		private void OpenRegistrationForm()
		{
			driver.FindElement(By.CssSelector("a.back-to-login-link.pull-left")).Click();
		}

	
		private void SubmitRegistration()
		{
			driver.FindElement(By.CssSelector("input.width-40.pull-right.btn.btn-success.btn-inverse.bigger-110")).Click();
			
		}

		private void FillRegistrationForm(AccountData account)
		{
			driver.FindElement(By.Name("username")).SendKeys(account.Name);
			driver.FindElement(By.Name("email")).SendKeys(account.Email);

		}

		private void OpenMainPage()
		{
			manager.Driver.Url = "http://localhost:8080/mantisbt-2.22.1/login_page.php";

		}
	}
}
