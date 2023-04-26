using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDB.Entities
{
    public class QuizItemUserAnswerEntity
    {
        [BsonElement("id")]
        public int AnswerId { get; set; }

        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("QuizItemId")]
        public int QuizItemId { get; set; }

        [BsonElement("QuizId")]
        public int QuizId { get; set; }
        [BsonElement("UserAnswer")]
        public string UserAnswer { get; set; }

    }
}
