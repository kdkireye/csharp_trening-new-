using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
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
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

        }

        

        

       

        

    }
}
