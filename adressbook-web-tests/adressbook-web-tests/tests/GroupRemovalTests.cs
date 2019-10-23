using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAdressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
 
        [Test]
        public void GroupRemovalTest()
        {
            app.Groups.GetManager().Navigator.GoToGroupsPage();
            app.Groups.EnsureThereIsAtLeastOneGroup();
            app.Groups.Remove(1);
            
        }  
    }
}
