using Module05Exercise01.Services;
using Module05Exercise01.ViewModel;

namespace Module05Exercise01.View;

public partial class ViewEmployees : ContentPage
{
	public ViewEmployees()
	{
		InitializeComponent();

        var employeeViewModel = new EmployeelViewModel();
        BindingContext = employeeViewModel;
    }
    private async void OnHomePageClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
    }
}