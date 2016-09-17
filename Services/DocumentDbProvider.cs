using System;
using System.Linq;
using System.Threading.Tasks;
using auth0documentdb.Models;
using Microsoft.Azure.Documents.Client;

namespace auth0documentdb.Services
{
    /// <summary>
    /// DocumentDB provider for wrapping common accessors
    /// </summary>
    public class DocumentDbProvider
    {
        private readonly DocumentDbSettings _settings;
        private readonly Uri _collectionUri;
        private DocumentClient _dbClient;
        public DocumentDbProvider(DocumentDbSettings settings)
        {
            _settings = settings;
            _collectionUri = GetCollectionLink();
            //See https://azure.microsoft.com/documentation/articles/documentdb-performance-tips/ for performance tips
            _dbClient = new DocumentClient(_settings.DatabaseUri, _settings.DatabaseKey, new ConnectionPolicy(){
                MaxConnectionLimit = 100
            });
            _dbClient.OpenAsync().Wait();
        }
        #region Private
        /// <summary>
        /// Obtains the link of a collection
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        private  Uri GetCollectionLink()
        {
            return UriFactory.CreateDocumentCollectionUri(_settings.DatabaseName, _settings.CollectionName);
        }

        /// <summary>
        /// Obtains the link for a document
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Uri GetDocumentLink(string id)
        {
            return UriFactory.CreateDocumentUri(_settings.DatabaseName, _settings.CollectionName, id);
        }
        #endregion

        #region Public
        /// <summary>
        /// Creates a Query with FeedOptions
        /// </summary>
        /// <typeparam name="T">Type of Class to serialize</typeparam>
        /// <param name="feedOptions"></param>
        /// <returns></returns>
        public IQueryable<T> CreateQuery<T>(FeedOptions feedOptions)
        {
            return _dbClient.CreateDocumentQuery<T>(_collectionUri, feedOptions);
        }

        /// <summary>
        /// Creates a Query with FeedOptions and a SQL expression
        /// </summary>
        /// <typeparam name="T">Type of Class to serialize</typeparam>
        /// <param name="sqlExpression">SQL query</param>
        /// <param name="feedOptions"></param>
        /// <returns></returns>
        public IQueryable<T> CreateQuery<T>(string sqlExpression, FeedOptions feedOptions)
        {
            return _dbClient.CreateDocumentQuery<T>(_collectionUri, sqlExpression, feedOptions);
        }

        /// <summary>
        /// Adds an item to a collection
        /// </summary>
        /// <typeparam name="T">Type of Class to serialize</typeparam>
        /// <param name="document">Document to add</param>
        /// <returns></returns>
        public async Task<string> AddItem<T>(T document)
        {
            var result = await _dbClient.CreateDocumentAsync(_collectionUri, document);
            return result.Resource.Id;
        }

        /// <summary>
        /// Updates a document on a collection
        /// </summary>
        /// <typeparam name="T">Type of Class to serialize</typeparam>
        /// <param name="document">Document to add</param>
        /// <param name="id">Id of the document to update</param>
        /// <returns></returns>
        public async Task<string> UpdateItem<T>(T document, string id)
        {
            var result = await _dbClient.ReplaceDocumentAsync(GetDocumentLink(id), document);
            return result.Resource.Id;
        }

        /// <summary>
        /// Deletes a document from a collection
        /// </summary>
        /// <param name="id">Id of the document to delete</param>
        /// <returns></returns>
        public async Task<string> DeleteItem(string id)
        {
            var result = await _dbClient.DeleteDocumentAsync(GetDocumentLink(id));
            return result.Resource.Id;
        }
                
        #endregion
    }
}
