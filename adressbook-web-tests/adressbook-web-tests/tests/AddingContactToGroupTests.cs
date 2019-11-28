using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAdressbookTests
{
     public class AddingContactToGroupTests: AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
        
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            var manager = app.Groups.GetManager();
            manager.Navigator.GoToGroupsPage();
            app.Groups.EnsureThereIsAtLeastOneGroup();
            manager.Navigator.GoToHomePage();
            app.Contacts.EnsureThereIsAtLeastOneContact();
            app.Contacts.EnsureThereContactAddTheGroup(contact, group);

            app.Contacts.AddContactToGroup(contact, group);


            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
