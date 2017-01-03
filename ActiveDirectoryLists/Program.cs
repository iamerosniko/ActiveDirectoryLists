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
        static AccessACL acl = new AccessACL();
        static ACLEntities aclEntity = new ACLEntities();
        static void Main(string[] args)
        {
            
            //Console.WriteLine("\n");
            //GetOneUsers( findUserName);
            //acl.GetConnectToDomain(myDomainName);
            //aclEntity = acl.GetOneUsers(searchString);
            
            //Console.WriteLine("\nSearch Result: \n");

            //if (aclEntity == null)
            //    Console.WriteLine("Search not found.");
            //else
            //{
            //    Console.WriteLine("First Name : " + aclEntity.ACL_GivenName);
            //    Console.WriteLine("Last Name : " + aclEntity.ACL_Surname);
            //    Console.WriteLine("SAM Account Name : " + aclEntity.ACL_UserName);
            //    Console.WriteLine("User Principal Name : " + aclEntity.ACL_Principalname);
            //}
            sampleGetUsers();
            Console.Read();
        }

        //using GetUsers
        static void sampleGetUsers()
        {
            string myDomainName = ConsoleReadAndWrite("Input Domain Name: ");
            string searchString = ConsoleReadAndWrite("Search by username: ");

            acl.GetConnectToDomain(myDomainName);
            
            foreach (var a in acl.getUsers(searchString))
            {
                Console.WriteLine(a.ACL_DisplayName);
            }
        }

        public static string ConsoleReadAndWrite(string instruction)
        {
            Console.Write(instruction);
            return Console.ReadLine();
        }

        
    }
}
