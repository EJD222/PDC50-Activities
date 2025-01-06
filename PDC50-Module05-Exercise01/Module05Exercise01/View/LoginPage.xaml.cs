namespace Module05Exercise01.View;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
        //await Navigation.PushAsync(new ViewEmployees());
    }
}