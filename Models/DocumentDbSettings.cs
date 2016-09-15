using System;
using Microsoft.Extensions.Configuration;

namespace auth0documentdb.Models
{
    public class DocumentDbSettings
    {
        public DocumentDbSettings(IConfiguration configuration)
        {
            try {
                DatabaseName = configuration.GetSection("DatabaseName").Value;
                CollectionName = configuration.GetSection("CollectionName").Value;
                DatabaseUri = new Uri(configuration.GetSection("EndpointUri").Value);
                DatabaseKey = configuration.GetSection("Key").Value;
            }
            catch
            {
                throw new MissingFieldException("IConfiguration missing a valid Azure DocumentDB fields on DocumentDB > [DatabaseName,CollectionName,EndpointUri,Key]");
            }
        }
        public string DatabaseName { get; private set; }
        public string CollectionName { get; private set; }
        public Uri DatabaseUri { get; private set; }
        public string DatabaseKey { get; private set; }
    }
}
