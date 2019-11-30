using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAdressbookTests
{
     public class RemoveContactFromGroupTests: AuthTestBase
    {
        [Test]
        public void TestRemovingContactFromGroup()
        {
            
            GroupData group = GroupData.GetAll()[0];

            bool groupHasContacts = app.Contacts.EnsureThereGroupHasContacts(group);

            if (!groupHasContacts)
            {
                ContactData newContact = new ContactData("aaa");
                newContact.LastName = "bbb";

                int newContactId = app.Contacts.AddContactDb(newContact);

                app.Groups.AddContactToGroupDb(newContactId, int.Parse(group.Id));
            }

            List<ContactData> oldList = group.GetContacts();
            ContactData contact = group.GetContacts().First();
            

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
