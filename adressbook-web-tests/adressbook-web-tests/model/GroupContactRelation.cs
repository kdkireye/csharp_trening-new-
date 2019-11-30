using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Mapping;

namespace WebAdressbookTests
{
    [Table(Name = "address_in_groups")]
    public class GroupContactRelation
    {
        [Column(Name = "group_id")]
        public string GroupId { get; set; }

        [Column(Name = "id")]
        public string ContactId { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public int AddNewRelation(int contactId, int groupId)
        {
            using (var db = new AddressbookDB())
            {
                return db.GCR
                    .Value(gcr => gcr.ContactId, contactId.ToString())
                    .Value(gcr => gcr.GroupId, groupId.ToString())
                    .Insert();
            }
        }
    }
}
