using Microsoft.AspNetCore.Mvc;
using a5.Models;
using a5.Services;

namespace a5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientRepository _repo;

        public PatientController(PatientRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repo.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var patient = _repo.GetById(id);
            if (patient is null) return NotFound();
            return Ok(patient);
        }

        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            var created = _repo.Add(patient);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool removed = _repo.Delete(id);
            if (!removed) return NotFound();
            return NoContent();
        }
    }
}
