using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectory
{
    public class AccessACL
    {
        static PrincipalContext context;

        public  string ConsoleReadAndWrite(string instruction)
        {
            Console.Write(instruction);
            return Console.ReadLine();
        }
        //this method is used to connect to server domain
        public  void GetConnectToDomain(string myDomain)
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
        public  void GetOneUsers(String searchString)
        {
           
            if (context == null)
            {
                Console.WriteLine("Domain Name not supplied. Searching for its default Domain values...");
                GetConnectToDomain("");
            }
                
            DirectoryEntry de = GetUserDetails(searchString);
            if (de != null)
            {
                //print details
                Console.WriteLine("\nSearch Result: \n");
                Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
                Console.WriteLine("Last Name : " + de.Properties["sn"].Value);
                Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
                Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
            }
            else
                Console.WriteLine("Search Not Found");

            Console.ReadLine();
        }

        DirectoryEntry GetUserDetails(string seachString)
        {
            DirectoryEntry de;
            UserPrincipal user = new UserPrincipal(context);
            PrincipalSearcher searcher = new PrincipalSearcher(user);
            for (int err = 0; err < 3; err++)
            {
                if (err == 0)
                    user.SamAccountName = seachString;
                else if (err == 1)
                    user.Surname = seachString;
                else
                    user.GivenName = seachString;

                user = searcher.FindOne() as UserPrincipal;

                try
                {
                    de = user.GetUnderlyingObject() as DirectoryEntry;
                    err = 3;
                }
                catch
                {
                    de = new DirectoryEntry();
                }
            }
            return de;
        }

        //Pass A Domain Name 
        public  void GetAllUsers(string myDomain)
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
