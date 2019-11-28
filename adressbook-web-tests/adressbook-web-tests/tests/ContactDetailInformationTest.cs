using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAdressbookTests
{
    [TestFixture]
    public class ContactDetailInformationTest : AuthTestBase
    {
        [Test]
        public void TestContactDetailInformation()
        {
            ContactData fromDetaiPage = app.Contacts.GetContactInformationFromDetailPage();
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            //verification
            
            Assert.AreEqual(fromDetaiPage.AllInformationFromDetailPage, fromForm.AllInformationFromForm);


        }
    }
}
