# Configure Azure AD Connect to synchronize with an Azure Active Directory tenant

## Scenario

This page lists the steps to ensure that relevant bits of information are being synchronized from the on-premise Active directory to its connected Azure Active Directory tenant.

## About the page

In most organizations, the AD Connect sync tool should already installed and configured. So we advise you follow the installation steps here only on a test instance of an Active Directory domain environment.

If its already installed, skip to [Post-Installation steps](#post-installation-steps).

### Pre-requisites

- An on-premise Active Directory environment
- An Azure Active Directory (Azure AD) tenant. For more information on how to get an Azure AD tenant, see [How to get an Azure AD tenant](https://azure.microsoft.com/documentation/articles/active-directory-howto-tenant/)
- A tenant admin account on Azure Active Directory

This document], [Azure AD Connect tool]((https://www.microsoft.com/download/details.aspx?id=47594)) will guide you through the initial steps on how to synchronize an ADFS environment with an Azure Active Directory tenant.

## Before you install Azure AD Connect

Please, [kindly read the Azure AD Connect prerequisites](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-install-prerequisites) before installing it. This documentation contains information about hardware and software requirements, security guidelines, installation best practices, and operations not supported by the tool.

## Installing Azure AD Connect

On a domain joined Windows Server 2012 or later, [download and install Azure AD Connect](https://www.microsoft.com/download/details.aspx?id=47594). It is highly recommended that this server is a domain controller.

The installation wizard will walk you through the tool configuration.

You will be asked to provide details about the Azure Active Directory tenant where you would like to migrate. A tenant admin account is required for this step.

## Post-Installation steps

After installation and configuration is complete, Azure AD Connect will automatically run and synchronize **users** and **groups** from the on-premise Active Directory environment to your Azure Active Directory tenant.

> Please wait this operation to finish before executing extra steps. You should be able to confirm that synchronization succeeded by signing-into the Azure portal and check if all users and groups are available in the Azure AD tenant.

Use [Graph Explorer](https://aka.ms/ge) to carefully examine the attributes of **users** and **groups** to ensure that all required attributes were synced.

## Additional sync tasks

If you run Azure AD Connect installation wizard again, it offers options for maintenance and set additional tasks.

See [additional tasks available](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-installation-wizard) for details about extra configuration steps.

## Next Step

- [Optional] If your on premise Active Directory have **Directory Extensions** that you'd like to synchronize to the Azure AD tenant, please [move to the next chapter to learn how to migrate them](../1-3-Directory-Extensions/README.md).
- [Migrate this .NET MVC application to an Azure Active Directory tenant](./../2-AAD-Migration/2-1-SAML-WebApp/README.md)

## Useful resources

- Learn how to [migrate the web app SAML application to Azure Active Directory](https://docs.microsoft.com/azure/active-directory/manage-apps/migrate-adfs-apps-to-azure).
