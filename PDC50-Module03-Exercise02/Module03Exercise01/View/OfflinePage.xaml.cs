namespace Module03Exercise01.View;

public partial class OfflinePage : ContentPage
{
	public OfflinePage()
	{
		InitializeComponent();
	}

    private async void OnRetryButtonClicked(object sender, EventArgs e)
    {
        var current = Connectivity.NetworkAccess;
        if (current == NetworkAccess.Internet)
        {
            await DisplayAlert("Connected", "Your internet connection was restored.", "Ok");
            await Shell.Current.GoToAsync("//LoginPage");
        }

        else
        {
            await DisplayAlert("No Internet", "Please check your internet connection and try again.", "Ok");
        }
    }
    private async void OnExitButtonClicked(object sender, EventArgs e)
    {
        bool isConfirmed = await DisplayAlert("Confirm Exit", "Are you sure you want to exit the application?", "Yes", "No");

        if (isConfirmed)
        {
            System.Environment.Exit(0);
        }
    }
}