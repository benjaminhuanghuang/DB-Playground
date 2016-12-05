using System.Collections.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

using DBPlayground.Models;

namespace DBPlayground
{
    public static class Extensions
    {
        public static async Task<ReplaceOneResult> SaveAsync<T>(this IMongoCollection<T> coll, T entity) where T : IIdentified
        {
            return await coll.ReplaceOneAsync(i => i.Id == entity.Id, entity,
                           new UpdateOptions { IsUpsert = true }); //insert if entity no exist
        }
    }
}