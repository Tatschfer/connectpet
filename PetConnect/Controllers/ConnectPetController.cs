using Microsoft.AspNetCore.Mvc;
using PetConnect.Models;
using PetConnect.Services;

namespace PetConnect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectPetController : ControllerBase
    {
        private readonly PetServicePet _petService;

        public ConnectPetController(PetServicePet petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public ActionResult<List<Pet>> BuscarPets()
        {
            var pets = _petService.GetAll();
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public ActionResult<Pet> GetPetById(string id)
        {
            var pet = _petService.GetById(id);
            if (pet == null)
                return NotFound();
            return Ok(pet);
        }

        [HttpPost]
        public IActionResult CadastrarPet([FromBody] PetInputDto petEntrada)
        {
            var novoPet = new Pet
            {
                Nome = petEntrada.Nome,
                Raca = petEntrada.Raca,
                Idade = petEntrada.Idade,
                CPF = petEntrada.CPF,
                Cor = petEntrada.Cor
            };

            var petCriado = _petService.Create(novoPet);
            return CreatedAtAction(nameof(GetPetById), new { id = petCriado.Id }, petCriado);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarPet(string id, [FromBody] PetUpdateDto petAtualizado)
        {
            var petExistente = _petService.GetById(id);
            if (petExistente == null)
                return NotFound();

            petExistente.Nome = petAtualizado.Nome;
            petExistente.Raca = petAtualizado.Raca;
            petExistente.Idade = petAtualizado.Idade;
            petExistente.CPF = petAtualizado.CPF;
            petExistente.Cor = petAtualizado.Cor;

            _petService.Update(id, petExistente);
            return NoContent();
        }

            [HttpDelete("{id}")]
            public IActionResult DeletarPet(string id)
            {
                var pet = _petService.GetById(id);
                if (pet == null)
                    return NotFound();

                _petService.Delete(id);
                return NoContent();
            }
        }
}
