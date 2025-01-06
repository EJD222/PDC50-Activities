using Module05Exercise01.Services; //Add line
using MySql.Data.MySqlClient; //Add line
using Module05Exercise01.View; //Add line

namespace Module05Exercise01
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseConnectionService _databaseConnectionService;
        public MainPage()
        {
            InitializeComponent();

            //Initialize database connection
            _databaseConnectionService = new DatabaseConnectionService();
        }

        private async void OnTestConnectionClicked(object sender, EventArgs e)
        {
            var connectionString = _databaseConnectionService.GetConnectionString();
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    ConnectionStatusLabel.Text = "Connection Successful";
                    ConnectionStatusLabel.TextColor = Colors.Green;
                }
            }
            catch (Exception ex)
            {
                ConnectionStatusLabel.Text = $"Connection Failed: {ex.Message}";
                ConnectionStatusLabel.TextColor = Colors.Red;
            }
        }

        private async void OnViewEmployeesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ViewEmployees");
            //await Navigation.PushAsync(new ViewEmployees());
        }
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }

}
