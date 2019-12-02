using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_tests_autoit
{
	[TestFixture]
	public class GroupRemovalTests : TestBase
	{
		[Test]
		public void TestGroupRemove()
		{
			app.Groups.OpenGroupsDialogue();
			app.Groups.EnsureThereIsAtLeastTwoGroup();
			List<GroupData> oldGroups = app.Groups.GetGroupList();
			GroupData toBeRemoved = oldGroups[1];

			app.Groups.Remove(toBeRemoved);


			List<GroupData> newGroups = app.Groups.GetGroupList();

			oldGroups.RemoveAt(1);
			oldGroups.Sort();
			newGroups.Sort();

			Assert.AreEqual(oldGroups, newGroups);
		}
	}
}