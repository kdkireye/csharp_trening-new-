using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests_autoit
{
	public class GroupHelper : HelperBase
	{
		public static string GROUPWINTITLE = "Group editor";
		public static string CONTACTGROUPSTITLE = "Group list";
		public static string DELETEGROUPWINTITLE = "Delete group";


		public GroupHelper(ApplicationManager manager) : base(manager) { }

		public List<GroupData> GetGroupList()
		{
			List<GroupData> list = new List<GroupData>();
			OpenGroupsDialogue();
			string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.a6ad9a1", "GetItemCount", "#0", "");
			for (int i = 0; i < int.Parse(count); i++)
			{
				string item = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.a6ad9a1", "GetText", "#0 | #"+i, "");
				list.Add(new GroupData()
				{
					Name = item
				});
			}
			
			return list;
		}

		public void EnsureThereIsAtLeastTwoGroup()
		{
			List<GroupData> list = new List<GroupData>();
			
			string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
				"GetItemCount", "#0", "");
			if (int.Parse(count) <= 1)
			{
				GroupData newGroup = new GroupData()
				{
					Name = "test1"
				};

				Add(newGroup);
			}

		}

		public void Remove(GroupData toBeRemoved)
		{
			EnsureThereIsAtLeastTwoGroup();
			SelectGroup(toBeRemoved); // выбирем элемент из корня
			aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d51"); // клик на кнопку "удалить"
			aux.WinWait(DELETEGROUPWINTITLE);
			aux.ControlClick(DELETEGROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d51"); 
			aux.WinWait(DELETEGROUPWINTITLE);
			aux.ControlClick(DELETEGROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53"); // клик "ок" удаление
			CloseDeleteGroupsDialogue();
			CloseGroupsDialogue();
		}

		private void SelectGroup(GroupData id)
		{
			string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
				"GetItemCount", "#0", "");
			for (int i = 0; i < int.Parse(count); i++)
			{
				string item = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
				"GetText", "#0|#" + i, "");

				if (item == id.Name)
				{
					aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "Select", "#0" + i, "");
					break;
				}
			}
		}

		public void Add(GroupData newGroup)
		{
			OpenGroupsDialogue();
			aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.a6ad9a3");
			aux.Send(newGroup.Name);
			aux.Send("{ENTER}");
			CloseGroupsDialogue();

		}

		private void CloseDeleteGroupsDialogue()
		{
			aux.ControlClick(DELETEGROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d5");
		}

		private void CloseGroupsDialogue()
		{
			aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.a6ad9a4");
		}

		public void OpenGroupsDialogue()
		{
			aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.a6ad9a12");
			aux.WinWait(GROUPWINTITLE);
		}
	}
}
