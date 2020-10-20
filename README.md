---
page_type: sample
languages:
- csharp
products:
- azure-active-directory
- dotnet
description: "Guidance to help migrate web applications from AD FS to Azure AD"
urlFragment: "ms-identity-dotnet-adfs-to-aad"
---

# AD FS to Azure AD application migration playbook

This set of ASP.NET code samples and accompanying tutorials can help you learn how to safely and securely migrate your applications that are integrated with Active Directory Federation Services (AD FS) to Azure Active Directory (Azure AD).

These code samples are a companion to [Moving application authentication from AD FS to Azure AD](https://docs.microsoft.com/azure/active-directory/manage-apps/migrate-adfs-apps-to-azure) on docs<span>.microsoft<span>.com.

The directories in this repo contain tutorial *chapters*, each with a README and sample code covering a scenario. The chapters cover the most common types of authentication used by applications that are integrated with AD FS today, and with them you can learn and gain experience before undertaking similar steps for your applications currently running in production.

## Prerequisites

- [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework)
- [AD FS](https://docs.microsoft.com/windows-server/identity/ad-fs/ad-fs-overview) environment
- [Azure AD tenant](https://docs.microsoft.com/azure/active-directory/develop/quickstart-create-new-tenant)

## Migration steps

By following the tutorial chapters in this code sample, you progress through the following scenarios:

- Start by integrating a sample web application that uses the [SAML](https://docs.microsoft.com/azure/active-directory/develop/single-sign-on-saml-protocol) protocol with an AD FS instance.
- Next, migrate the application to an Azure AD tenant.
- Finally, we guide you through changing the authentication protocol from SAML to [OAuth 2.0 and OpenID Connect](https://docs.microsoft.com/azure/active-directory/develop/active-directory-v2-protocols) so you can to reap the benefits of accessing rich APIs like [Microsoft Graph](https://docs.microsoft.com/graph/overview) and the [Azure REST API](https://docs.microsoft.com/rest/api/azure/).

Also covered are the following topics as they might play a part in your migration:

- Configuring [Azure AD Connect sync](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-whatis).
- Migrating [Directory Extensions](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-feature-directory-extensions) to Azure AD.
- Sync and use Security Groups on the migrated application.

## Contents

| Chapter                                                                           | Description                                                                                    |
|-----------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------|
| [**1. Integrate app on AD FS**](1-ADFS-Host/1-1-Setup-SAML-Playground/README.md)  | Configure the web application on AD FS.                                                        |
| [1.1 Set up the SAML playground](1-ADFS-Host/1-1-Setup-SAML-Playground/README.md) | Integrate the provided ASP.NET MVC web application to an AD FS instance.                       |
| [1.2 Set up Azure AD Connect](1-ADFS-Host/1-2-Setup-AzureADConnect/README.md)     | Configure Azure AD Connect to synchronize with an Azure AD tenant.                             |
| [1.3 Directory Extensions](1-ADFS-Host/1-3-Directory-Extensions/README.md)        | Migrate Directory Extensions from on-prem Active Directory to your Azure AD tenant.            |
| [**2. Migrate app to Azure AD**](2-AAD-Migration/2-1-SAML-WebApp/README.md)       | Migrate the working web app from AD FS to Azure AD.                                            |
| [2.1 SAML web application](2-AAD-Migration/2-1-SAML-WebApp/README.md)             | Migrating an ASP.NET MVC app that uses SAML protocol from AD FS to Azure AD.                   |
| [2.2 Security groups](2-AAD-Migration/2-2-Security-Groups/README.md)              | Use on-prem Active Directory security groups in applications registered in an Azure AD tenant. |
| [2.3 From SAML to OIDC](2-AAD-Migration/2-3-From-SAML-to-OIDC/README.md)          | Migrate an ASP.NET application that uses the SAML protocol to OpenID Connect (OIDC).           |

For information about Integrated Windows Authentication (IWA), see [Azure-Samples/active-directory-dotnet-iwa-v2](https://github.com/Azure-Samples/active-directory-dotnet-iwa-v2).

## We'd love your feedback!

Were we successful in addressing your learning objective? [Do consider taking a moment to share your experience with us.](https://forms.office.com/Pages/ResponsePage.aspx?id=v4j5cvGGr0GRqy180BHbR73pcsbpbxNJuZCMKN0lURpUODFCRVg4VTk2QUE2VEFPMUZKSEJNUFhWUyQlQCN0PWcu)

We're always listening, and if you want to get in touch with you directly, send an email to <aadappfeedback@microsoft.com>.


## Community Help and Support

Use [Stack Overflow](http://stackoverflow.com/questions/tagged/msal) to get support from the community.

If you find a bug in the sample, raise the issue on [GitHub Issues](../issues).

To provide feedback on or suggest features for Azure Active Directory, visit [User Voice](https://feedback.azure.com/forums/169401-azure-active-directory).

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
