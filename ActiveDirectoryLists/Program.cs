﻿using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ActiveDirectoryLists
{
    class Program
    {
        static PrincipalContext context;
        static void Main(string[] args)
        {
            //GetAllUsers("americas.manulife.net");
            //GetOneUsers("prd.manulifeusa.com", "alverer");
            //string myDomainName = ConsoleReadAndWrite("Input Domain Name: ");
            string findUserName = ConsoleReadAndWrite("Search by username: ");
            Console.WriteLine("\n");
            GetOneUsers( findUserName);
        }
        static string ConsoleReadAndWrite(string instruction)
        {
            Console.Write(instruction);
            return Console.ReadLine();  
        }

        static void ConnectToDomain(string myDomain)
        {

        }

        //Pass A Domain Name 
        static void GetAllUsers(string myDomain)
        {
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, myDomain))
                {
                    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                    {
                        foreach (var result in searcher.FindAll())
                        {
                            DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                            Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
                            Console.WriteLine("Last Name : " + de.Properties["sn"].Value);
                            Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
                            Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
                            Console.WriteLine();
                        }
                    }
                }

            }

            catch
            {
                Console.Write("Domain is unreachable.");
            }
           
            Console.ReadLine();
        }
        //if declared a domain name
        static void GetOneUsers(PrincipalContext context, String findUserName)
        {
            try
            {
                var user = new UserPrincipal(context);
                user.SamAccountName = findUserName;
                var searcher = new PrincipalSearcher(user);
                user = searcher.FindOne() as UserPrincipal;
                DirectoryEntry de = user.GetUnderlyingObject() as DirectoryEntry;
                Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
                Console.WriteLine("Last Name : " + de.Properties["sn"].Value);
                Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
                Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
            }

            catch
            {
                Console.Write("Domain is unreachable.");
            }

            Console.ReadLine();
        }
        static void GetOneUsers(String findUserName)
        {
            try
            {
                context = new PrincipalContext(ContextType.Domain);
                var user = new UserPrincipal(context);
                user.SamAccountName = findUserName;
                var searcher = new PrincipalSearcher(user);
                user = searcher.FindOne() as UserPrincipal;
                DirectoryEntry de = user.GetUnderlyingObject() as DirectoryEntry;
                Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
                Console.WriteLine("Last Name : " + de.Properties["sn"].Value);
                Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
                Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
            }

            catch
            {
                Console.Write("Domain is unreachable.");
            }

            Console.ReadLine();
        }

    }
}