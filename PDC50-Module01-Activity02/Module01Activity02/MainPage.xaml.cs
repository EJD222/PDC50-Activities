namespace Module01Activity02
{
    public partial class MainPage : ContentPage
    {
        public string PageTitle { get; set; }
        public Color ColorBlack { get; set; }
        public Color ColorWhite { get; set; }
        public FontAttributes BoldText { get; set; }
        public double Heading1 { get; set; }
        public double Heading2 { get; set; }
        public double Text1 { get; set; }
        public MainPage()
        {
            InitializeComponent();

            PageTitle = "Hello, MAUI!";
            ColorWhite = Colors.White;
            ColorBlack = Colors.Black;
            BoldText = FontAttributes.Bold;
            Heading1 = 50;
            Heading2 = 22;
            Text1 = 16;

            this.BindingContext = this;
        }

        private void OnChangedLabelNameStatic(object sender, EventArgs e)
        {
            this.Resources["StaticUsername"] = "Time to TWICE";
        }
        private void OnChangedLabelNameDynamic(object sender, EventArgs e)
        {
            this.Resources["DynamicUsername"] = "Im Nayeon";
        }
        private void OnChangedBackgroundColorToLightGreen(object sender, EventArgs e)
        {
            this.Resources["LocalBackgroundColor"] = Color.FromArgb("#A3D1CC");
        }
        private void OnChangedBackgroundColorToPink(object sender, EventArgs e)
        {
            this.Resources["LocalBackgroundColor"] = Colors.Pink;
        }
        private void OnChangedBackgroundColorToLightBlue(object sender, EventArgs e)
        {
            this.Resources["LocalBackgroundColor"] = Colors.LightBlue;
        }
        private void OnChangedBackgroundColorToBlack(object sender, EventArgs e)
        {
            this.Resources["LocalBackgroundColor"] = Colors.Black;
        }
        private void OnChangeBoxViewColor(object sender, EventArgs e)
        {
            this.Resources["DynamicBoxColor"] = Color.FromArgb("#F3991A");
        }
    }

}