
# Moving the AAD application from SAML to OpenIdConnect(OIDC) 

Give links about SAML and OIDC

- Change the code from `app.UseWsFederationAuthentication` to `app.UseOpenIdConnectAuthentication`
- Set metadata to app specific one due to JWT Signing Keys, `https://login.microsoftonline.com/979f4440-75dc-4664-b2e1-2cafa0ac67d1/.well-known/openid-configuration?appid=d3fe55db-31dd-4d85-8d18-06fb7219766f`.