﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
	public class ApplicationManager
	{
		protected IWebDriver driver;
		protected string baseURL;

	

		private static ThreadLocal<ApplicationManager> app=new ThreadLocal<ApplicationManager>();


		private ApplicationManager()
		{
			driver = new FirefoxDriver();
			//driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));
			baseURL = "http://localhost:8080";
			//"http://localhost/mantisbt-1.2.17/login_page.php"
			Registration = new RegistrationHelper(this);
			Ftp = new FtpHelper(this);
			James = new JamesHelper(this);
			Mail = new MailHelper(this);
		}

		~ApplicationManager()
		{
			try
			{
				driver.Quit();
			}
			catch (Exception)
			{
				// Ignore errors if unable to close the browser
			}
		}

		public static ApplicationManager GetInstance()
		{
			if (! app.IsValueCreated)
			{
				ApplicationManager newInstance = new ApplicationManager();
				newInstance.driver.Url = "http://localhost:8080/mantisbt-2.22.1/login_page.php";
				app.Value = newInstance;
			}
			return app.Value;
		}

		public IWebDriver Driver
		{
			get
			{
				return driver;
			}
		}

		public RegistrationHelper Registration { get; set; }

		public FtpHelper Ftp { get; set; }

		public JamesHelper James { get; set; }
		public MailHelper Mail { get; }
	}
}
