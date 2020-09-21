# Migrating AD FS Security Group to Azure Active Directory

## Scenario

You have an AD FS application that uses security groups, and you want that applications on Azure AD can work with the same groups.

### About the sample

This documentation guides you how to configure an Azure AD application to include the security groups used on an ADFS application.

### Pre-requisites

- An AD FS environment
- An Azure Active Directory (Azure AD) tenant. For more information on how to get an Azure AD tenant, see [How to get an Azure AD tenant](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/)
- [Azure AD Connect](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-whatis) configured on a domain joined machine

## Migrate Security Groups

Users and groups will get synced to Azure AD using the Azure AD Connect tool out of the box, as long as they are presented in the **Synced OU** folder on AD FS.

If you haven't set Azure AD Connect tool yet, please refer to [chapter 1-2-Setup-AzureADConnect](https://github.com/Azure-Samples/ms-identity-dotnet-adfs-to-aad/tree/master/1-ADFS-Host/1-2-Setup-AzureADConnect) first.

## Configure Security Group Claims

1. Sign in to the [Azure portal](https://portal.azure.com).
2. Navigate to the Microsoft identity platform for developers [App registrations](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/RegisteredApps) page.
3. Search and select the application where you want to include Security Groups. For instance, `WebApp_SAML`.
4. Navigate to **Token configuration** on the left blade.
5. If there isn't a configuration for groups yet, select **Add groups claim**, otherwise edit the existing configuration.
6. The following blade is divided by the sections **ID**, **Access** and **SAML**. Each section correspond to the groups claim configuration for the **Id Token**, **Access Token** and **SAML** claims issued by the application respectively. For each section that you would like to include group claims, select the group property that the claim should return. [Learn more details about group claim](https://docs.microsoft.com/azure/active-directory/develop/active-directory-optional-claims#configuring-groups-optional-claims).
7. Select **Save**.

### SAML applications only - Include Group Claims

>NOTE: This step is required only for applications that use SAML authentication protocol.

Once you have configured the group claims, follow the steps below to include group claims to your SAML application:

1. Sign in to the [Azure portal](https://portal.azure.com).
2. Navigate to the Microsoft identity platform for developers [Enterprise applications](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/EnterpriseApps) page.
3. Search and select the application where you want to include Security Groups. For instance, `WebApp_SAML`.
4. Select **Single sign-on** on the left blade.
5. In the **User Attributes & Claims** section, select the **Edit** icon (a pencil) in the upper-right corner.
6. Select **Add a group claim**.
7. Choose the group to be returned in the claim, in this sample select **Security groups**.
8. For **Source attribute**, select the same option that you have chosen in the **Token configuration** blade.
9. Select **Save**.

### Test group claims in the application

Clean and build the solution. Run the Azure AD integrated `WebApp_SAML` project, sign-in a user, and should be able see that the user's groups listed in the claims:

For each group that the user belongs to, a claim for will be displayed with the chosen **Source attribute** as its value.

![GroupClaims](./ReadmeFiles/groupClaim.png)

## Use Directory Extensions in Dynamic Groups

One of the most useful scenario in dynamic groups is the usage of Directory Extensions to dynamically associate users to it.

If you would like to configure dynamic groups, [please follow this tutorial](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-feature-directory-extensions#use-the-attributes-in-dynamic-groups).

### Useful resources

- [Moving application authentication from AD FS to Azure Active Directory](https://docs.microsoft.com/azure/active-directory/manage-apps/migrate-adfs-apps-to-azure)
- [Configure SAML-based single sign-on to non-gallery applications](https://docs.microsoft.com/azure/active-directory/manage-apps/configure-single-sign-on-non-gallery-applications)
- [Synchronizing Directory Extensions to Azure AD](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-feature-directory-extensions)
- [Using Directory Extensions in Dynamic groups](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-feature-directory-extensions#use-the-attributes-in-dynamic-groups)
