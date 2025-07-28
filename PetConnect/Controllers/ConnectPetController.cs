using Microsoft.AspNetCore.Mvc;

namespace PetConnect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectPetController : ControllerBase
    {

        [HttpGet("GetPets")]
        public String BuscarPets()
        {
            return "Oi Fernanda";
        }

        [HttpPost("PostPet")]
        public string CadastrarPet([FromBody] Pet petEntrada)
        {
            return petEntrada.Nome+petEntrada.Raca;
        }

    }

    public class Pet
    {
        public string Nome { get; set; }
        public string Raca { get; set; }
        public int Idade { get; set; }
        public string NomeTutor {  get; set; }
        public string Cor {  get; set; }
    }
}
