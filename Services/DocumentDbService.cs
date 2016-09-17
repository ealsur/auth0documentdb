using System.Linq;
using auth0documentdb.Models;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;

namespace auth0documentdb.Services
{
    /// <summary>
    /// DocumentDB service
    /// </summary>
    public class DocumentDbService : IDocumentDbService
    {
        private readonly DocumentDbProvider _provider;
        public DocumentDbService(IConfiguration configuration)
        {
            _provider = new DocumentDbProvider(new DocumentDbSettings(configuration));
        }

        /// <summary>
        /// Builds a query for users
        /// </summary>
        /// <returns></returns>
        public IQueryable<auth0documentdb.Models.Db.User> UserQuery(int size = 10, string continuationToken = "")
        {
            var feedOptions = new FeedOptions() { MaxItemCount = size };
            if (!string.IsNullOrEmpty(continuationToken))
            {
                feedOptions.RequestContinuation = continuationToken;
            }
            return  _provider.CreateQuery<auth0documentdb.Models.Db.User>(feedOptions).Where(x => x.Type == "user");
        }
    }
}
