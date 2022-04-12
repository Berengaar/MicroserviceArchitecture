using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]     // Automatical string -- _id: ObjectId("5099803df3f4948bd2f98391")
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
