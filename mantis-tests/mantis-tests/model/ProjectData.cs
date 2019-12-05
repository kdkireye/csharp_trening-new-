using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LinqToDB.Mapping;
using LinqToDB;

namespace mantis_tests
{
	[Table(Name = "mantis_project_table")]
	public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
	{
		public ProjectData()
		{
		}
		public ProjectData(string name)
		{
			Name = name;
		}

		public ProjectData(string name, string description)
		{
			Name = name;
			Description = description;
		}

		public bool Equals(ProjectData other)
		{
			if (Object.ReferenceEquals(other, null))
			{
				return false;
			}
			if (Object.ReferenceEquals(this, other))
			{
				return true;
			}

			return Name == other.Name;

		}

		public override string ToString()
		{
			return "name=" + Name + "\ndescription=" + Description;
		}

		public int CompareTo(ProjectData other)
		{
			if (Object.ReferenceEquals(other, null))
			{
				return 1;
			}
			return Name.CompareTo(other.Name);
		}


		[Column(Name = "name")]
		public string Name { get; set; }

		[Column(Name = "description")]
		public string Description { get; set; }

		[Column(Name = "id"), PrimaryKey, Identity]
		public string Id { get; set; }

		public static List<ProjectData> GetAll()
		{
			using (MantisDB db = new MantisDB())
			{
				return (from p in db.Projects select p).Distinct().ToList();
			}
		}

		public int? AddProject(ProjectData project)
		{
			using (MantisDB db = new MantisDB())
			{
				return db.Projects
					.Value(p => p.Name, project.Name)
					.Value(p => p.Description, project.Description)
					.InsertWithInt32Identity();
			}
		}
	}
}