using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using auth0documentdb.Models;
using Microsoft.Extensions.Configuration;

namespace auth0documentdb.Services
{
    /// <summary>
    /// DocumentDB service
    /// </summary>
    public class DocumentDbService
    {
        private readonly DocumentDbProvider _provider;
        public DocumentDbService(IConfiguration configuration)
        {
            _provider = new DocumentDbProvider(new DocumentDbSettings(configuration));
        }
    }
}
