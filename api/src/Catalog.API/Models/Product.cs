using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Catalog.API.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
