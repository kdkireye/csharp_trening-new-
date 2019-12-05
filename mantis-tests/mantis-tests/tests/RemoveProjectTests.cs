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
	public class RemoveProjectTests : TestBase
	{
		[Test]

		public void RemoveProjectTest()
		{
			AccountData account = new AccountData("administrator", "root");

			bool appHasProjects = app.Project.EnsureThereHasProject();
			if (!appHasProjects)
			{
				ProjectData projectData = new ProjectData("Removable project");
				projectData.Description = "Removable projectDescription";


				app.API.CreateNewProject(account, projectData);
			}

			//List<ProjectData> oldProjects = ProjectData.GetAll();

			List<Mantis.ProjectData> mantisProjectData = app.API.GetProjectsList(account);

			List<ProjectData> oldProjects = mantisProjectData.Select(e => new ProjectData(e.name, e.description)).ToList();

			ProjectData toBeRemoved = oldProjects[0];


			app.Project.Remove(toBeRemoved);


			List<Mantis.ProjectData> newMantisProjectData = app.API.GetProjectsList(account);
			List<ProjectData> newProjects = newMantisProjectData.Select(e => new ProjectData(e.name, e.description)).ToList();

			oldProjects.RemoveAt(0);

			newProjects.Sort();
			oldProjects.Sort();

			Assert.AreEqual(oldProjects, newProjects);
		}
	}
}