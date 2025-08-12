using Microsoft.AspNetCore.Mvc;
using PetConnect.Models;
using PetConnect.Services;

namespace PetConnect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectTutorController : ControllerBase
    {
        private readonly PetServiceTutor _petService;
        public ConnectTutorController(PetServiceTutor petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public ActionResult<List<Tutor>> BuscarTutor()
        {
            var tutores = _petService.GetAll();
            return Ok(tutores);
        }

        [HttpGet("{id}")]
        public ActionResult<Tutor> GetTutorById(string id)
        {
            var tutor = _petService.GetById(id);
            if (tutor == null)
                return NotFound();
            return Ok(tutor);
        }

        [HttpPost]
        public IActionResult CadastrarTutor([FromBody] TutorInputDto tutorEntrada)
        {
            var novoTutor = new Tutor
            {
                Nome = tutorEntrada.Nome,
                CPF = tutorEntrada.CPF,
                Idade = tutorEntrada.Idade,
                EnderecoTutor = tutorEntrada.EnderecoTutor,
                TelefoneTutor = tutorEntrada.TelefoneTutor
            };

            var tutorCriado = _petService.Create(novoTutor);
            return CreatedAtAction(nameof(GetTutorById), new { id = tutorCriado.Id }, tutorCriado);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarTutor(string id, [FromBody] TutorUpdateDto tutorAtualizado)
        {
            var tutorExistente = _petService.GetById(id);
            if (tutorExistente == null)
                return NotFound();

            tutorExistente.Nome = tutorAtualizado.Nome;
            tutorExistente.CPF = tutorAtualizado.CPF;
            tutorExistente.Idade = tutorAtualizado.Idade;
            tutorExistente.EnderecoTutor = tutorAtualizado.EnderecoTutor;
            tutorExistente.TelefoneTutor = tutorAtualizado.TelefoneTutor;

            _petService.Update(id, tutorExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarTutor(string id)
        {
            var tutor = _petService.GetById(id);
            if (tutor == null)
                return NotFound();

            _petService.Delete(id);
            return NoContent();
        }
    }
}
