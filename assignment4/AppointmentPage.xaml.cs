using MedicalChartApp.Models;

namespace MedicalChartApp.Pages;

public partial class AppointmentPage : ContentPage
{
    private Appointment? selectedAppointment;

    public AppointmentPage()
    {
        InitializeComponent();
        RefreshAppointmentList();
    }

    private void RefreshAppointmentList()
    {
        AppointmentList.ItemsSource = null;
        AppointmentList.ItemsSource = DataStore.Appointments;
    }

    private void OnAppointmentSelected(object sender, SelectionChangedEventArgs e)
    {
        selectedAppointment = e.CurrentSelection.FirstOrDefault() as Appointment;
    }

    private async void OnAddAppointmentClicked(object sender, EventArgs e)
    {
        if (!DataStore.Patients.Any() || !DataStore.Physicians.Any())
        {
            await DisplayAlert("Error", "Need at least one patient and physician.", "OK");
            return;
        }

        var appointment = new Appointment
        {
            Patient = DataStore.Patients.First(),
            Physician = DataStore.Physicians.First(),
            StartTime = DateTime.Now.AddHours(1)
        };

        // Check double booking
        bool conflict = DataStore.Appointments.Any(a =>
            a.Physician == appointment.Physician &&
            a.StartTime == appointment.StartTime);

        if (conflict)
        {
            await DisplayAlert("Conflict", "Physician is already booked at that time.", "OK");
            return;
        }

        DataStore.Appointments.Add(appointment);
        RefreshAppointmentList();
        await DisplayAlert("Added", $"Appointment added.", "OK");
    }

    private async void OnDeleteAppointmentClicked(object sender, EventArgs e)
    {
        if (selectedAppointment == null)
        {
            await DisplayAlert("Error", "Select an appointment first.", "OK");
            return;
        }

        DataStore.Appointments.Remove(selectedAppointment);
        RefreshAppointmentList();
        await DisplayAlert("Deleted", $"Appointment removed.", "OK");
        selectedAppointment = null;
    }
}
