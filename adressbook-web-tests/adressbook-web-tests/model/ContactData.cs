using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        

        public ContactData(string firstName)
        {
            FirstName = firstName;
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
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

        public string Adress { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }
       
    }
}
