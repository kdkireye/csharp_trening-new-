//using System;
//using NUnit.Framework;
//using OpenQA.Selenium;
//using System.IO;
//using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.Support.UI;

//namespace mantis_tests

//{

//	[TestFixture]
//	public class AccountCreationTests : TestBase
//	{
//		[OneTimeSetUp]
//		public void setUpConfig()
//		{
//			app.Ftp.BackupFile("/config_inc.php");
//			using (Stream localFile = File.Open(@".\\mantis-tests\\config_inc.php", FileMode.Open))
//			{
//				app.Ftp.Upload("/config_inc.php", localFile);
//			}
//		}
//		[Test]
//		public void TestAccountRegistration()
//		{
//			AccountData account = new AccountData()
//			{
//				Name = "testuser4",
//				Password = "password",
//				Email = "testuser4@localhost.localdomain"
//			};

//			app.James.Delete(account);
//			app.James.Add(account);

//			app.Registration.Register(account);
//		}
//	}
//}
//		[OneTimeTearDown]
//		public void restoreConfig()
//		{
//			app.Ftp.RestoreBackupFile("/config_inc.php");
//		}
//	}
//}
