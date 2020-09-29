---
page_type: sample
languages:
- csharp
products:
- dotnet
description: "Guidance to help migrate web applications from ADFS to Azure AD"
urlFragment: "ms-identity-dotnet-adfs-to-aad"
---

# ADFS to Azure AD application migration playbook

## About this sample

This set of tutorials and guides will help you learn how to safely and securely migrate your applications integrated with Active Directory Federation Services (ADFS) to Azure Active Directory.

These code samples are an extension to the official Microsoft guide,[Moving application authentication from Active Directory Federation Services to Azure Active Directory](https://docs.microsoft.com/azure/active-directory/manage-apps/migrate-adfs-apps-to-azure).

In the following chapters, we cover the most common types of authentication used by application integrated with ADFS today. We hope that you can use these examples to learn and get experienced before undertaking the same steps with your in-production applications.

### Overview

- We'd start with integrating a sample web application that uses the [SAML](https://docs.microsoft.com/azure/active-directory/develop/single-sign-on-saml-protocol) protocol to an ADFS instance.
- We'd then migrate this application to an Azure AD tenant.
- We'd additionally also guide you on how to change the authentication protocol from SAML to [OAuth 2\.0 and OpenID Connect protocols](https://docs.microsoft.com/azure/active-directory/develop/active-directory-v2-protocols) in case you wish to reap the benefits of being able to access rich API like [Microsoft Graph](https://docs.microsoft.com/graph/overview) and the [Azure REST API](https://docs.microsoft.com/rest/api/azure/).

Additionally, we'd also cover the following topics in some detail as they might play a part in your plans:

- Configuring [Azure AD Connect sync](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-whatis)
- Migrating [Directory Extensions](https://docs.microsoft.com/azure/active-directory/hybrid/how-to-connect-sync-feature-directory-extensions) to Azure AD.
- Sync and use Security Groups on the migrated application

### Chapters

The following chapters are available:
| Chapter                   | Description                                |
|---------------------------|--------------------------------------------|
| [1-Integrate app on ADFS](1-ADFS-Host/1-1-Setup-SAML-Playground/README.md) | Covers configuration of the web application on ADFS.
| [2-Migrate app to Azure AD](2-AAD-Migration\2-1-SAML-WebApp\README.md) | Migrate the working web app from ADFS to Azure AD.

>NOTE: For Integrated Windows Authentication (IWA) guide, please check [this sample](https://github.com/Azure-Samples/active-directory-dotnet-iwa-v2).

## Contents

| File/folder       | Description                                |
|-------------------|--------------------------------------------|
| `CONTRIBUTING.md` | Guidelines for contributing to the sample. |
| `README.md`       | This README file.                          |
| `LICENSE`         | The license for the sample.                |

## Setup

### Prerequisites

- .NET Framework 4.7.2
- An ADFS environment
- An Azure Active Directory (Azure AD) tenant. For more information on how to get an Azure AD tenant, see [How to get an Azure AD tenant](https://azure.microsoft.com/documentation/articles/active-directory-howto-tenant/)
- An Azure AD tenant admin account

## Community Help and Support

Use [Stack Overflow](http://stackoverflow.com/questions/tagged/msal) to get support from the community.

If you find a bug in the sample, please raise the issue on [GitHub Issues](../issues).

To provide a recommendation, visit the following [User Voice page](https://feedback.azure.com/forums/169401-azure-active-directory).

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
