using MongoDB.Driver;
using PetConnect.Models;

namespace PetConnect.Services
{
    public class PetServiceTutor
    {
        private readonly IMongoCollection<Tutor> _petCollection;

        public PetServiceTutor(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("PetConnectDb");
            _petCollection = database.GetCollection<Tutor>("Tutores");
        }

        public List<Tutor> GetAll() => _petCollection.Find(_ => true).ToList();

        public Tutor Create(Tutor tutor)
        {
            _petCollection.InsertOne(tutor);
            return tutor;
        }

        public Tutor GetById(string id)
        {
            return _petCollection.Find(p => p.Id == id).FirstOrDefault();
        }

        public void Delete(string id)
        {
            _petCollection.DeleteOne(p => p.Id == id);
        }

        public void Update(string id, Tutor tutor)
        {
            _petCollection.ReplaceOne(p => p.Id == id, tutor);
        }
    }

}
