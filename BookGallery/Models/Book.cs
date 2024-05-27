using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookGallery.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
