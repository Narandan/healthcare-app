using MedicalChartApp.Models;

namespace MedicalChartApp.Pages;

public partial class PhysicianPage : ContentPage
{
    private Physician? selectedPhysician;

    public PhysicianPage()
    {
        InitializeComponent();
        RefreshPhysicianList();
    }

    private void RefreshPhysicianList()
    {
        PhysicianList.ItemsSource = null;
        PhysicianList.ItemsSource = DataStore.Physicians;
    }

    private void OnPhysicianSelected(object sender, SelectionChangedEventArgs e)
    {
        selectedPhysician = e.CurrentSelection.FirstOrDefault() as Physician;
    }

    private async void OnAddPhysicianClicked(object sender, EventArgs e)
    {
        var physician = new Physician { Name = "New Physician" };
        DataStore.Physicians.Add(physician);
        RefreshPhysicianList();
        await DisplayAlert("Added", $"Physician {physician.Name} added.", "OK");
    }

    private async void OnEditPhysicianClicked(object sender, EventArgs e)
    {
        if (selectedPhysician == null)
        {
            await DisplayAlert("Error", "Select a physician first.", "OK");
            return;
        }

        selectedPhysician.Name += " (Edited)";
        RefreshPhysicianList();
        await DisplayAlert("Edited", $"Physician {selectedPhysician.Name} updated.", "OK");
    }

    private async void OnDeletePhysicianClicked(object sender, EventArgs e)
    {
        if (selectedPhysician == null)
        {
            await DisplayAlert("Error", "Select a physician first.", "OK");
            return;
        }

        DataStore.Physicians.Remove(selectedPhysician);
        RefreshPhysicianList();
        await DisplayAlert("Deleted", $"Physician {selectedPhysician.Name} removed.", "OK");
        selectedPhysician = null;
    }
}
