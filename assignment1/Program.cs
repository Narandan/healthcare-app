using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Main_Menu menu = new Main_Menu();
        menu.Run();
    }
}

// -------------------- DATA CLASSES --------------------
public class Patient
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public DateTime Birthdate { get; set; }
    public required string Race { get; set; }
    public required string Gender { get; set; }
    public List<MedicalNote> MedicalNotes { get; } = new List<MedicalNote>();
}

public class Physician
{
    public required string Name { get; set; }
    public required string LicenseNumber { get; set; }
    public DateTime GraduationDate { get; set; }
    public required string Specialization { get; set; }
}

public class Appointment
{
    public Patient Patient { get; set; }
    public Physician Physician { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime => StartTime.AddMinutes(90); // 1.5 hours
}

public class MedicalNote
{
    public DateTime Date { get; set; } = DateTime.Now;
    public string Diagnosis { get; set; } = "";
    public string Prescription { get; set; } = "";
}

// -------------------- GLOBAL DATA STORE --------------------
public static class DataStore
{
    public static List<Patient> Patients { get; } = new List<Patient>();
    public static List<Physician> Physicians { get; } = new List<Physician>();
    public static List<Appointment> Appointments { get; } = new List<Appointment>();
}

// -------------------- MENUS --------------------
public class Patient_Menu
{
    public void CreatePatient()
    {
        string name = InputHelper.ReadName("Enter patient name:");
        string address = InputHelper.ReadNonEmpty("Enter patient address:");
        DateTime birthdate = InputHelper.ReadDate("Enter birthdate (YYYY-MM-DD):");

        string race = InputHelper.SelectOption("Select race:", new string[]
        {
            "White", "Black or African American", "Asian", "Hispanic or Latino", "Other"
        });

        string gender = InputHelper.SelectOption("Select gender:", new string[]
        {
            "Male", "Female", "Other"
        });

        Patient patient = new Patient
        {
            Name = name,
            Address = address,
            Birthdate = birthdate,
            Race = race,
            Gender = gender
        };

        DataStore.Patients.Add(patient);
        Console.WriteLine($"Patient '{name}' added successfully.");
    }
}

public class Physician_Menu
{
    public void CreatePhysician()
    {
        string name = InputHelper.ReadName("Enter physician name:");
        string license = InputHelper.ReadNonEmpty("Enter license number:");
        DateTime gradDate = InputHelper.ReadDate("Enter graduation date (YYYY-MM-DD):");
        string specialization = InputHelper.ReadNonEmpty("Enter specialization:");

        Physician physician = new Physician
        {
            Name = name,
            LicenseNumber = license,
            GraduationDate = gradDate,
            Specialization = specialization
        };

        DataStore.Physicians.Add(physician);
        Console.WriteLine($"Physician '{name}' added successfully.");
    }
}

public class Appointment_Menu
{
    public void CreateAppointment()
    {
        if (!DataStore.Patients.Any() || !DataStore.Physicians.Any())
        {
            Console.WriteLine("You must have at least one patient and one physician to create an appointment.");
            return;
        }

        Patient patient = InputHelper.ChoosePatient();
        Physician physician = InputHelper.ChoosePhysician();

        // Year selection (current or next)
        int currentYear = DateTime.Now.Year;
        string yearChoice = InputHelper.SelectOption("Select year:", new string[] { currentYear.ToString(), (currentYear + 1).ToString() });
        int year = (yearChoice == "1") ? currentYear : currentYear + 1;

        // Month selection
        int month = InputHelper.SelectMonth(year);

        // Day selection
        int day = InputHelper.ReadDay(year, month);

        // Time slots
        TimeSpan[] timeSlots = new TimeSpan[]
        {
            new TimeSpan(8,0,0),
            new TimeSpan(9,0,0),
            new TimeSpan(10,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(12,0,0),
            new TimeSpan(13,0,0),
            new TimeSpan(14,0,0),
            new TimeSpan(15,0,0),
        };

        string[] timeLabels = new string[]
        {
            "8:00 AM - 9:30 AM",
            "9:00 AM - 10:30 AM",
            "10:00 AM - 11:30 AM",
            "11:00 AM - 12:30 PM",
            "12:00 PM - 1:30 PM",
            "1:00 PM - 2:30 PM",
            "2:00 PM - 3:30 PM",
            "3:00 PM - 4:30 PM",
        };

        int slot = InputHelper.SelectTimeSlot(patient, physician, year, month, day, timeSlots, timeLabels);

        DateTime start = new DateTime(year, month, day, timeSlots[slot].Hours, timeSlots[slot].Minutes, 0);

        Appointment appt = new Appointment
        {
            Patient = patient,
            Physician = physician,
            StartTime = start
        };

        DataStore.Appointments.Add(appt);

        Console.WriteLine($"Appointment created for {patient.Name} with Dr. {physician.Name} on {appt.StartTime:MM/dd/yyyy h:mm tt}");
    }
}

public class MedicalNote_Menu
{
    public void ManageNotes()
    {
        if (!DataStore.Patients.Any())
        {
            Console.WriteLine("No patients available.");
            return;
        }

        Patient patient = InputHelper.ChoosePatient();

        Console.WriteLine("\n--- Medical Notes Menu ---");
        Console.WriteLine("1. Add Note");
        Console.WriteLine("2. View Notes");

        string choice = Console.ReadLine() ?? "";

        if (choice == "1")
        {
            string diagnosis = InputHelper.ReadNonEmpty("Enter diagnosis:");
            string prescription = InputHelper.ReadNonEmpty("Enter prescription:");

            MedicalNote note = new MedicalNote
            {
                Diagnosis = diagnosis,
                Prescription = prescription
            };

            patient.MedicalNotes.Add(note);
            Console.WriteLine("Note added successfully.");
        }
        else if (choice == "2")
        {
            if (!patient.MedicalNotes.Any())
            {
                Console.WriteLine("No notes available for this patient.");
                return;
            }

            Console.WriteLine($"\nMedical notes for {patient.Name}:");
            foreach (var note in patient.MedicalNotes)
            {
                Console.WriteLine($"{note.Date:MM/dd/yyyy} - Diagnosis: {note.Diagnosis}, Prescription: {note.Prescription}");
            }
        }
    }
}

// -------------------- HELPER METHODS --------------------
public static class InputHelper
{
    public static string ReadName(string prompt)
    {
        string input;
        do
        {
            Console.WriteLine(prompt);
            input = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(input)) Console.WriteLine("Invalid input. Try again.");
        } while (string.IsNullOrWhiteSpace(input));
        return input.Trim();
    }

