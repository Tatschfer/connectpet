using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PetConnect.Models

{
    public class Tutor
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int Idade { get; set; }
        public string EnderecoTutor { get; set; }
        public string TelefoneTutor { get; set; }
    }
}