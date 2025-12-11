using ClinicAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private static readonly List<Patient> Patients = new List<Patient>();
    private static int _nextId = 1;

    // CREATE a new patient
    // POST: api/patient
    [HttpPost]
    public IActionResult Create(Patient patient)
    {
        patient.Id = _nextId++;
        Patients.Add(patient);
        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
    }

    // READ all patients
    // GET: api/patient
    [HttpGet]
    public IActionResult GetAll() => Ok(Patients);

    // READ a patient by Id
    // GET: api/patient/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var patient = Patients.FirstOrDefault(p => p.Id == id);
        return patient == null ? NotFound() : Ok(patient);
    }

    // UPDATE a patient
    // PUT: api/patient/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, Patient updated)
    {
        var patient = Patients.FirstOrDefault(p => p.Id == id);
        if (patient == null) return NotFound();

        patient.Name = updated.Name;
        patient.Address = updated.Address;
        patient.Birthdate = updated.Birthdate;
        patient.Race = updated.Race;
        patient.Gender = updated.Gender;

        return NoContent();
    }

    // DELETE a patient
    // DELETE: api/patient/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var patient = Patients.FirstOrDefault(p => p.Id == id);
        if (patient == null) return NotFound();

        Patients.Remove(patient);
        return NoContent();
    }

    // SEARCH patients by query (name, gender, or race)
    // GET: api/patient/search?name=John&gender=Male
    [HttpGet("search")]
    public IActionResult Search([FromQuery] string? name, [FromQuery] string? gender, [FromQuery] string? race)
    {
        var results = Patients.AsQueryable();

        if (!string.IsNullOrEmpty(name))
            results = results.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(gender))
            results = results.Where(p => p.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(race))
            results = results.Where(p => p.Race.Equals(race, StringComparison.OrdinalIgnoreCase));

        return Ok(results.ToList());
    }
}
