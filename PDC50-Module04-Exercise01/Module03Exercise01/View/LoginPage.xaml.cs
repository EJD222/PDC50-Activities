using Microsoft.Maui.Controls;
using Module03Exercise01.Services;

namespace Module03Exercise01.View

{
    public partial class LoginPage : ContentPage
    {
        //Field for the Service
        private readonly IMyService _myService; 
        public LoginPage(IMyService myService)
        {
            InitializeComponent();
            _myService = myService;

            var message = _myService.GetMessage();
            MyLabel.Text = message;
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AddEmployee");
        }
    }
}