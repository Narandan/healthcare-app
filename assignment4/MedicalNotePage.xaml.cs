using MedicalChartApp.Models;

namespace MedicalChartApp.Pages;

public partial class MedicalNotePage : ContentPage
{
    private Patient? selectedPatient;
    private MedicalNote? selectedNote;

    public MedicalNotePage()
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
        RefreshNoteList();
    }

    private void RefreshNoteList()
    {
        if (selectedPatient == null) { NoteList.ItemsSource = null; return; }
        NoteList.ItemsSource = null;
        NoteList.ItemsSource = selectedPatient.MedicalNotes;
    }

    private void OnNoteSelected(object sender, SelectionChangedEventArgs e)
    {
        selectedNote = e.CurrentSelection.FirstOrDefault() as MedicalNote;
    }

    private async void OnAddNoteClicked(object sender, EventArgs e)
    {
        if (selectedPatient == null) { await DisplayAlert("Error", "Select a patient first.", "OK"); return; }

        var note = new MedicalNote { Diagnosis = "New Diagnosis", Prescription = "New Prescription" };
        selectedPatient.MedicalNotes.Add(note);
        RefreshNoteList();
    }

    private async void OnEditNoteClicked(object sender, EventArgs e)
    {
        if (selectedNote == null) { await DisplayAlert("Error", "Select a note first.", "OK"); return; }
        selectedNote.Diagnosis += " (Edited)";
        RefreshNoteList();
    }

    private async void OnDeleteNoteClicked(object sender, EventArgs e)
    {
        if (selectedPatient == null || selectedNote == null) { await DisplayAlert("Error", "Select a note first.", "OK"); return; }
        selectedPatient.MedicalNotes.Remove(selectedNote);
        RefreshNoteList();
        selectedNote = null;
    }
}
