using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace mantis_tests
{
	[TestFixture]
	public class CreateProjectTests : TestBase
	{
		[Test]
		public void CreateProjectTest()
		{
			//List<ProjectData> oldList = ProjectData.GetAll();
			AccountData account = new AccountData("administrator", "root");
			//List<ProjectData> oldList = ProjectData.GetAll();
			List<Mantis.ProjectData> mantisProjectData = app.API.GetProjectsList(account);
			List<ProjectData> oldList = mantisProjectData.Select(e => new ProjectData(e.name, e.description)).ToList();

			ProjectData project = new ProjectData("New Project8");
			project.Description = "New Description8";

			app.Project.Create(project);

			//List<ProjectData> newList = ProjectData.GetAll();

			List<Mantis.ProjectData> newMantisProjectData = app.API.GetProjectsList(account);
			List<ProjectData> newList = newMantisProjectData.Select(e => new ProjectData(e.name, e.description)).ToList();

			oldList.Add(project);
			newList.Sort();
			oldList.Sort();

			Assert.AreEqual(oldList, newList);
		}
	}
}