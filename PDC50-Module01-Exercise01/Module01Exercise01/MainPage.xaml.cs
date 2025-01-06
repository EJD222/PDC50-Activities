using Microsoft.Maui.Controls;

namespace Module01Exercise01
{
    public partial class MainPage : ContentPage
    {
        public string WelcomeMessage { get; set; }
        public string EditYourProfile { get; set; }
        public FontAttributes BoldText { get; set; }
        public double Heading1 { get; set; }
        public double Heading2 { get; set; }
        public double Text1 { get; set; }

        // Property to bind the switch state
        private bool _isDarkMode;
        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                _isDarkMode = value;
                OnPropertyChanged(nameof(IsDarkMode));
                UpdateTheme();
            }
        }

        public MainPage()
        {
            InitializeComponent();

            WelcomeMessage = "Welcome User!";
            EditYourProfile = "Edit Your Profile";
            BoldText = FontAttributes.Bold;
            Heading1 = 40;
            Heading2 = 25;
            Text1 = 16;
            IsDarkMode = false; // Default to light mode
            this.BindingContext = this;
        }

        private void OnDarkModeToggled(object sender, ToggledEventArgs e)
        {
            IsDarkMode = e.Value;
        }

        private void UpdateTheme()
        {
            if (IsDarkMode)
            {
                this.Resources["BackgroundColor"] = Colors.Black;
                this.Resources["TextColor"] = Colors.White;
                this.Resources["EntryBorderColor"] = Color.FromArgb("#502bd4");
            }
            else
            {
                this.Resources["BackgroundColor"] = Colors.White;
                this.Resources["TextColor"] = Colors.Black;
                this.Resources["EntryBorderColor"] = Colors.Black;
            }
        }

        private void OnClickSave(object sender, EventArgs e)
        {
            this.Resources["DynamicMessage"] = "Saved successfully!";
        }
    }
}