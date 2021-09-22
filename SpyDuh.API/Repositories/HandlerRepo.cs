using SpyDuh.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace SpyDuh.API.Repositories
{
    public class HandlerRepo
    {
        static List<Handler> _handlers = new List<Handler>
        {
        new Handler
            {
                Name = "M",
                Id = new Guid("626c99be-a979-4d56-ba8b-3353e4165145"),
                AgencyName = "M's Agency",
            },
             new Handler
            {
                Name = "Q",
                Id = new Guid("a31e693e-1867-4c94-8445-5d4a76aaaf24"),
                AgencyName = "Q's Agency",
            },
             new Handler
            {
                Name = "Z",
                Id = new Guid("3732f2d5-3291-4494-8470-f6e7f719efde"),
                AgencyName = "Z's Agency",
            }
        };

        readonly string _connectionString;
        public HandlerRepo(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SpyDuh");
        }

        internal IEnumerable<Handler> GetAll()
        {
            return _handlers;
        }

        internal Handler GetHandler(Guid handlerGuid)
        {
            return _handlers.FirstOrDefault(handler => handler.Id == handlerGuid);
        }
        
        internal void Add(Handler newHandler)
        {
            newHandler.Id = Guid.NewGuid();
            _handlers.Add(newHandler);
        }
    }
}
