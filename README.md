# Using Azure DocumentDB with Auth0

This repository acts as sample in a scenario where you want to use Azure DocumentDB as your database and integrate Auth0's authentication service by a custom database scenario.

It includes an ASP.NET Core Web application with a [appsettings.json](https://github.com/ealsur/auth0documentdb/blob/master/appsettings.json) file you need to configure with your account settings:
```javascript
    {
     "Auth0": {
        "Domain": "{YOUR_AUTH0_DOMAIN}",
        "ClientId": "{YOUR_AUTH0_CLIENT_ID}",
        "ClientSecret": "{YOUR_AUTH0_CLIENT_SECRET}",
        "CallbackUrl": "{YOUR_CALLBACK_URL}"
      },
      "DocumentDb":{
        "EndpointUri":"{YOUR_DOCUMENTDB_ENDPOINT_URL}",
        "Key":"{YOUR_DOCUMENTDB_PASSWORD}",
        "DatabaseName":"{YOUR_EXISTENT_DOCUMENTDB_DATABASE_NAME}",
        "CollectionName":"{YOUR_EXISTENT_DOCUMENTDB_COLLECTION_NAME}"
      }
    }
```
> Due to [Azure DocumentDB's SDK](https://www.nuget.org/packages/Microsoft.Azure.DocumentDB) restrictions, even though this is an ASP.NET Core application, it runs on Full Framework mode.
