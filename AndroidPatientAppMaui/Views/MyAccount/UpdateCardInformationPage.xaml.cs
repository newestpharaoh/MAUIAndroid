using AndroidPatientAppMaui.ViewModels.MyAccount;


namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class UpdateCardInformationPage : ContentPage
{
    //To define the class lavel variable.
    UpdateCardInformationPageViewModel VM;
    #region Constructor
    public UpdateCardInformationPage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new UpdateCardInformationPageViewModel(this.Navigation);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion

    #region Event Handler
    /// <summary>
    /// TODO : to define onappearing...
    /// </summary>
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await VM.GetCreditCarddInfo();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To define the State picker Selected Index change event...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StatePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VM.BillingStateLbl = string.Empty;
            if (VM.card != null)
            {
                Picker picker = (Picker)sender;
                int selectedIndex = picker.SelectedIndex;
                if (selectedIndex != -1)
                {
                    VM.card.BillingState = (string)picker.SelectedItem;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To define the Month Picker Selected Index chang event...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MonthPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VM.CardExpirationMonthLbl = string.Empty;
            if (VM.card != null)
            {
                Picker picker = (Picker)sender;
                int selectedIndex = picker.SelectedIndex;
                if (selectedIndex != -1)
                {
                    VM.card.CardExpirationMonth = (string)picker.SelectedItem;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To define the Year picker Selected Index change event...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void YearPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VM.CardExpirationYearLbl = string.Empty;
            if (VM.card != null)
            {
                Picker picker = (Picker)sender;
                int selectedIndex = picker.SelectedIndex;
                if (selectedIndex != -1)
                {
                    VM.card.CardExpirationYear = (string)picker.SelectedItem;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion


}