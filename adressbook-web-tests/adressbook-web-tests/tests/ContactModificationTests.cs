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
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newContactData = new ContactData("Petr");
            newContactData.LastName = "Petrov";

            app.Contacts.EnsureThereIsAtLeastOneContact();

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.ModifyContact(0, newContactData);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[0].FirstName = newContactData.FirstName;
            oldContacts[0].LastName = newContactData.LastName;
            oldContacts = oldContacts.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();
            newContacts = newContacts.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();
            Assert.AreEqual(oldContacts, newContacts);

        }

        

        

       

        

    }
}
