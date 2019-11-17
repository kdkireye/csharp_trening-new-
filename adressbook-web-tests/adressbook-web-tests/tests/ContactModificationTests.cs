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
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newContactData = new ContactData("Petr");
            newContactData.LastName = "Petrov";
            
            app.Contacts.EnsureThereIsAtLeastOneContact();

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData oldContactData = oldContacts[0];

            app.Contacts.ModifyContact(oldContactData, newContactData);

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].FirstName = newContactData.FirstName;
            oldContacts[0].LastName = newContactData.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            // oldContacts = oldContacts.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();
            // newContacts = newContacts.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();
            Assert.AreEqual(oldContacts, newContacts);
            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldContactData.Id)
                {
                    Assert.AreEqual(contact.Id, oldContactData.Id);
                }
            }
        }

        

        

       

        

    }
}
