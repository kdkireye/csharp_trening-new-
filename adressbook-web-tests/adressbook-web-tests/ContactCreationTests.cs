using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAdressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            AddNewContact();
            ContactData contact = new ContactData("Ivan");
            contact.LastName = "Ivanov";
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
        }

        

        

       

        

    }
}
