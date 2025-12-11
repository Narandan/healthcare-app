using Microsoft.AspNetCore.Mvc;
using ClinicAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// In-memory storage for demo purposes
var patients = new List<Patient>();
int nextId = 1; // auto-incrementing Id

// -------------------- PATIENT ENDPOINTS --------------------

// GET all patients
app.MapGet("/api/patient", () =>
{
    return Results.Ok(patients);
});

// GET a patient by Id
app.MapGet("/api/patient/{id:int}", (int id) =>
{
    var patient = patients.FirstOrDefault(p => p.Id == id);
    return patient == null ? Results.NotFound("Patient not found.") : Results.Ok(patient);
});

// POST a new patient
app.MapPost("/api/patient", (Patient newPatient) =>
{
    newPatient.Id = nextId++;
    patients.Add(newPatient);
    return Results.Created($"/api/patient/{newPatient.Id}", newPatient);
});

// PUT to update a patient by Id
app.MapPut("/api/patient/{id:int}", (int id, Patient updatedPatient) =>
{
    var patient = patients.FirstOrDefault(p => p.Id == id);
    if (patient == null)
        return Results.NotFound("Patient not found.");

    // update fields
    patient.Name = updatedPatient.Name;
    patient.Address = updatedPatient.Address;
    patient.Birthdate = updatedPatient.Birthdate;
    patient.Race = updatedPatient.Race;
    patient.Gender = updatedPatient.Gender;
    patient.MedicalNotes = updatedPatient.MedicalNotes;

    return Results.Ok(patient);
});

// DELETE a patient by Id
app.MapDelete("/api/patient/{id:int}", (int id) =>
{
    var patient = patients.FirstOrDefault(p => p.Id == id);
    if (patient == null)
        return Results.NotFound("Patient not found.");

    patients.Remove(patient);
    return Results.Ok($"Patient {id} deleted.");
});

app.Run();
