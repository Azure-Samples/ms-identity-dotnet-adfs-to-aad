# Registering a .NET MVC Web application on on-prem AD with an ADFS instance 

## Scenario

In this chapter, we'd integrate the provided .NET MVC web application to an ADFS instance first.

### About the sample

Here we use the provided ASP.NET web application that uses the [SAML](https://docs.microsoft.com/azure/active-directory/develop/single-sign-on-saml-protocol) protocol to authenticate users.
We'd first register this web app with an ADFS instance and then later move on to register (migrate) this application to the Azure AD tenant that's connected (synced) to the on-prem Active Directory domain of this ADFS instance.

### Pre-requisites

> This sample assumes that you already have an on-prem Active Directory and an ADFS environment.

- [Visual Studio](https://aka.ms/vsdownload)
- .NET Framework 4.7.2
- An Internet connection
- A SSL certificate to use during registering the app (Relying Party) on ADFS.
- An Azure Active Directory (Azure AD) tenant. For more information on how to get an Azure AD tenant, see [How to get an Azure AD tenant](https://azure.microsoft.com/documentation/articles/active-directory-howto-tenant/)

## Configuring the sample on an ADFS server

### Step 1: Add a Relying Party Trust

1. Log into the server in the on-prem Active Directory domain where ADFS is also installed.
1. Open the **Server Manager** Dashboard. Under Tools choose **ADFS Management**
1. Select **Add Relying Party Trust**
1. Click **Start**
1. Choose the option **Enter data about relying party manually** and click **Next**
1. Add a display name, for instance `WebApp_SAML`, and click **Next**
1. Choose the **ADFS profile** option and click **Next**
1. Click **Next** on the Configure Certificate step.
1. Click **Next** on the Configure URL step
1. Under Relying party trust identifier add your application’s web URL (this sample uses `https://localhost:44347`)
1. Click **Add** and then click **Next**
    > For demo purposes, we have an IIS Express development certificate.
1. Click **Next** on the Configure Multi-factor Authentication if needed
1. Click **Next** on the Choose Issuance Authorization Rules if needed
1. Click **Next** on the Ready to Add Trust step if needed.
1. Close the wizard

### Step 2: Adding a Claim Policy

1. On the ADFS Management window, select **Relying Party Trust**
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

### Step 3: Configure the .NET MVC application

This sample is using the NuGet package **Microsoft.Owin.Security.WsFederation** to configure the authentication with ADFS.

1. Open the **WebApp_SAML** application
1. Open the `Web.config` file and replace the key `ida:ADFSMetadata` value with your ADFS FederationMetadata.xml URI. For instance:

    ```xml
    <add key="ida:ADFSMetadata" value="https://sts.contoso.com/FederationMetadata/2007-06/FederationMetadata.xml" />
    ```

1. The key `ida:Wtrealm` is the current website URL. Since this sample is running on *localhost*, there is no need to update it.

## Run the sample

Clean and build the solution, then run the **WebApp_SAML** application and sign-in using an on-prem Active Directory user. The homepage will print the claims in the user's token.

> If you find a bug in the sample, please raise the issue on [GitHub Issues](../../issues).

## Next chapter

In the next chapter, we'd cover [Azure AD Connect sync configuration for migration scenarios](../1-2-Setup-AzureADConnect/README.md)

### Useful resources

- [Moving application authentication from ADFS to Azure Active Directory](https://docs.microsoft.com/azure/active-directory/manage-apps/migrate-adfs-apps-to-azure)
- [Configure SAML-based single sign-on to non-gallery applications](https://docs.microsoft.com/azure/active-directory/manage-apps/configure-single-sign-on-non-gallery-applications)
