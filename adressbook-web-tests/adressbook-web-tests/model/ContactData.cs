using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;
using LinqToDB;

namespace WebAdressbookTests
{

    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string fullName;
        private string allInformation;
        private string allPhonesFromForm;
        private string contentDetails;
        private string allPhonesFromDetailPage;

        public ContactData()
        {
        }
        public ContactData(string firstName)
        {
            FirstName = firstName;
        }

        [Column(Name = "firstname")]
        public string FirstName { get; set; }

        [Column(Name = "lastname")]
        public string LastName { get; set; }

        [Column(Name = "address")]
        public string Adress { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }
        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public string AllPhones 
        { 
            get 
            {
                if (allPhones!= null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            } 
            set 
            {
                allPhones = value;
            } 
        }
        public string AllPhonesFromForm
        {
            get
            {
                if (allPhonesFromForm != null)
                {
                    return allPhonesFromForm;
                }
                else
                {
                    return (AddPhoneSumbolH(HomePhone) + AddPhoneSumbolM(MobilePhone) + AddPhoneSumbolW(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhonesFromForm = value;
            }
        }

        public string AllPhonesFromDetailPage
        {
            get
            {
                if (allPhonesFromDetailPage != null)
                {
                    return allPhonesFromDetailPage;
                }
                else
                {
                    return HomePhone + MobilePhone + WorkPhone;
                }
            }
            set
            {
                allPhonesFromDetailPage = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUpEmail(Email) + CleanUpEmail(Email2) + CleanUpEmail(Email3) + "\r\n").Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }
        public string AllInformationFromForm
        {
            get
            {
                if (allInformation != null)
                {
                    return allInformation;
                }
                else
                {
                    return (FullName+ "\r\n" + Adress+ "\r\n"+ "\r\n" + AllPhonesFromForm+ "\r\n" + AllEmails);
                }
            }
            set
            {
                allInformation = value;
            }
        }

        public string AllInformationFromDetailPage
        {
            get
            {
                if (contentDetails != null)
                {
                    return contentDetails;
                }
                else
                {
                    return (FullName + Adress + AllPhonesFromDetailPage+ AllEmails)/*.Replace("\n", "").Replace("\r", "").Trim()*/;
                }
            }
            set
            {
                contentDetails = value;
            }
        }

        public string FullName 
        {
            get
            {
                if (fullName != null)
                {
                    return fullName.Trim();
                }
                else
                {
                    return (FirstName.Trim() + " " + LastName.Trim());
                }
            }
            set 
            {
                fullName = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace("H:", "").Replace("M:", "").Replace("W:", "") + "\r\n";
        }
        private string AddPhoneSumbolH(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return "H: "+ phone;
        }
        private string AddPhoneSumbolM(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return "M: " + phone;
        }
        private string AddPhoneSumbolW(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return "W: " + phone;
        }

        private string CleanUpEmail(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return email;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return FirstName == other.FirstName && LastName == other.LastName;

        }
        public override int GetHashCode()
        {
            return (FirstName+LastName).GetHashCode();
        }
        public override string ToString()
        {
            return "firstName&lastName=" + FirstName +" "+ LastName;
        }
        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (LastName.CompareTo(other.LastName) == 0 )
            {
                return FirstName.CompareTo(other.FirstName);
            }

            return LastName.CompareTo(other.LastName);
        }
        public static List<ContactData> GetAll()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }
        public List<ContactData> GetContact()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from c in db.Contacts
                        from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id && c.Deprecated == "0000-00-00 00:00:00")
                        select c).Distinct().ToList();
            }
        }
        
        public List<ContactData> GetContactsListWithGroup(string groupId)
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from c in db.Contacts from gcr in db.GCR.Where(p => p.GroupId == groupId && p.ContactId == c.Id && c.Deprecated == "0000-00-00 00:00:00") select c).Distinct().ToList();
            }
        }

        public int? AddContact(ContactData contact)
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return db.Contacts
                    .Value(c => c.FirstName, contact.FirstName)
                    .Value(c => c.LastName, contact.LastName)
                    .InsertWithInt32Identity();
            }
        }
    }
}
