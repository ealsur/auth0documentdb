using System.Linq;

namespace auth0documentdb.Services
{
    /// <summary>
    /// DocumentDB service
    /// </summary>
    public interface IDocumentDbService
    {
        IQueryable<auth0documentdb.Models.Db.User> UserQuery(int size = 10, string continuationToken = "");
    }
}