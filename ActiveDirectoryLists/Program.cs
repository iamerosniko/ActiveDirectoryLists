﻿using System;
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
            //string myDomainName = ConsoleReadAndWrite("Input Domain Name: ");
            string findUserName = acl.ConsoleReadAndWrite("Search by username: ");
            //Console.WriteLine("\n");
            //GetOneUsers( findUserName);
            acl.GetConnectToDomain("americas.manulife.net");
            aclEntity = acl.GetOneUsers(findUserName);
            
            Console.WriteLine("\nSearch Result: \n");

            if (aclEntity == null)
                Console.WriteLine("Search not found.");
            else
            {
                Console.WriteLine("First Name : " + aclEntity.givenname);
                Console.WriteLine("Last Name : " + aclEntity.givenname);
                Console.WriteLine("SAM Account Name : " + aclEntity.givenname);
                Console.WriteLine("User Principal Name : " + aclEntity.givenname);

            }
            
            Console.Read();
        }
        
    }
}
