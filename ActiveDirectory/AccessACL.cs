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
        public ACLEntities GetOneUsers(String searchString)
        {
            
            if (context == null)
            {
                Console.WriteLine("Domain Name not supplied. Searching for its default Domain values...");
                GetConnectToDomain("");
            }
                
            DirectoryEntry de = GetUserDetails(searchString);
            if (de != null)
                return new ACLEntities{
                    givenname = de.Properties["givenName"].ToString(),
                    surname = de.Properties["sn"].ToString(),
                    username = de.Properties["samAccountName"].ToString(),
                    principalname = de.Properties["userPrincipalName"].ToString()
                };
            else
                return null;
        }

        DirectoryEntry GetUserDetails(string searchString)
        {
            DirectoryEntry de = null;
            for (int err = 0; err < 3; err++)
            {
                UserPrincipal user = new UserPrincipal(context);
                PrincipalSearcher searcher = new PrincipalSearcher(user);
                if (err == 0)
                    user.SamAccountName = searchString;
                else if (err == 1)
                    user.Surname = searchString;
                else
                    user.GivenName = searchString;

                user = searcher.FindOne() as UserPrincipal;

                try
                {
                    return user.GetUnderlyingObject() as DirectoryEntry;
                }
                catch
                {
                    de = null;
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
