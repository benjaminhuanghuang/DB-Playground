using MongoDB.Bson;

namespace DBPlayground.Models
{
   public interface IIdentified
    {
        ObjectId Id {get;}
    }
}