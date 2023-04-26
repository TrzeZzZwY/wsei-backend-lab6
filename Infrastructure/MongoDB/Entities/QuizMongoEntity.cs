using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDB.Entities
{
    public class QuizMongoEntity : BaseMongoEntity
    {
        [BsonElement("id")]
        public int QuizId { get; set; }

        [BsonElement("title")]
        public String Title { get; set; }

        [BsonElement("items")]
        public List<QuizItemMongoEntity> Items { get; set; }
    }
}
