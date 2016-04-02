using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace WindowsFormsApplication1
{

    //Get an Object DistinguishedName: ADO.NET search (ADVANCED)
    //This method is the glue that ties all the methods together since most all the methods require the consumer to provide a distinguishedName.
    //Wherever you put this code, you must ensure that you add these enumerations as well.
    //This allows the consumers to specify the type of object to search for and whether they want the distinguishedName returned or the objectGUID.
    public enum objectClass
    {
        user, group, computer
    }
    public enum returnType
    {
        distinguishedName, ObjectGUID
    }
    static class Constants
    {
        //User Account Control Flags
        public const int SCRIPT = 0x0001;
        public const int ACCOUNTDISABLE = 0x0002;
        public const int HOMEDIR_REQUIRED = 0x0008;
        public const int LOCKOUT = 0x0010;
        public const int PASSWD_NOTREQD = 0x0020;
        public const int PASSWD_CANT_CHANGE = 0x0040;
        public const int ENCRYPTED_TEXT_PWD_ALLOWED = 0x0080;
        public const int TEMP_DUPLICATE_ACCOUNT = 0x0100;
        public const int NORMAL_ACCOUNT = 0x0200;
        public const int INTERDOMAIN_TRUST_ACCOUNT = 0x0800;
        public const int WORKSTATION_TRUST_ACCOUNT = 0x1000;
        public const int SERVER_TRUST_ACCOUNT = 0x2000;
        public const int DONT_EXPIRE_PASSWORD = 0x10000;
        public const int MNS_LOGON_ACCOUNT = 0x20000;
        public const int SMARTCARD_REQUIRED = 0x40000;
        public const int TRUSTED_FOR_DELEGATION = 0x80000;
        public const int NOT_DELEGATED = 0x100000;
        public const int USE_DES_KEY_ONLY = 0x200000;
        public const int DONT_REQ_PREAUTH = 0x400000;
        public const int PASSWORD_EXPIRED = 0x800000;
        public const int TRUSTED_TO_AUTH_FOR_DELEGATION = 0x1000000;
    }

    public class ADUnit
    {

        //------------------------------------------
        //          Active Directory Management
        //------------------------------------------

        // Translate the Friendly Domain Name to Fully Qualified Domain
        public static string FriendlyDomainToLdapDomain(string friendlyDomainName)
        {
            string ldapPath = null;
            try
            {
                DirectoryContext objContext = new DirectoryContext(
                    DirectoryContextType.Domain, friendlyDomainName);
                Domain objDomain = Domain.GetDomain(objContext);
                ldapPath = objDomain.Name;
            }
            catch (DirectoryServicesCOMException e)
            {
                ldapPath = e.Message.ToString();
            }
            return ldapPath;
        }

        // Enumerate Domains in the Current Forest
        public static ArrayList EnumerateDomains()
        {
            ArrayList alDomains = new ArrayList();
            Forest currentForest = Forest.GetCurrentForest();
            DomainCollection myDomains = currentForest.Domains;

            foreach (Domain objDomain in myDomains)
            {
                alDomains.Add(objDomain.Name);
            }
            return alDomains;
        }

        //Enumerate Global Catalogs in the Current Forest
        public static ArrayList EnumerateGlobalCatalogs()
        {
            ArrayList alGCs = new ArrayList();
            Forest currentForest = Forest.GetCurrentForest();
            foreach (GlobalCatalog gc in currentForest.GlobalCatalogs)
            {
                alGCs.Add(gc.Name);
            }
            return alGCs;
        }


        //Enumerate Domain Controllers in a Domain
        public static ArrayList EnumerateDomainControllers()
        {
            ArrayList alDcs = new ArrayList();
            Domain domain = Domain.GetCurrentDomain();
            foreach (DomainController dc in domain.DomainControllers)
            {
                alDcs.Add(dc.Name);
            }
            return alDcs;
        }

        //Create a Trust Relationship
        public static void CreateTrust(string sourceForestName, string targetForestName)
        {
            Forest sourceForest = Forest.GetForest(new DirectoryContext(
                DirectoryContextType.Forest, sourceForestName));

            Forest targetForest = Forest.GetForest(new DirectoryContext(
                DirectoryContextType.Forest, targetForestName));

            // create an inbound forest trust

            sourceForest.CreateTrustRelationship(targetForest,
                TrustDirection.Outbound);
        }

        //Delete a Trust Relationship
        public void DeleteTrust(string sourceForestName, string targetForestName)
        {
            Forest sourceForest = Forest.GetForest(new DirectoryContext(
                DirectoryContextType.Forest, sourceForestName));

            Forest targetForest = Forest.GetForest(new DirectoryContext(
                DirectoryContextType.Forest, targetForestName));

            // delete forest trust

            sourceForest.DeleteTrustRelationship(targetForest);
        }


        //Enumerate Objects in an OU
        //The parameter OuDn is the Organizational Unit distinguishedName such as OU=Users,dc=myDomain,dc=com
        public static ArrayList EnumerateOU(string OuDn)
        {
            ArrayList alObjects = new ArrayList();
            try
            {
                string ldapPf = null;
                if (OuDn.Substring(1, 7) == "LDAP://") { ldapPf = "LDAP://"; } else { ldapPf = ""; };
                DirectoryEntry directoryObject = new DirectoryEntry(ldapPf + OuDn);
                foreach (DirectoryEntry child in directoryObject.Children)
                {
                    string childPath = child.Path.ToString();
                    alObjects.Add(childPath.Remove(0, 7));
                    //remove the LDAP prefix from the path

                    child.Close();
                    child.Dispose();
                }
                directoryObject.Close();
                directoryObject.Dispose();
            }
            catch (DirectoryServicesCOMException e)
            {
                Console.WriteLine("An Error Occurred: " + e.Message.ToString());
            }
            return alObjects;
        }

        //Enumerate Directory Entry Settings
        //One of the nice things about the 2.0 classes is the ability to get and set a configuration object for your directoryEntry objects.
        public static void DirectoryEntryConfigurationSettings(string domainADsPath)
        {
            // Bind to current domain

            DirectoryEntry entry = new DirectoryEntry(domainADsPath);
            DirectoryEntryConfiguration entryConfiguration = entry.Options;
            //
            //Delete Console and use other wethod
            //
            Console.WriteLine("Server: " + entryConfiguration.GetCurrentServerName());
            Console.WriteLine("Page Size: " + entryConfiguration.PageSize.ToString());
            Console.WriteLine("Password Encoding: " + 
                entryConfiguration.PasswordEncoding.ToString());
            Console.WriteLine("Password Port: " + 
                entryConfiguration.PasswordPort.ToString());
            Console.WriteLine("Referral: " + entryConfiguration.Referral.ToString());
            Console.WriteLine("Security Masks: " + 
                entryConfiguration.SecurityMasks.ToString());
            Console.WriteLine("Is Mutually Authenticated: " + 
                entryConfiguration.IsMutuallyAuthenticated().ToString());
            Console.WriteLine();
            Console.ReadLine();
        }



        //------------------------------------------
        //        Active Directory Objects
        //------------------------------------------


        //Check for the Existence of an Object
        //This method does not need you to know the distinguishedName, you can concat strings or even guess a location and it will still run (and return false if not found).
        public static bool Exists(string objectPath)
        {
            bool found = false;
            string ldapPf = null;
            if (objectPath.Substring(1, 7) == "LDAP://") { ldapPf = "LDAP://"; } else { ldapPf = ""; };
            if (DirectoryEntry.Exists(ldapPf + objectPath))
            {
                found = true;
            }
            return found;
        }
        
        //Move an Object from one Location to Another
        //It should be noted that the string newLocation should NOT include the CN= value of the object.
        //The method will pull that from the objectLocation string for you.
        //So object CN=group,OU=GROUPS,DC=contoso,DC=com is sent in as the objectLocation but the newLocation is
        //        something like: OU=NewOUParent,DC=contoso,DC=com. The method will take care of the CN=group.
        public static void Move(string objectLocation, string newLocation)
        {
            //For brevity, removed existence checks
            string ldapPf = null;
            if (objectLocation.Substring(1, 7) == "LDAP://") { ldapPf = "LDAP://"; } else { ldapPf = ""; };
            DirectoryEntry eLocation = new DirectoryEntry(ldapPf + objectLocation);

            if (newLocation.Substring(1, 7) == "LDAP://") { ldapPf = "LDAP://"; } else { ldapPf = ""; };
            DirectoryEntry nLocation = new DirectoryEntry(ldapPf + newLocation);
            string newName = eLocation.Name;
            eLocation.MoveTo(nLocation, newName);
            nLocation.Close();
            eLocation.Close();
        }

        //Enumerate Multi-String Attribute Values of an Object
        //This method includes a recursive flag in case you want to recursively dig up properties of properties such as enumerating all the member values
        //      of a group and then getting each member group's groups all the way up the tree.
        public ArrayList AttributeValuesMultiString(string attributeName, string objectDn, ArrayList valuesCollection, bool recursive)
        {
            DirectoryEntry ent = new DirectoryEntry(objectDn);
            PropertyValueCollection ValueCollection = ent.Properties[attributeName];
            IEnumerator en = ValueCollection.GetEnumerator();

            while (en.MoveNext())
            {
                if (en.Current != null)
                {
                    if (!valuesCollection.Contains(en.Current.ToString()))
                    {
                        valuesCollection.Add(en.Current.ToString());
                        if (recursive)
                        {
                            AttributeValuesMultiString(attributeName, "LDAP://" +
                            en.Current.ToString(), valuesCollection, true);
                        }
                    }
                }
            }
            ent.Close();
            ent.Dispose();
            return valuesCollection;
        }

        //Enumerate Single String Attribute Values of an Object
        public string AttributeValuesSingleString  (string attributeName, string objectDn)
        {
            string strValue;
            DirectoryEntry ent = new DirectoryEntry(objectDn);
            strValue = ent.Properties[attributeName].Value.ToString();
            ent.Close();
            ent.Dispose();
            return strValue;
        }

        //Enumerate an Object's Properties: The Ones with Values
        public static ArrayList GetUsedAttributes(string objectDn)
        {
            string ldapPf = null;
            if (objectDn.Substring(1, 7) == "LDAP://") { ldapPf = "LDAP://"; } else { ldapPf = ""; };
            DirectoryEntry objRootDSE = new DirectoryEntry(ldapPf + objectDn);
            ArrayList props = new ArrayList();

            foreach (string strAttrName in objRootDSE.Properties.PropertyNames)
            {
                props.Add(strAttrName);
            }
            return props;
        }

        //A call to this class might look like:
        //        myObjectReference.GetObjectDistinguishedName(objectClass.user, returnType.ObjectGUID, "john.q.public", "contoso.com")
        public static string GetObjectDistinguishedName(objectClass objectCls, returnType returnValue, string objectName, string LdapDomain)
        {
            string distinguishedName = string.Empty;
            string ldapPf = null;
            if (LdapDomain.Substring(1, 7) == "LDAP://") { ldapPf = "LDAP://"; } else { ldapPf = ""; };
            string connectionPrefix = ldapPf + LdapDomain;
            DirectoryEntry entry = new DirectoryEntry(connectionPrefix);
            DirectorySearcher mySearcher = new DirectorySearcher(entry);

            switch (objectCls)
            {
                case objectClass.user:
                    mySearcher.Filter = "(&(objectClass=user)(|(cn=" + objectName + ")(sAMAccountName=" + objectName + ")))";
                    break;
                case objectClass.group:
                    mySearcher.Filter = "(&(objectClass=group)(|(cn=" + objectName + ")(dn=" + objectName + ")))";
                    break;
                case objectClass.computer:
                    mySearcher.Filter = "(&(objectClass=computer)(|(cn=" + objectName + ")(dn=" + objectName + ")))";
                    break;
            }
            SearchResult result = mySearcher.FindOne();

            if (result == null)
            {
                throw new NullReferenceException
                ("unable to locate the distinguishedName for the object " +
                objectName + " in the " + LdapDomain + " domain");
            }
            DirectoryEntry directoryObject = result.GetDirectoryEntry();
            if (returnValue.Equals(returnType.distinguishedName))
            {
                distinguishedName = "LDAP://" + directoryObject.Properties
                    ["distinguishedName"].Value;
            }
            if (returnValue.Equals(returnType.ObjectGUID))
            {
                distinguishedName = directoryObject.Guid.ToString();
            }
            entry.Close();
            entry.Dispose();
            mySearcher.Dispose();
            return distinguishedName;
        }
 
        //Convert distinguishedName to ObjectGUID
        public static string ConvertDNtoGUID(string objectDN)
        {
            //Removed logic to check existence first

            DirectoryEntry directoryObject = new DirectoryEntry(objectDN);
            return directoryObject.Guid.ToString();
        }

        //Convert an ObjectGUID to OctectString: The Native ObjectGUID
        public static string ConvertGuidToOctectString(string objectGuid)
        {
            System.Guid guid = new Guid(objectGuid);
            byte[] byteGuid = guid.ToByteArray();
            string queryGuid = "";
            foreach (byte b in byteGuid)
            {
                queryGuid += @"\" + b.ToString("x2");
            }
            return queryGuid;
        }

        //Search by ObjectGUID or convert ObjectGUID to distinguishedName
        public static string ConvertGuidToDn(string GUID)
        {
              DirectoryEntry ent = new DirectoryEntry();
              String ADGuid = ent.NativeGuid;
              DirectoryEntry x = new DirectoryEntry("LDAP://{GUID=" + ADGuid + ">"); 
                  //change the { to <>

              return x.Path.Remove(0,7); //remove the LDAP prefix from the path

        }

        //Publish Network Shares in Active Directory
        //Example
        private static void init()
        {
            CreateShareEntry("OU=HOME,dc=baileysoft,dc=com",
                "Music", @"\\192.168.2.1\Music", "mp3 Server Share");
            Console.ReadLine();
        }

        //Actual Method
        public static void CreateShareEntry(string ldapPath, string shareName, string shareUncPath, string shareDescription)
        {
            string oGUID = string.Empty;
            string ldapPf = null;
            if (ldapPath.Substring(1, 7) == "LDAP://") { ldapPf = "LDAP://"; } else { ldapPf = ""; };
            string connectionPrefix = ldapPf + ldapPath;
            DirectoryEntry directoryObject = new DirectoryEntry(connectionPrefix);
            DirectoryEntry networkShare = directoryObject.Children.Add("CN=" + 
                shareName, "volume");
            networkShare.Properties["uNCName"].Value = shareUncPath;
            networkShare.Properties["Description"].Value = shareDescription;
            networkShare.CommitChanges();

            directoryObject.Close();
            networkShare.Close();
        }

        //Create a New Security Group
        //Note: by default if no GroupType property is set, the group is created as a domain security group.
        public static void Create(string ouPath, string name)
        {
            if (!DirectoryEntry.Exists("LDAP://CN=" + name + "," + ouPath))
            {
                try
                {
                    DirectoryEntry entry = new DirectoryEntry("LDAP://" + ouPath);
                    DirectoryEntry group = entry.Children.Add("CN=" + name, "group");
                    group.Properties["sAmAccountName"].Value = name;
                    group.CommitChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }
            }
            else { Console.WriteLine(ouPath + " already exists"); }
        }

        //Delete a group
        public static void Delete(string ouPath, string groupPath)
        {
            if (DirectoryEntry.Exists("LDAP://" + groupPath))
            {
                try
                {
                    DirectoryEntry entry = new DirectoryEntry("LDAP://" + ouPath);
                    DirectoryEntry group = new DirectoryEntry("LDAP://" + groupPath);
                    entry.Children.Remove(group);
                    group.CommitChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }
            }
            else
            { 
                Console.WriteLine(groupPath + " doesn't exist"); 
            }
        }

        //------------------------------------------
        //          Active Directory Users Tasks
        //------------------------------------------
 

        //Authenticate a User Against the Directory
        private static bool Authenticate(string userName, string password, string domain)
        {
            bool authentic = false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain,
                    userName, password);
                object nativeObject = entry.NativeObject;
                authentic = true;
            }
            catch (DirectoryServicesCOMException) { }
            return authentic;
        }

        //Add User to Group
        public static void AddToGroup(string userDn, string groupDn)
        {
            try
            {
                DirectoryEntry dirEntry = new DirectoryEntry("LDAP://" + groupDn);
                dirEntry.Properties["member"].Add(userDn);
                dirEntry.CommitChanges();
                dirEntry.Close();
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                //doSomething with E.Message.ToString();

            }
        }

        //Remove User from Group
        public static void RemoveUserFromGroup(string userDn, string groupDn)
        {
            try
            {
                DirectoryEntry dirEntry = new DirectoryEntry("LDAP://" + groupDn);
                dirEntry.Properties["member"].Remove(userDn);
                dirEntry.CommitChanges();
                dirEntry.Close();
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                //doSomething with E.Message.ToString();

            }
        }

        //Get User Group Memberships of the Logged in User from ASP.NET
        public static ArrayList Groups()
        {
            ArrayList groups = new ArrayList();
            foreach (System.Security.Principal.IdentityReference group in
                System.Web.HttpContext.Current.Request.LogonUserIdentity.Groups)
            {
                groups.Add(group.Translate(typeof
                    (System.Security.Principal.NTAccount)).ToString());
            }
            return groups;
        }

        //Get User Group Memberships
        //This method requires that you have the AttributeValuesMultiString method earlier in the article included in your class.
        public ArrayList Groups(string userDn, bool recursive)
        {
            ArrayList groupMemberships = new ArrayList();
            return AttributeValuesMultiString("memberOf", userDn,
                groupMemberships, recursive);
        }

        //Create User Account
        public static string CreateUserAccount(string ldapPath, string userName, string userPassword)
        {
            string oGUID = string.Empty;
            try
            {
                string ldapPf = null;
                if (ldapPath.Substring(1, 7) == "LDAP://") { ldapPf = "LDAP://"; } else { ldapPf = ""; };
                string connectionPrefix = ldapPf + ldapPath;
                DirectoryEntry dirEntry = new DirectoryEntry(connectionPrefix);
                DirectoryEntry newUser = dirEntry.Children.Add
                    ("CN=" + userName, "user");
                newUser.Properties["samAccountName"].Value = userName;
                newUser.CommitChanges();
                oGUID = newUser.Guid.ToString();

                newUser.Invoke("SetPassword", new object[] { userPassword });
                newUser.CommitChanges();
                dirEntry.Close();
                newUser.Close();
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                //DoSomethingwith --> E.Message.ToString();

            }
            return oGUID;
        }

        //Dealing with User Passwords
        //There are some specifics to understand when dealing with user passwords and boundaries around passwords such as forcing a user to change their
        // password on the next logon, denying the user the right to change their own passwords, setting passwords to never expire, to when to expire,
        // and these tasks can be accomplished using UserAccountControl flags that are demonstrated in the proceeding sections.
        // Please refer to this great MSDN article: Managing User Passwords for examples and documentation regarding these features.
        // (thanks to Daniel Ocean for identifying this resource)
        //
        public static string SetUserAccountFlags(string ldapPath, string userName, int UAF)
        {
            string oGUID = string.Empty;
            try
            {
                string ldapPf = null;
                if ( ldapPath.Substring(1, 7) == "LDAP://") { ldapPf = "LDAP://"; } else { ldapPf = ""; };
                string connectionPrefix = ldapPf + ldapPath;
                DirectoryEntry dirEntry = new DirectoryEntry(connectionPrefix);
                DirectoryEntry newUser = dirEntry.Children.Add
                    ("CN=" + userName, "user");
                newUser.Properties["samAccountName"].Value = userName;
                newUser.CommitChanges();
                oGUID = newUser.Guid.ToString();
                int val = (int)newUser.Properties["userAccountControl"].Value; 
                    //newUser is DirectoryEntry object

                newUser.Properties["userAccountControl"].Value = val | UAF; 
                
                newUser.CommitChanges();
                dirEntry.Close();
                newUser.Close();
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                //DoSomethingwith --> E.Message.ToString();

            }
            return oGUID;
        }
        //Enable User Account
        public static void Enable(string userDn)
        {
            try
            {
                DirectoryEntry user = new DirectoryEntry(userDn);
                //Message(;
                //MessageBox.Show(user.Properties["samAccountNAme"].Value.ToString());
                int val = (int)user.Properties["userAccountControl"].Value;
                user.Properties["userAccountControl"].Value = val & Constants.NORMAL_ACCOUNT; //~0x2;
                //ADS_UF_NORMAL_ACCOUNT;

                user.CommitChanges();
                user.Close();
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                //DoSomethingWith --> E.Message.ToString();

            }
        }

        //Disable User Account
        public static void Disable(string userDn)
        {
            try
            {
                DirectoryEntry user = new DirectoryEntry(userDn);
                int val = (int)user.Properties["userAccountControl"].Value;
                user.Properties["userAccountControl"].Value = val | Constants.NORMAL_ACCOUNT;
                //ADS_UF_ACCOUNTDISABLE;

                user.CommitChanges();
                user.Close();
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                //DoSomethingWith --> E.Message.ToString();

            }
        }
        
        //Unlock UserAccount
        public static void Unlock(string userDn)
        {
            try
            {
                DirectoryEntry uEntry = new DirectoryEntry(userDn);
                uEntry.Properties["LockOutTime"].Value = 0; //unlock account

                uEntry.CommitChanges(); //may not be needed but adding it anyways

                uEntry.Close();
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                //DoSomethingWith --> E.Message.ToString();

            }
        }
        //Alternate Lock/Unlock Account
        // It's hard to find code to lock an account. Here is my code to lock or unlock an account.
        // dEntry is class variable already set to a user account. Shared by dextrous1.

        public static bool IsLocked (string userDn)
        {            
            DirectoryEntry dEntry = new DirectoryEntry(userDn);
            bool LockStatus=Convert.ToBoolean(dEntry.InvokeGet("IsAccountLocked"));
            
            dEntry.Close();
            return LockStatus;
            //set { dEntry.InvokeSet("IsAccountLocked", value); }
        }

        public static bool SetUserLockStatus (string userDn, bool value)
        {            
            DirectoryEntry dEntry = new DirectoryEntry(userDn);
            dEntry.InvokeSet("IsAccountLocked", Convert.ToBoolean(value));
            bool LockStatus = Convert.ToBoolean(dEntry.InvokeGet("IsAccountLocked"));
            dEntry.Close();
            return LockStatus;
        }

        //Reset User Password
        public static void ResetPassword(string userDn, string password)
        {
            DirectoryEntry uEntry = new DirectoryEntry(userDn);
            uEntry.Invoke("SetPassword", new object[] { password });
            uEntry.Properties["LockOutTime"].Value = 0; //unlock account

            uEntry.Close();
        }

        //Rename an Object
        public static void Rename(string objectDn, string newName)
        {
            DirectoryEntry child = new DirectoryEntry("LDAP://" + objectDn);
            child.Rename("CN=" + newName);
        }

        internal static void GetObjectDistinguishedName()
        {
            throw new NotImplementedException();
        }
    }
}