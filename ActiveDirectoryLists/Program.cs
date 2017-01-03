using System;
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
            //string myDomainName = ConsoleReadAndWrite("Input Domain Name: ");
            //string findUserName = ConsoleReadAndWrite("Search by username: ");
            //Console.WriteLine("\n");
            //GetOneUsers( findUserName);
            GetConnectToDomain("americas.manulife.net");
            GetOneUsers("macasa");
            Console.Read();
        }
        static string ConsoleReadAndWrite(string instruction)
        {
            Console.Write(instruction);
            return Console.ReadLine();  
        }
        //this method is used to connect to server domain
        static void GetConnectToDomain(string myDomain)
        {
            try
            {
                if (myDomain == "")
                    context = new PrincipalContext(ContextType.Domain);
                else
                    context = new PrincipalContext(ContextType.Domain, myDomain);

                Console.WriteLine("Successfully Connected to : " + context.ConnectedServer);
            }
            catch
            {
                Console.WriteLine("Domain Name Unreachable.");
            }
        }

        //this method is to search a user details using its username
        static void GetOneUsers(String findUserName)
        {
            try
            {
                UserPrincipal user = new UserPrincipal(context);
                //user.SamAccountName = "macagez";
                //user.Surname= findUserName;
                user.GivenName = "Gezel";
                PrincipalSearcher searcher = new PrincipalSearcher(user);
                user = searcher.FindOne() as UserPrincipal;

                DirectoryEntry de = user.GetUnderlyingObject() as DirectoryEntry;
                //print details
                Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
                Console.WriteLine("Last Name : " + de.Properties["sn"].Value);
                Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
                Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
            }

            catch
            {
                Console.Write("Search not found.");
            }

            Console.ReadLine();
        }

        //Pass A Domain Name 
        static void GetAllUsers(string myDomain)
        {
            try
            {
                var context = new PrincipalContext(ContextType.Domain, myDomain);

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

            catch
            {
                Console.Write("Domain is unreachable.");
            }

            Console.ReadLine();
        }
    }
}
