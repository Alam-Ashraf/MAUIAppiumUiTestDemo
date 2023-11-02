namespace MAUIAppiumUiTestDemo;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        StatusLabel.Text = "Logging in " + DateTime.Now.ToString("HH:mm:ss");

        await Task.Delay(2000);

        await Navigation.PushAsync(new HomePage());
    }
}

