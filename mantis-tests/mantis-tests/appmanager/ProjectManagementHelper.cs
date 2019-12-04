using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
	public class ProjectManagementHelper : HelperBase
	{
		public ProjectManagementHelper(ApplicationManager manager) : base(manager) { }



		public ProjectManagementHelper Create(ProjectData project)
		{
			manager.MenuHelper.GoToProjects();
			InnitProjectCreation();
			FillProjectForm(project);
			SubmitProjectCreation();
			//ReturnToProjectPage();
			System.Threading.Thread.Sleep(100);
			return this;
		}

		public ProjectManagementHelper Remove(ProjectData project)
		{
			manager.MenuHelper.GoToProjects();
			SelectProject(project.Name);
			RemoveProject();
			SubmitRemoveProject();
			return this;
		}


		public ProjectManagementHelper SubmitRemoveProject()
		{
			driver.FindElement(By.CssSelector("input.btn.btn-primary.btn-white.btn-round")).Click();
			return this;
		}

		public ProjectManagementHelper RemoveProject()
		{
			driver.FindElement(By.CssSelector("input.btn.btn-primary.btn-sm.btn-white.btn-round")).Click();
			return this;
		}
		// выбрать проект по ID из базы
		public ProjectManagementHelper SelectProject(String id)
		{
			// driver.FindElement(By.CssSelector(".widget-box table tbody tr:nth-child(" + id + ") td a")).Click();
			//driver.FindElement(By.PartialLinkText("manage_proj_edit_page.php?project_id=" + id + "")).Click();
			driver.FindElement(By.LinkText(id)).Click();

			return this;
		}

		public ProjectManagementHelper InnitProjectCreation()
		{
			driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-white.btn-round")).Click();
			return this;
		}

		public ProjectManagementHelper FillProjectForm(ProjectData project)
		{
			Type(By.Name("name"), project.Name);
			Type(By.Name("description"), project.Description);
			return this;
		}

		public ProjectManagementHelper SubmitProjectCreation()
		{
			driver.FindElement(By.CssSelector("input.btn.btn-primary.btn-white.btn-round")).Click();
			return this;
		}

		public bool EnsureThereHasProject()
		{

			List<ProjectData> projects = ProjectData.GetAll();

			return projects.Count != 0;
		}

		public int AddProjectToDb(ProjectData project)
		{
			return project.AddProject(project).GetValueOrDefault();
		}
	}
}
