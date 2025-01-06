namespace Module08.View;
using Module08.ViewModel; //Add this line

public partial class UserPage : ContentPage
{
	public UserPage()
	{
		InitializeComponent();
		BindingContext = new UserViewModel();
	}

    private async void OnClickedHome(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}