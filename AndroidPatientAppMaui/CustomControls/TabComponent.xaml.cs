namespace AndroidPatientAppMaui.CustomControls;

public partial class TabComponent : ContentView
{
    public static string CurrentActiveTab = string.Empty;

    public static readonly BindableProperty ActiveTabProperty =
     BindableProperty.Create("ActiveTab", typeof(string), typeof(TabComponent), null, BindingMode.TwoWay, propertyChanged: ArbitraryGuidPropertyChanged);
    private static void ArbitraryGuidPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var currentTestContentView = bindable as TabComponent;

        if (currentTestContentView.ActiveTab == "Home")
        {
            CurrentActiveTab = "Home";
        }
        else if (currentTestContentView.ActiveTab == "VisitHistory")
        {
            CurrentActiveTab = "VisitHistory";
        }
        else if (currentTestContentView.ActiveTab == "Family")
        {
            CurrentActiveTab = "Family";
        }
        else if (currentTestContentView.ActiveTab == "MyMedical")
        {
            CurrentActiveTab = "MyMedical";
        }
        else if (currentTestContentView.ActiveTab == "Account")
        {
            CurrentActiveTab = "Account";
        }
    }
    public string ActiveTab
    {
        get => (string)GetValue(ActiveTabProperty);
        set => SetValue(ActiveTabProperty, value);
    }
    public TabComponent()
    {
        InitializeComponent();
        Tabs();
    }
    #region Event Handlers 
    public async Task Tabs()
    {
        await Task.Delay(500);
        demolable.Text = CurrentActiveTab;
        if (demolable.Text == "Home")
        {

        }
    } 
    /// <summary>
    /// TODO : To Define Home Page Tab Event...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TabHome_Tapped(object sender, EventArgs e)
    {
        try
        {
            App.Current.MainPage = new Views.Home.HomePage();
            //App.Current.MainPage = new Views.Home.DashboardPage();
        }
        catch (Exception ex)
        { }
    }  
    /// <summary>
    /// TODO : To Define Account Page Tab Event...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TabAccount_Tapped(object sender, EventArgs e)
    {
        try
        {
            App.Current.MainPage = new Views.MyAccount.MyAccountPage();
            // App.Current.MainPage = new Views.Account.AccountMainPage();
        }
        catch (Exception ex)
        { }
    }
    /// <summary>
    /// TODO : To Define Visit History Page Tab Event...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TabVisitHistory_Tapped(object sender, TappedEventArgs e)
    {
        App.Current.MainPage = new Views.VisitHistory.VisitHistoryPage();
    }
    /// <summary>
    /// TODO : To Define family Page Tab Event...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TabFamily_Tapped(object sender, TappedEventArgs e)
    {
        App.Current.MainPage = new Views.MyAccount.AccountProfilePage();
    }
    /// <summary>
    /// TODO : To Define My Medical Page Tab Event...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TabMyMedical_Tapped(object sender, TappedEventArgs e)
    {
        App.Current.MainPage = new Views.MyMedicalInfo.MyMedicalInfoPage(null);
    }
    #endregion


}