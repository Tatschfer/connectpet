using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PetConnect.Models

{
    public class Pet
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public int Idade { get; set; }
        public string CPF { get; set; }
        public string Cor { get; set; }
    }
}