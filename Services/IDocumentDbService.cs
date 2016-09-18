using System.Linq;
using System.Threading.Tasks;
using auth0documentdb.Models;

namespace auth0documentdb.Services
{
    /// <summary>
    /// DocumentDB service
    /// </summary>
    public interface IDocumentDbService
    {
        Task<PagedResults<auth0documentdb.Models.Db.ContactAddress>> GetContactAddresses(string user,int size = 10, string continuationToken = "");
        Task<string> AddContactAddress(auth0documentdb.Models.Db.ContactAddress address);
        Task UpdateContactAddress(auth0documentdb.Models.Db.ContactAddress address);
        auth0documentdb.Models.Db.NotificationPreferences GetNotificationPreferences(string user);
        Task<string> AddNotificationPreferences(auth0documentdb.Models.Db.NotificationPreferences preferences);
        Task UpdateNotificationPreferences(auth0documentdb.Models.Db.NotificationPreferences preferences);
        Task DeleteContactAddress(string id);
    }
}