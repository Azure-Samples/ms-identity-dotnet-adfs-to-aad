# Setup Azure AD Connect on AD FS to migrate data to Azure Active Directory

> This sample assumes that you have a working AD FS environment.

## Scenario

You have an AD FS environment and would like to migrate it Azure Active Directory.

## About the sample

This documentation will guide you through the initial steps on how to migrate an AD FS environment to Azure Active Directory, using [Azure AD Connect tool]((https://www.microsoft.com/download/details.aspx?id=47594))

### Pre-requisites

- An Internet connection
- An AD FS environment
- An Azure Active Directory (Azure AD) tenant. For more information on how to get an Azure AD tenant, see [How to get an Azure AD tenant](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/)
- A tenant admin account on Azure Active Directory

### Useful resources

- [Moving application authentication from AD FS to Azure Active Directory](https://docs.microsoft.com/azure/active-directory/manage-apps/migrate-adfs-apps-to-azure)
- [Configure SAML-based single sign-on to non-gallery applications](https://docs.microsoft.com/azure/active-directory/manage-apps/configure-single-sign-on-non-gallery-applications)

## Before you install Azure AD Connect

Please, [kindly read the Azure AD Connect prerequisites](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-install-prerequisites) before installing it. This documentation contains information about hardware and software requirements, security guidelines, installation best practices, and operations not supported by the tool.

## Installing Azure AD Connect

On a domain joined Windows Server 2012 or later, [download and install Azure AD Connect](https://www.microsoft.com/download/details.aspx?id=47594). It is highly recommended that this server is a domain controller.

The installation wizard will walk you through how to configure the tool.

You will be asked to provide details about the Azure Active Directory tenant where you would like to migrate. A tenant admin account is required for this step.

**After the installation is complete, Azure AD Connect will automatically run and data like users and groups from the AD FS environment will be copied to your Azure Active Directory. Please, gently wait this operation to finish before executing extra steps.**

## Additional sync tasks

If you run Azure AD Connect installation wizard again, it offers options for maintenance and additional tasks.

See [additional tasks available](https://docs.microsoft.com/en-us/azure/active-directory/hybrid/how-to-connect-installation-wizard) for details about extra configuration steps.

## Next Step
//TODO: FIX LINKS
- [Optional] If your AD FS has **Directory Extensions**, please [move to the next sample to learn how to migrate them]().
- [Migrate the web app SAML application to Azure Active Directory]().
