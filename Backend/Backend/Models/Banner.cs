using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models
{
    public class Banner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull]
        public string Id { get; set; }
        public string Html { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public Banner()
        {
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }
    }

}