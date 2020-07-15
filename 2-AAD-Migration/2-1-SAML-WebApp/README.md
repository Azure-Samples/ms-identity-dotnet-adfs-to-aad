# Migrating a .NET MVC application using SAML protocol from AD FS to Microsoft Active Directory

## Scenario

You have a web application using SAML protocol on AD FS and you would like to migrate it to Microsoft Active Directory.

## About the sample

// FIX LINK
This sample uses a .NET MVC application configured to use SAML single-sign-on on AD FS, to migrate it to Microsoft Active Directory. The web application used in this sample is the playground project from [chapter 1](). Ideally, you would like to follow the chapter 1 first to have a testing application for this migration.

After the migration, this sample will use the `App Federation Metadata Url` from the Azure AD application, for the SAML authentication.

### Pre-requisites

- [Visual Studio](https://aka.ms/vsdownload)
- .NET Framework 4.7.2
- An Internet connection
- An AD FS environment
- An Azure Active Directory (Azure AD) tenant. For more information on how to get an Azure AD tenant, see [How to get an Azure AD tenant](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/)

## Migrate the AD FS SAML application to Azure AD

### Register the SAML application

1. Sign in to the [Azure portal](https://portal.azure.com) using a tenant admin account.
1. Navigate to the Microsoft identity platform for developers [Enterprise applications](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/EnterpriseApps) page.
1. Select **New registration**.
1. Select **Non gallery app**.
1. Choose a name for the application, for instance `WebApp_SAML` and select **Add** on the bottom.
1. Under the Manage section, select **Single sign-on**.
1. Select **SAML** and the **Set up Single Sign-On with SAML** page will appear.

#### Basic SAML configuration

Learn more about [configuring SAML-based single sign-on on Azure AD](https://docs.microsoft.com/azure/active-directory/manage-apps/configure-single-sign-on-non-gallery-applications).

1. To edit the basic SAML configuration options, select the **Edit** icon (a pencil) in the upper-right corner of the Basic SAML Configuration section.
1. Set **Identifier (Entity ID)** with an unique URL that follows the pattern, `http://.{your-domain}.com`. For instance: `http://webappsaml.contoso.com`. Copy the Entity ID value to be used in later steps.
1. Set **Reply URL** with the URL that Azure AD will reply after the authentication. In this sample we are using `https://localhost:44347/`.
1. [Optional] Set the optional parameters if they are required in your scenario.

#### [Optional] User Attributes and Claims

By default, the claims *givenname*, *surname*, *emailaddress* and *name* will be already configured. If you added extra claims on the AD FS SAML playground web application, follow the steps below to register all the additional claims that should be returned by the Azure AD SAML application:

1. In the **User Attributes and Claims** section, select the **Edit** icon (a pencil) in the upper-right corner.
1. Add additional claims that you would like to use in the Web App project.
1. To add a claim, select **Add new claim** at the top of the page. Enter the Name and select the appropriate source.
1. Select **Save**. The new claim appears in the table.

#### SAML Signing Certificate

1. In the **SAML Signing Certificate** section, copy the value for `App Federation Metadata Url`. We will use it on the .NET MVC project.

## Configure the .NET MVC project (WebApp_SAML) to use your app registration

Open the project **WebApp_SAML** in your IDE (like Visual Studio) to configure the code.

1. Open the `Web.config` file.
1. Replace the value for `ida:ADFSMetadata` with the link that you copied from **App Federation Metadata Url** field.
1. Replace the value for `ida:Wtrealm` with the value that you set for **Identifier (Entity ID)**. For instance, `http://webappsaml.contoso.com`.
1. Save, clean and build the solution.

### Testing the application

Run the **WebApp_SAML** application and sign-in using a user migrated from AD FS or a user existing in your Azure AD tenant only.

You will notice that all the claims configured on the **User Attributes and Claims** steps will be listed in the page, case the signed-in user has a value set to it.

### Useful resources

- [Moving application authentication from ADFS to Azure Active Directory](https://docs.microsoft.com/azure/active-directory/manage-apps/migrate-adfs-apps-to-azure)
- [Configure SAML-based single sign-on to non-gallery applications](https://docs.microsoft.com/azure/active-directory/manage-apps/configure-single-sign-on-non-gallery-applications)