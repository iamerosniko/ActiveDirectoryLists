using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveDirectory;

namespace ActiveDirectoryLists
{
    class Program
    {
        static void Main(string[] args)
        {
            AccessACL acl = new AccessACL();
            ACLEntities aclEntity = new ACLEntities();
            string myDomainName = acl.ConsoleReadAndWrite("Input Domain Name: ");
            string findUserName = acl.ConsoleReadAndWrite("Search by username: ");
            //Console.WriteLine("\n");
            //GetOneUsers( findUserName);
            acl.GetConnectToDomain(myDomainName);
            aclEntity = acl.GetOneUsers(findUserName);
            acl.getUsers();
            Console.WriteLine("\nSearch Result: \n");

            //if (aclEntity == null)
            //    Console.WriteLine("Search not found.");
            //else
            //{
            //    Console.WriteLine("First Name : " + aclEntity.ACL_GivenName);
            //    Console.WriteLine("Last Name : " + aclEntity.ACL_Surname);
            //    Console.WriteLine("SAM Account Name : " + aclEntity.ACL_UserName);
            //    Console.WriteLine("User Principal Name : " + aclEntity.ACL_Principalname);
            //}
            
            Console.Read();
        }
        
    }
}
