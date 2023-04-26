using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDB
{
    public class MongoDBSettings
    {
        public string ConnectionUri { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string QuizCollection { get; set; } = null!;
        public string QuizAnswersCollection { get; set; } = null!;
    }
}
