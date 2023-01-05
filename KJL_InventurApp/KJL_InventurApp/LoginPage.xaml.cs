namespace KJL_InventurApp;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private void checkLoginData(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync($"//{nameof(ScanPage)}");
	}
}	