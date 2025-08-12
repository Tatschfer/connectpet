using MongoDB.Driver;
using PetConnect.Models;

namespace PetConnect.Services
{
    public class PetServicePet
    {
        private readonly IMongoCollection<Pet> _petCollection;

        public PetServicePet(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("PetConnectDb");
            _petCollection = database.GetCollection<Pet>("Pets");
        }

        public List<Pet> GetAll() => _petCollection.Find(_ => true).ToList();

        public Pet Create(Pet pet)
        {
            _petCollection.InsertOne(pet); 
            return pet;
        }

        public Pet GetById(string id)
        {
            return _petCollection.Find(p => p.Id == id).FirstOrDefault();
        }

        public void Delete(string id)
        {
            _petCollection.DeleteOne(p => p.Id == id);
        }

        public void Update(string id, Pet pet)
        {
            _petCollection.ReplaceOne(p => p.Id == id, pet);
        }
    }

}
