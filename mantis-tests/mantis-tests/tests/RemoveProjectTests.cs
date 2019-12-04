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
	public class RemoveProjectTests : TestBase
	{
		[Test]
		public void RemoveProjectTest()
		{
			bool appHasProjects = app.Project.EnsureThereHasProject();
			if (!appHasProjects)
			{
				ProjectData newProject = new ProjectData("Removable project");
				newProject.Description = "Removable projectDescription";


				app.Project.AddProjectToDb(newProject);
			}

			List<ProjectData> oldProjects = ProjectData.GetAll();
			ProjectData toBeRemoved = oldProjects[0];


			app.Project.Remove(toBeRemoved);



			List<ProjectData> newProjects = ProjectData.GetAll();

			oldProjects.RemoveAt(0);

			newProjects.Sort();
			oldProjects.Sort();

			Assert.AreEqual(oldProjects, newProjects);
		}
	}
}