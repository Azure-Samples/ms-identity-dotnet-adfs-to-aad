# Playground .NET MVC Web application to test the migration to Azure AD

> This sample assumes that you already have an AD FS environment.

## Scenario

You have an AD FS web application using SAML protocol and would like to migrate this application to Azure AD.

## About the sample

This sample is a .NET MVC web application, configured to use SAML single-sign-on on AD FS, that you could use as a playground app to test the migration to Azure AD.

### Pre-requisites

- [Visual Studio](https://aka.ms/vsdownload)
- .NET Framework 4.7.2
- An AD FS environment
- An Azure Active Directory (Azure AD) tenant. For more information on how to get an Azure AD tenant, see [How to get an Azure AD tenant](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/)

### Useful resources

- [Moving application authentication from ADFS to Azure Active Directory](https://docs.microsoft.com/azure/active-directory/manage-apps/migrate-adfs-apps-to-azure)
- [Configure SAML-based single sign-on to non-gallery applications](https://docs.microsoft.com/azure/active-directory/manage-apps/configure-single-sign-on-non-gallery-applications)

## Configuring the sample on your AD FS

### Step 1: Adding a Relying Party Trust

1. Log into the server where AD is installed
1. Open the Server Manager Dashboard. Under Tools choose **ADFS Management**
1. Select **Add Relying Party Trust**
1. Click **Start**
1. Choose the option **Enter data about relying party manually** and click **Next**
1. Add a display name, for instance `AD FS Migration Playground`, and click **Next**
1. Choose the **AD FS profile** option and click **Next**
1. Click **Next** on the Configure Certificate step
1. Click **Next** on the Configure URL step
1. Under Relying party trust identifier add your application’s website (this sample uses `https://localhost:44347`)
1. Click **Add** and then click **Next**
    - >Note: The website needs to have an SSL certificate and https as the navigation path. The AD FS will only communicate through https context. For demo purposes, we have an IIS Express development certificate.
1. Click **Next** on the Configure Multi-factor Authentication step
1. Click **Next** on the Choose Issuance Authorization Rules step
1. Click **Next** on the Ready to Add Trust step
1. Close the wizard

### Step 2: Adding Claim Policy

1. On AD FS Management window, select **Relying Party Trust**
1. Select the application name that you have chosen on the previous step and click **Edit Claim Issuance Policy** on the right menu
1. In the new popup window, click **Add Rule**
1. Choose the Claim rule template as **Send LDAP Attributes as Claims** and click **Next**
1. Add a Claim rule name, for instance `Basic Claims`
1. Select **Active Directory** as the Attribute Store
1. Configure the LDAP attributes to outgoing claim types. Add the above four LDAP attributes and their corresponding outing claim type:
    - E-Mail-Addresses -> Email Address
    - User-Principal-Name -> UPN
    - Given-Name -> Given Name
    - Display-Name -> Name
    - Employee-ID -> EmployeeID
1. [*Optional*] Feel free to map other claims that are relevant in your scenario, like groups and directory extensions
1. Click **Finish**

### Step 3: Configuring the .NET MVC application

This sample is using the NuGet package **Microsoft.Owin.Security.WsFederation** to configure the authentication with AD FS.

1. Open the **WebApp_SAML** application
1. Open the `Web.config` file and replace the key `ida:ADFSMetadata` value with your ADFS FederationMetadata.xml URI. For instance: 
    ```c#
    <add key="ida:ADFSMetadata" value="https://sts.contoso.com/FederationMetadata/2007-06/FederationMetadata.xml" />
    ```
1. The key `ida:Wtrealm` is the current website URL. Since this sample is running on *localhost*, there is no need to update it. 

## Run the sample

Clean and build the solution, then run the **WebApp_SAML** application and sign-in using an AD FS user. The homepage will print the claims in the user's token.
