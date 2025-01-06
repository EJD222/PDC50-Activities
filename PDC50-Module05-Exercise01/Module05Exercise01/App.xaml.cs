using Module05Exercise01.Resources.Styles;
namespace Module05Exercise01
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                this.Resources.MergedDictionaries.Add(new DefaultResources());
            }
        }
    }
}