    public static string ReadNonEmpty(string prompt)
    {
        string input;
        do
        {
            Console.WriteLine(prompt);
            input = Console.ReadLine() ?? "";
        } while (string.IsNullOrWhiteSpace(input));
        return input.Trim();
    }

    public static DateTime ReadDate(string prompt)
    {
        DateTime date;
        do
        {
            Console.WriteLine(prompt);
        } while (!DateTime.TryParse(Console.ReadLine(), out date));
        return date;
    }

    public static string SelectOption(string prompt, string[] options)
    {
        int choice = 0;
        do
        {
            Console.WriteLine(prompt);
            for (int i = 0; i < options.Length; i++)
                Console.WriteLine($"{i + 1}. {options[i]}");
            string input = Console.ReadLine();
            if (int.TryParse(input, out choice) && choice >= 1 && choice <= options.Length)
                break;
            Console.WriteLine("Invalid selection. Try again.");
        } while (true);
        return choice.ToString();
    }

    public static Patient ChoosePatient()
    {
        Console.WriteLine("Select patient:");
        for (int i = 0; i < DataStore.Patients.Count; i++)
            Console.WriteLine($"{i + 1}. {DataStore.Patients[i].Name}");
        int choice;
        do
        {
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= DataStore.Patients.Count)
                break;
            Console.WriteLine("Invalid selection. Try again.");
        } while (true);
        return DataStore.Patients[choice - 1];
    }

