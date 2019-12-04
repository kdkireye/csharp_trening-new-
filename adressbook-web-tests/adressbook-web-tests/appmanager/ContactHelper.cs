﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAdressbookTests
{
    public class ContactHelper : HelperBase
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
            InitContactModification(v);
            FillContactForm(newContactData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper ModifyContact(ContactData contact, ContactData newContactData)
        {
            SelectContact(contact.Id);
            InitContactModification(contact.Id);
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

        public ContactHelper Remove(ContactData contact)
        {
            SelectContact(contact.Id);
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
            contactCache = null;
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
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        { driver.FindElements(By.Name("entry"))[index]
               .FindElements(By.TagName("td"))[7]
               .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper InitContactModification(String id)
        {
            driver.FindElement(By.XPath("//a[@href=\"edit.php?id=" + id + "\"]")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper ViewContactDetailsPage(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
               .FindElements(By.TagName("td"))[6]
               .FindElement(By.TagName("a")).Click();
            return this;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    IList<IWebElement> cells = element.FindElements(By.TagName("td"));
                    IWebElement cell = cells[1];
                    ContactData contact = new ContactData(cells[2].Text);
                    contact.LastName = cells[1].Text;
                    contactCache.Add(contact);

                }
                return contactCache;
            }
            return new List<ContactData>(contactCache);
        }
        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;
            string allEmails = cells[4].Text;

            return new ContactData(firstName)
            {
                LastName = lastName,
                Adress = address,
                AllPhones = allPhones,
                AllEmails = allEmails

            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(0);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            return new ContactData(firstName)
            {
                LastName = lastName,
                Adress = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3

            };
        }


        public ContactData GetContactInformationFromDetailPage()
        {
            manager.Navigator.GoToHomePage();
            ViewContactDetailsPage(0);
            string contentDetails = driver.FindElement(By.Id("content")).Text;

            return new ContactData(contentDetails)
            {
                AllInformationFromDetailPage = contentDetails

            };
        }
        public bool EnsureThereContactAddTheGroup(ContactData contact, GroupData group)
        {
            bool canAddToGroup = true;

            List<ContactData> contactsInGroup = group.GetContacts();
            foreach (ContactData c in contactsInGroup)
            {
                if (c.Equals(contact))
                {
                    canAddToGroup = false;
                    break;
                }
            }

            return canAddToGroup;

        }

        public bool EnsureThereGroupHasContacts(GroupData group)
        {
            ContactData contact = new ContactData();
            List<ContactData> contacts = contact.GetContactsListWithGroup(group.Id);

            return contacts.Count != 0;
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public int AddContactDb(ContactData contact)
        {
            return contact.AddContact(contact).GetValueOrDefault();
        }
        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            SelectGroupFilter(group.Id);
            SelectContact(contact.Id);
            CommitRemovingContacFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void CommitRemovingContacFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void SelectGroupFilter(string groupId)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByValue(groupId);
        }

        private void SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void SelectGroupToAdd(string name)
        {
           new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void ClearGroupFilter()
        {
           new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }
    }
}
