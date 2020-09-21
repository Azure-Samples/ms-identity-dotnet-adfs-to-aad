# Migrating a .NET MVC application using SAML protocol from AD FS to Microsoft Active Directory

## Scenario

You have a web application using SAML protocol on AD FS and you would like to migrate it to Microsoft Active Directory.

## About the sample

This sample uses a .NET MVC application configured to use SAML single-sign-on on AD FS, to migrate it to Microsoft Active Directory. The web application used in this sample is the playground project from [chapter 1](https://github.com/Azure-Samples/ms-identity-dotnet-adfs-to-aad/tree/master/1-ADFS-Host/1-1-Setup-SAML-Playground). Ideally, you would like to follow the chapter 1 first to have a testing application for this migration.

After the migration, this sample will use the `App Federation Metadata Url` from the Azure AD application, for the SAML authentication.

### Pre-requisites

- [Visual Studio](https://aka.ms/vsdownload)
- .NET Framework 4.7.2
- An AD FS environment
- An Azure Active Directory (Azure AD) tenant. For more information on how to get an Azure AD tenant, see [How to get an Azure AD tenant](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/)

## Migrate the AD FS SAML application to Azure AD

### Register the SAML application in Azure AD

1. Sign in to the [Azure portal](https://portal.azure.com) using a tenant admin account.
1. Navigate to the Microsoft identity platform for developers [Enterprise applications](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/EnterpriseApps) page.
1. Select **New registration**.
1. Select **Non gallery app**.
1. Choose a name for the application, for instance `WebApp_SAML` and select **Add** on the bottom.
1. Under the Manage section, select **Single sign-on**.
1. Select **SAML** and the **Set up Single Sign-On with SAML** page will appear.

#### Basic SAML configuration for your app

Learn more about [configuring SAML-based single sign-on on Azure AD](https://docs.microsoft.com/azure/active-directory/manage-apps/configure-single-sign-on-non-gallery-applications).

1. To edit the basic SAML configuration options, select the **Edit** icon (a pencil) in the upper-right corner of the Basic SAML Configuration section.
1. Set **Identifier (Entity ID)** with an unique URL that follows the pattern, `http://{your-appName}.{your-domain}.com`. For instance: `http://webappsaml.contoso.com`. Copy the Entity ID value to be used in later steps.
1. Set **Reply URL** with the URL that Azure AD will reply after the authentication. In this sample we are using `https://localhost:44347/`.
1. [Optional] Set the optional parameters if they are required in your scenario. The guide [Moving application authentication from Active Directory Federation Services to Azure Active Directory](https://docs.microsoft.com/azure/active-directory/manage-apps/migrate-adfs-apps-to-azure) is an excellent resource to learn about the various available options.

#### User Attributes and Claims

By default, the claims *givenname*, *surname*, *emailaddress* and *name* will be already configured. 

The playground web application also have **Employee-ID**, so we need to configure that. If you added extra claims on the AD FS SAML playground web application, follow the steps below to register all the additional claims that should be returned by the Azure AD SAML application:

1. In the **User Attributes and Claims** section, select the **Edit** icon (a pencil) in the upper-right corner.
1. Add additional claims that you would like to use in the Web App project.
1. To add a claim, select **Add new claim** at the top of the page. 
2. Enter the **Name**, for instance `employeeID`.
3. Enter the **Namespace** if desired, for instance `http://schemas.xmlsoap.org/ws/2005/05/identity/claims`.
4. Select the appropriate **Source** that contains the claim value. For **Employee-ID**, the source is **Attribute**.
5. Select the appropriate **Source Attribute** or **Transformation**, depending on what you selected on the previous step. For **Employee-ID**, please select `user.employeeid`.
6. Select **Save**. The new claim appears in the table.

#### SAML Signing Certificate

1. In the **SAML Signing Certificate** section, copy the value for `App Federation Metadata Url`. We will use it on the .NET MVC project.

### Customize Claims Emitted in Tokens

In Azure AD, it is possible to customize claims emitted in tokens for specific applications. Check the [permitted values that can be emitted](https://docs.microsoft.com/azure/active-directory/develop/active-directory-claims-mapping#table-3-valid-id-values-per-source) and [SAML exceptions and restrictions](https://docs.microsoft.com/azure/active-directory/develop/active-directory-claims-mapping#exceptions-and-restrictions).

In order to customize the claims emitted in tokens, you have to create a **claim mapping policy** and assign it to the desired application. Learn [how to create and assign a claim mapping policy](https://docs.microsoft.com/azure/active-directory/develop/active-directory-claims-mapping#claims-mapping-policy-assignment).

## Configure the .NET MVC project (WebApp_SAML) to use your app registration

Open the project **WebApp_SAML** in your IDE (like Visual Studio) to configure the code.

1. Open the `Web.config` file.
1. Replace the value for `ida:ADFSMetadata` with the link that you copied from **App Federation Metadata Url** field.
1. Replace the value for `ida:Wtrealm` with the value that you set for **Identifier (Entity ID)**. For instance, `http://webappsaml.contoso.com`.
1. Save, clean and build the solution.

### Testing the application

Clean and build the solution, then run the **WebApp_SAML** application and sign-in using a user migrated from AD FS or a user existing in your Azure AD tenant only.

All the claims configured on the **User Attributes and Claims** steps will be listed in the page, in case the signed-in user has a value set for it.

### Useful resources

- [Moving application authentication from ADFS to Azure Active Directory](https://docs.microsoft.com/azure/active-directory/manage-apps/migrate-adfs-apps-to-azure)
- [Configure SAML-based single sign-on to non-gallery applications](https://docs.microsoft.com/azure/active-directory/manage-apps/configure-single-sign-on-non-gallery-applications)
