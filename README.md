# ActiveDirectoryLists

This Solution is used to search users that are in Domain's Access Control Lists

Instructions to use:

1.You must copy and paste the ActiveDirectory.DLL located at ./ActiveDirectory/bin/debug to your solution.

2.Add ActiveDirectory.DLL to project references by right clicking the project's references > add references > click browse and select ActiveDirectory.DLL.

3.include ActiveDirectory in namespaces

4.create an instance of the following : 

  * ACLQuery -- A Class that is responsible on searching users to defined Domain Server. It has functions.
    1.) GetConnectToDomain(myDomainName) -- it is used to set a domain server. if you pass an empty string it returns its user's connected Domain Server as default.
    
    2.) GetUsers(searchString) -- it is used to select all users according to searchString. it Returns a list of All users
    
  * ACLEntities -- A Class that holds a user's information after querying.
  

    
