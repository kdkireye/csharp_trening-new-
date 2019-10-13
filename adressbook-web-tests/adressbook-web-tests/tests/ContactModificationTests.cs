﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAdressbookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newContactData = new ContactData("Petr");
            newContactData.LastName = "Petrov";
            app.Contacts.ModifyContact(1, newContactData);


        }

        

        

       

        

    }
}
