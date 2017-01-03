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
                Console.WriteLine("Domain Name Unreachable.\n");
            }
        }

        //this method is to search a user details using its username
        public ACLEntities GetOneUsers(String searchString)
        {
            
            if (context == null)
            {
                Console.WriteLine("Domain Name is invalid or not supplied. Trying to Connect to its default Domain...");
                GetConnectToDomain("");
            }
                
            DirectoryEntry de = GetUserDetails(searchString);
            if (de != null)
                return new ACLEntities{
                    ACL_GivenName = de.Properties["givenName"].Value.ToString(),
                    ACL_Surname = de.Properties["sn"].Value.ToString(),
                    ACL_UserName = de.Properties["samAccountName"].Value.ToString(),
                    ACL_Principalname= de.Properties["userPrincipalName"].Value.ToString()
                };
            else
                return null;
        }
        //this method determines if the 
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

        //public List<Principal> getUsers(string searchString)
        //{
        //    List<UserPrincipal> searchPrinciples = new List<UserPrincipal>();
        //    searchPrinciples.Add(new UserPrincipal(context) { SamAccountName = searchString+"*" });
        //    searchPrinciples.Add(new UserPrincipal(context) { Surname = searchString + "*" });
        //    searchPrinciples.Add(new UserPrincipal(context) { GivenName = searchString + "*" });
        //    List<Principal> results = new List<Principal>();
        //    var searcher = new PrincipalSearcher();
        //    foreach (var item in searchPrinciples)
        //    {
        //        searcher = new PrincipalSearcher(item);
        //        results.AddRange(searcher.FindAll());
        //    }

        //    return results;
        //}

        public List<ACLEntities> getUsers(string searchString)
        {
            if (context == null)
            {
                Console.WriteLine("Domain Name is invalid or not supplied. Trying to Connect to its default Domain...");
                GetConnectToDomain("");
            }
            List<UserPrincipal> searchPrinciples = new List<UserPrincipal>(); // this is used for filtering of users
            List<Principal> results = new List<Principal>(); // this fetch all user details from [searchPrinciples]
            List<ACLEntities> entity = new List<ACLEntities>(); // this compiles needed fields

            //this is the filter conditions 
            searchPrinciples.Add(new UserPrincipal(context) { SamAccountName = searchString + "*" });
            searchPrinciples.Add(new UserPrincipal(context) { Surname = searchString + "*" });
            searchPrinciples.Add(new UserPrincipal(context) { GivenName = searchString + "*" });

            var searcher = new PrincipalSearcher();
            foreach (var item in searchPrinciples)
            {
                searcher = new PrincipalSearcher(item);
                results.AddRange(searcher.FindAll());
            }
            for (int i = 0; i < results.Count; i++)
            {
                entity.Add(new ACLEntities
                {
                    ACL_DisplayName = results[i].DisplayName
                });
            }
            return entity;
        }


        //Pass A Domain Name 
        public void GetAllUsers(string myDomain)
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
