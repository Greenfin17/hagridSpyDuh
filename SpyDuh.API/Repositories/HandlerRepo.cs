using SpyDuh.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;

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
            var db = new SqlConnection(_connectionString);
            var sql = @"Select * from Handler";
            var handlers = db.Query<Handler>(sql);
            return handlers;
        }

        internal Handler GetHandlerById(Guid handlerGuid)
        {

            var db = new SqlConnection(_connectionString);
            var sql = @"Select * from Handler
                        Where Id = @handlerGuid";
            var handler = db.QueryFirstOrDefault<Handler>(sql, new { handlerGuid });
            return handler;
        }
        
        internal bool Add(Handler newHandler)
        {
            bool returnVal = false;
            var origId = newHandler.Id;
            var db = new SqlConnection(_connectionString);
            var sql = @"Insert into Handler ( Name, AgencyName )
                        output inserted.*
	                    Values(@Name, @AgencyName)";

            var result = db.QueryFirstOrDefault<Handler>(sql, newHandler);
            if ( result != null)
            {
                newHandler.Id = result.Id;
                returnVal = true;
            }
            return returnVal;
        }
    }
}
