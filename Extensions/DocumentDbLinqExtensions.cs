using System.Linq;
using System.Threading.Tasks;
using auth0documentdb.Models;
using Microsoft.Azure.Documents.Linq;

namespace auth0documentdb.Extensions
{
    public static class DocumentDBLinqExtensions
    {
        /// <summary>
        /// Gets the first result
        /// </summary>
        /// <typeparam name="T">Type of the Class</typeparam>
        /// <param name="source">Queryable to take one from</param>
        /// <returns></returns>
        public static T TakeOne<T>(this IQueryable<T> source)
        {
            var documentQuery = source.AsDocumentQuery();            
            if (documentQuery.HasMoreResults)
			{
				var queryResult = documentQuery.ExecuteNextAsync<T>().Result;
				if (queryResult.Any())
				{
					return queryResult.Single<T>();
				}
			}
			return default(T);
        }

        /// <summary>
        /// Creates a pagination wrapper with Continuation Token support
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static async Task<PagedResults<T>> ToPagedResults<T>(this IQueryable<T> source)
        {
            var documentQuery = source.AsDocumentQuery();
            var results = new PagedResults<T>();

            try
            {
                var queryResult = await documentQuery.ExecuteNextAsync<T>();
                if (!queryResult.Any())
                {
                    return results;
                }
                results.ContinuationToken = queryResult.ResponseContinuation;
                results.Results.AddRange(queryResult);
            }
            catch
            {
                //documentQuery.ExecuteNextAsync throws an Exception if there are no results
                return results;
            }

            return results;
        }
    }
}
