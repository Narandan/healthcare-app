using System;
using System.Collections.Generic;

namespace ClinicAPI.Models
{
    public class Patient
    {
        public int Id { get; set; } // unique identifier
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public DateTime Birthdate { get; set; }
        public string Race { get; set; } = "";
        public string Gender { get; set; } = "";
        public List<MedicalNote>? MedicalNotes { get; set; } = new List<MedicalNote>();
    }

    public class MedicalNote
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public string? Diagnosis { get; set; }
        public string? Prescription { get; set; }
    }
}
