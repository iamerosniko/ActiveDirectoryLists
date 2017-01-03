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
            //string myDomainName = ConsoleReadAndWrite("Input Domain Name: ");
            string findUserName = acl.ConsoleReadAndWrite("Search by username: ");
            //Console.WriteLine("\n");
            //GetOneUsers( findUserName);
            acl.GetConnectToDomain("americas.manulife.net");
            acl.GetOneUsers(findUserName);
            Console.Read();
        }
        
    }
}
