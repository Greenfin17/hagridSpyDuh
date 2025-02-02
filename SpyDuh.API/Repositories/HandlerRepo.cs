﻿using SpyDuh.API.Models;
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

        internal bool AddSpyToHandler(Handler handler, Spy spy)
        {
            bool returnVal = false;
            var db = new SqlConnection(_connectionString);
            var sql = @"Insert into HandlerSpyRelationship (HandlerId, SpyId)
                        output inserted.*
                        Values (@handlerId, @spyId)";
            var result = db.ExecuteScalar<Guid>(sql, new { handlerId = handler.Id, spyId = spy.Id });
            // test that the row was added
            if (result != new Guid())
            {
                returnVal = true;
            }
            return returnVal;
        }
    }
}
