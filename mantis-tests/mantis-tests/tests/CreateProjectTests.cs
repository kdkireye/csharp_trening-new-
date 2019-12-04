using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Collections.Generic;

namespace mantis_tests
{
	[TestFixture]
	public class CreateProjectTests : TestBase
	{
		[Test]
		public void CreateProjectTest()
		{
			List<ProjectData> oldList = ProjectData.GetAll();

			ProjectData project = new ProjectData("New Project3");
			project.Description = "New Description2";

			app.Project.Create(project);

			List<ProjectData> newList = ProjectData.GetAll();

			oldList.Add(project);
			newList.Sort();
			oldList.Sort();

			Assert.AreEqual(oldList, newList);
		}
	}
}