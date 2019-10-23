using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            app.Contacts.ModifyContact(1, newContactData);


        }

        

        

       

        

    }
}