    public static Physician ChoosePhysician()
    {
        Console.WriteLine("Select physician:");
        for (int i = 0; i < DataStore.Physicians.Count; i++)
            Console.WriteLine($"{i + 1}. {DataStore.Physicians[i].Name}");
        int choice;
        do
        {
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= DataStore.Physicians.Count)
                break;
            Console.WriteLine("Invalid selection. Try again.");
        } while (true);
        return DataStore.Physicians[choice - 1];
    }

    public static int SelectMonth(int year)
    {
        int month = 0;
        int currentYear = DateTime.Now.Year;
        int currentMonth = DateTime.Now.Month;
        string[] monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        do
        {
            Console.WriteLine("Select month:");
            for (int i = 0; i < 12; i++)
                Console.WriteLine($"{i + 1}. {monthNames[i]}");
            string input = Console.ReadLine();
            if (int.TryParse(input, out month) && month >= 1 && month <= 12)
            {
                if (year == currentYear && month < currentMonth)
                {
                    Console.WriteLine("Invalid month selection (date has already passed).");
                    continue;
                }
                break;
            }
            Console.WriteLine("Invalid selection. Try again.");
        } while (true);
        return month;
    }

    public static int ReadDay(int year, int month)
    {
        int day;
        int maxDay = DateTime.DaysInMonth(year, month);
        do
        {
            Console.WriteLine("Enter day:");
            if (int.TryParse(Console.ReadLine(), out day) && day >= 1 && day <= maxDay)
            {
                DateTime date = new DateTime(year, month, day);
                if (date < DateTime.Now.Date)
                {
                    Console.WriteLine("Date has already passed. Choose again.");
                    continue;
                }
                break;
            }
            Console.WriteLine("Invalid day. Try again.");
        } while (true);
        return day;
    }

    public static int SelectTimeSlot(Patient patient, Physician physician, int year, int month, int day, TimeSpan[] timeSlots, string[] labels)
    {
        int slot = -1;
        do
        {
            Console.WriteLine("Select time:");
            for (int i = 0; i < labels.Length; i++)
                Console.WriteLine($"{i + 1}. {labels[i]}");

            string input = Console.ReadLine();
            if (int.TryParse(input, out slot) && slot >= 1 && slot <= labels.Length)
            {
                DateTime start = new DateTime(year, month, day, timeSlots[slot - 1].Hours, timeSlots[slot - 1].Minutes, 0);
                DateTime end = start.AddMinutes(90);

                bool conflict = DataStore.Appointments.Any(a =>
                    (a.Physician == physician || a.Patient == patient) &&
                    ((start < a.EndTime && end > a.StartTime))
                );

                if (conflict)
                {
                    Console.WriteLine("Selected time conflicts with an existing appointment. Choose again.");
                    continue;
                }

                break;
            }
            Console.WriteLine("Invalid selection. Try again.");
        } while (true);
        return slot - 1;
    }
}

// -------------------- MAIN MENU --------------------
public class Main_Menu
{
    public void Run()
    {
        string choice = "";
        do
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("1. Create Patient");
            Console.WriteLine("2. Create Physician");
            Console.WriteLine("3. Create Appointment");
            Console.WriteLine("4. Manage Medical Notes");
            Console.WriteLine("Q. Quit");

            choice = Console.ReadLine()?.ToUpper() ?? "";

            switch (choice)
            {
                case "1":
                    new Patient_Menu().CreatePatient();
                    break;
                case "2":
                    new Physician_Menu().CreatePhysician();
                    break;
                case "3":
                    new Appointment_Menu().CreateAppointment();
                    break;
                case "4":
                    new MedicalNote_Menu().ManageNotes();
                    break;
            }

        } while (choice != "Q");
    }
}
