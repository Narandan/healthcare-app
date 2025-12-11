using MedicalChartApp.Models;

namespace MedicalChartApp.Pages;

public partial class PatientPage : ContentPage
{
    private Patient? selectedPatient;

    public PatientPage()
    {
        InitializeComponent();
        RefreshPatientList();
    }

    private void RefreshPatientList()
    {
        PatientList.ItemsSource = null;
        PatientList.ItemsSource = DataStore.Patients;
    }

    private void OnPatientSelected(object sender, SelectionChangedEventArgs e)
    {
        selectedPatient = e.CurrentSelection.FirstOrDefault() as Patient;
    }

    private async void OnAddPatientClicked(object sender, EventArgs e)
    {
        var patient = new Patient { Name = "New Patient" };
        DataStore.Patients.Add(patient);
        RefreshPatientList();
        await DisplayAlert("Added", $"Patient {patient.Name} added.", "OK");
    }

    private async void OnEditPatientClicked(object sender, EventArgs e)
    {
        if (selectedPatient == null)
        {
            await DisplayAlert("Error", "Select a patient first.", "OK");
            return;
        }

        selectedPatient.Name += " (Edited)";
        RefreshPatientList();
        await DisplayAlert("Edited", $"Patient {selectedPatient.Name} updated.", "OK");
    }

    private async void OnDeletePatientClicked(object sender, EventArgs e)
    {
        if (selectedPatient == null)
        {
            await DisplayAlert("Error", "Select a patient first.", "OK");
            return;
        }

        DataStore.Patients.Remove(selectedPatient);
        RefreshPatientList();
        await DisplayAlert("Deleted", $"Patient {selectedPatient.Name} removed.", "OK");
        selectedPatient = null;
    }
}
