namespace Module03Exercise01.View;

public partial class AddEmployee : ContentPage
{
    public AddEmployee()
    {
        InitializeComponent();
    }
    private async void OnGetLocationClicked(object sender, EventArgs e)
    {
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync
                (
                    new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.High
                    }
                );
            }
            if (location != null)
            {
                CoordinatesLabel.Text = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}";

                //Get Geocoding - Get address from Latitude and Longitude
                var placemarks = await Geocoding.Default.GetPlacemarksAsync(location.Latitude, location.Longitude);
                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    MunicipalityEntry.Text = $"{placemark.Locality}";
                    ProvinceEntry.Text = $"{placemark.AdminArea}";
                }
                else
                {
                    MunicipalityEntry.Text = "Unable to determine the municipality.";
                    ProvinceEntry.Text = "Unable to determine the province.";
                }

            }
            else
            {
                MunicipalityEntry.Text = "Unable to get location.";
                ProvinceEntry.Text = "Unable to get location.";
            }
        }
        catch (Exception ex)
        {
            MunicipalityEntry.Text = $"Error: {ex.Message}";
            ProvinceEntry.Text = $"Error: {ex.Message}";
        }
    }

    private async void OnCapturePhotoClicked(object sender, EventArgs e)
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                //Capture a photo using MediaPicker
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {
                    await LoadPhotoAsync(photo);
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occured: {ex.Message}", "OK");
        }
    }

    //Load photo and display it in the Image control
    private async Task LoadPhotoAsync(FileResult photo)
    {
        if (photo == null)
            return;

        Stream stream = await photo.OpenReadAsync();
        CaptureImage.Source = ImageSource.FromStream(() => stream);
    }

    private async void OnEmployeePageClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//EmployeePage");
    }
}