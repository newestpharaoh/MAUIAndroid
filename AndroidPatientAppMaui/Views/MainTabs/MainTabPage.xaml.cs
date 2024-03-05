namespace AndroidPatientAppMaui.Views.MainTabs;

public partial class MainTabPage : TabbedPage
{
	string IsTabChange = string.Empty;
	public MainTabPage(string _IsTabChange)
	{
		try
		{
			InitializeComponent();
			IsTabChange = _IsTabChange;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		if (IsTabChange == "Home")
			CurrentPage = Children[0];
		else if (IsTabChange == "VisitHistory")
			CurrentPage = Children[1];
		else if (IsTabChange == "Family")
			CurrentPage = Children[2];
		else if(IsTabChange == "MyMedicalPage")
			CurrentPage = Children[3];
		 else if (IsTabChange == "MyAccount")
            CurrentPage = Children[4];

		IsTabChange = string.Empty;
    }
}