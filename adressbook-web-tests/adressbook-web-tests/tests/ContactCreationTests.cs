using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;


namespace WebAdressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(20))
                {
                    LastName = GenerateRandomString(20)
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            return (List<ContactData>)
                    new XmlSerializer(typeof(List<ContactData>))
                    .Deserialize(new StreamReader(@"adressbook-web-tests\\contact.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText("adressbook-web-tests\\contact.json"));
        }

        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTest(ContactData contact)
        {

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
