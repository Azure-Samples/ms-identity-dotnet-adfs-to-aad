# Migrating AD FS Directory Extensions to Azure Active Directory

> This sample assumes that you already have an AD FS environment.

## Scenario

You have an AD FS environment with Directory Extensions, and you want to migrate them to your Azure Active Directory tenant.

## About the sample

This documentation guides you how to configure [Azure AD Connect](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-whatis) to migrate Directory Extensions from your AD FS to Azure Active Directory.

### Pre-requisites

- An AD FS environment
- An Azure Active Directory (Azure AD) tenant. For more information on how to get an Azure AD tenant, see [How to get an Azure AD tenant](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/)
- [Azure AD Connect](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-whatis) configured on a domain joined machine

## Migrate Directory Extensions

While users, groups and claims will get synced to Azure AD using the Azure AD Connect tool out of the box, AD FS Directory Extensions require an extra configuration to have it done.

If your AD FS doesn't have Directory Extensions but you would like to add it so you can try this sample, please follow [this tutorial to create an extension](https://social.technet.microsoft.com/wiki/contents/articles/51121.active-directory-how-to-add-custom-attribute-to-schema.aspx).

## Configuring Azure AD Connect for Directory Extensions

1. Open the Azure AD Connect tool and select **Configure**
2. Select the option **Customize synchronization options** and click **Next**
3. Sign-in with your AAD global administrator user
4. Select **Next** on the *Connect your directories* tab
5. Select **Next** on the *Domain and OU filtering* tab
6. Check the box **Directory extension sync** and click **Next**
7. The following screen will show all the available attributes in your AD FS. Move to the right box all the attributes that you would like to send to AAD
8. Click **Next** and the synchronization will be executed.

Learn more details about [synchronizing Directory Extensions to Azure AD](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-feature-directory-extensions).

### Testing the Directory Extensions migration

To test the migration, you can access [Graph Explorer](https://aka.ms/ge) and sign-in using an AD FS user who has some of the extra attributes set.

Once signed-in and accepted the consent screen, run the query for the URL, `https://graph.microsoft.com/beta/me`, and the extra attributes will be presented in the result.

>Note: When you sync Directory Extensions via Azure AD Connect tool, their name on Microsoft Graph will have an auto-generated prefix that **cannot be edited**. For instance, if you have an extension called `regionDivision` and use the tool so sync it with Azure AD, its name will be transformed to something like: `extension_90f5761cbd854b259d47fde20b522087_regionDivision`. [Learn more about it here](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-feature-directory-extensions#configuration-changes-in-azure-ad-made-by-the-wizard).

## Use Directory Extensions in Dynamic Groups

One of the most useful scenario in dynamic groups is the usage of Directory Extensions to dynamically associate users to it.

If you would like to configure dynamic groups, [please follow this tutorial](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-feature-directory-extensions#use-the-attributes-in-dynamic-groups).

### Useful resources

- [Moving application authentication from AD FS to Azure Active Directory](https://docs.microsoft.com/azure/active-directory/manage-apps/migrate-adfs-apps-to-azure)
- [Configure SAML-based single sign-on to non-gallery applications](https://docs.microsoft.com/azure/active-directory/manage-apps/configure-single-sign-on-non-gallery-applications)
- [Synchronizing Directory Extensions to Azure AD](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-feature-directory-extensions)
- [Using Directory Extensions in Dynamic groups](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-feature-directory-extensions#use-the-attributes-in-dynamic-groups)
