namespace DCC;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new MainPage()) { Title = "DCC" };

#if WINDOWS || MACCATALYST
        // Set the desired width and height in pixels
        window.Width = 600;
        window.Height = 600;
#endif

        return window;
    }
}