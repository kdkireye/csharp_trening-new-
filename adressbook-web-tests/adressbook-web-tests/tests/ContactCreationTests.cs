using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace WebAdressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {

            ContactData contact = new ContactData("Ivan");
            contact.LastName = "Ivanov";

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.CreateContact(contact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts = oldContacts.OrderBy(e=>e.LastName).ThenBy(e=>e.FirstName).ToList();
            newContacts = newContacts.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();
            Assert.AreEqual(oldContacts, newContacts);
        }

        

        

       

        

    }
}
