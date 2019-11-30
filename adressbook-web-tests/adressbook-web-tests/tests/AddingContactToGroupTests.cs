using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAdressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
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
            bool canAddToGroup = app.Contacts.EnsureThereContactAddTheGroup(contact, group); // можем ли добавить контакт в выбранную группу?


            if (canAddToGroup)
            {
                app.Contacts.AddContactToGroup(contact, group);

            }
            else
            {
                contact.FirstName = "aaa";
                contact.LastName = "bbb";
                app.Contacts.CreateContact(contact);

                app.Contacts.AddContactToGroup(contact, group);

            }

            System.Threading.Thread.Sleep(100);
            List<ContactData> newList = group.GetContacts();

            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
