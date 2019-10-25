using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAdressbookTests
{
    public class ContactHelper: HelperBase
    {
        public ContactHelper(ApplicationManager manager)
            : base(manager)
        {
        }

       

        public ContactHelper CreateContact(ContactData contact)
        {
            AddNewContact();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;

        }

        

        public ContactHelper ModifyContact(int v, ContactData newContactData)
        {
            SelectContact(v);
            InitContactModification();
            FillContactForm(newContactData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

       

        public ContactHelper Remove(int v)
        {
            SelectContact(v);
            RemoveContact();
            SubmitRemoveContact();
            return this;
        }

        

        public ContactHelper AddNewContact()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        private bool IsHasContacts()
        {
            return HasElementsWithProperty(By.Name("selected[]"));
        }

        public ContactHelper EnsureThereIsAtLeastOneContact()
        {
            if (!IsHasContacts())
            {
                ContactData contact = new ContactData("aaa");
                contact.LastName = "bbb";

                CreateContact(contact);
            }
            return this;
        }

        public ContactHelper SubmitRemoveContact()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[1]")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public List<ContactData> GetContactList()
        {
            List<ContactData> contacts = new List<ContactData>();
            manager.Navigator.GoToHomePage();
            ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
            foreach (IWebElement element in elements)
            {
                IList<IWebElement> cells = element.FindElements(By.TagName("td"));
                IWebElement cell = cells[1];
                ContactData contact = new ContactData(cells[2].Text);
                contact.LastName = cells[1].Text;
                contacts.Add(contact);




                /* //String[] values = element.Text.Split(' ');
                 IList<IWebElement> cells = element.FindElements(By.TagName("td")); 
                 String[] values = new String[cells.Count];
                 int i = 0;
                 foreach (IWebElement element1 in cells)
                 {
                    //values[++i] = element1.Text;
                     // ContactData contact = new ContactData(values[1]);
                     //contact.LastName = values[0];
                     contacts.Add(new ContactData (element1.Text));
                     i++;
                 }
                 //ContactData contact = new ContactData(values[1]);
                 //contact.LastName = values[0];
                 */
            }
            return contacts;
        }
    }
}
