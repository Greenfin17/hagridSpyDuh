using SpyDuh.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpyDuh.API.Repositories;

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
                Id = new Guid("ee8467a1-971a-4b4a-8af1-cd2ae5a7f197"),
                AgencyName = "Q's Agency",
            },
             new Handler
            {
                Name = "Z",
                Id = new Guid("3732f2d5-3291-4494-8470-f6e7f719efde"),
                AgencyName = "Z's Agency",
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


        internal IEnumerable<Spy> GetSpiesByHandler(Guid handlerGuid)
        {
            var handlerObj = _handlers.FirstOrDefault(handler => handler.Id == handlerGuid);
            var spyGuids = new List<Guid>();
            var spyList = new List<Spy>();
            if (handlerObj != null)
            {
                foreach(var spy in _spies)
                {
                    if (spy.Handlers.Contains(handlerGuid))
                    {
                        foreach(var handler in spy.Handlers)
                        {
                            spyGuids.Add(handler);
                        }
                    }
                }
                if (spyGuids.Count > 0)
                {
                    Spy tempSpy;
                    foreach (var guid in spyGuids)
                    {
                        tempSpy = _spies.GetSpy(guid);
                        if (tempSpy != null)
                        {
                            spyList.Add(tempSpy);
                        }
                    }
                }
            }
            return spyList;
        }

    }
}
