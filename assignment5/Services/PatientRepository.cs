using System.Text.Json;
using a5.Models;

namespace a5.Services
{
    public class PatientRepository
    {
        private readonly string _filePath = "patients.json";
        private List<Patient> _patients;

        public PatientRepository()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                _patients = JsonSerializer.Deserialize<List<Patient>>(json) ?? new List<Patient>();
            }
            else
            {
                _patients = new List<Patient>();
                SaveChanges();
            }
        }

        public IEnumerable<Patient> GetAll() => _patients;

        public Patient? GetById(int id) =>
            _patients.FirstOrDefault(p => p.Id == id);

        public Patient Add(Patient patient)
        {
            patient.Id = _patients.Count == 0 ? 1 : _patients.Max(p => p.Id) + 1;
            _patients.Add(patient);
            SaveChanges();
            return patient;
        }

        public bool Delete(int id)
        {
            var patient = GetById(id);
            if (patient is null) return false;

            _patients.Remove(patient);
            SaveChanges();
            return true;
        }

        private void SaveChanges()
        {
            string json = JsonSerializer.Serialize(_patients, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }
    }
}
