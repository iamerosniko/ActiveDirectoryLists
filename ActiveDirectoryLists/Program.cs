using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveDirectory;

namespace ActiveDirectoryLists
{
    class Program
    {
        static ACLQuery acl = new ACLQuery();
        static ACLEntities aclEntity = new ACLEntities();
        static void Main(string[] args)
        {
            sampleGetUsers();
            Console.Read();
        }

        //using GetUsers
        static void sampleGetUsers()
        {
            string myDomainName = ConsoleReadAndWrite("Input Domain Name: ");
            string searchString = ConsoleReadAndWrite("Search : ");

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
