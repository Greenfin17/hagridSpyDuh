using SpyDuh.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.API.Repositories
{
    public class HandlerRepo
    {
        static List<Handler> _handlers = new List<Handler>
        {
        new Handler
            {
                Name = "John Doe",
                Id = Guid.NewGuid(),
                AgencyName = "John Doe's Agency",
            },
             new Handler
            {
                Name = "Jane Doe",
                Id = Guid.NewGuid(),
                AgencyName = "Jane Doe's Agency",
            },
             new Handler
            {
                Name = " Second John Doe",
                Id = Guid.NewGuid(),
                AgencyName = "Second John Doe's Agency",
            }
        };

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
