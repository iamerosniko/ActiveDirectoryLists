using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectory
{
    public class ACLQuery
    {
        static PrincipalContext context;

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
      
        //this method is used to getUsers filtered by searchString
        public List<ACLEntities> getUsers(string searchString)
        {
            if (searchString.Length == 0)
                return new List<ACLEntities>();

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

            //this loop gets the result according to its filter conditions
            foreach (var item in searchPrinciples)
            {
                searcher = new PrincipalSearcher(item);
                results.AddRange(searcher.FindAll());
            }
            //fills [results]'s values to list of [entity]
            for (int i = 0; i < results.Count; i++)
            {
                entity.Add(new ACLEntities
                {
                    ACL_DisplayName = results[i].DisplayName
                });
            }
            return entity;
        }
    }
}
