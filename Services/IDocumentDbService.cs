using System.Linq;
using System.Threading.Tasks;

namespace auth0documentdb.Services
{
    /// <summary>
    /// DocumentDB service
    /// </summary>
    public interface IDocumentDbService
    {
        Task<IQueryable<auth0documentdb.Models.Db.User>> UserQuery(int size = 10, string continuationToken = "");
    }
}