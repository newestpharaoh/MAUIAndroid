using AndroidPatientAppMaui.ViewModels.MyAccount;


namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class UpdateCardInformationPage : ContentPage
{
    //To define the class lavel variable.
    UpdateCardInformationPageViewModel VM;
    #region Constructor
    public UpdateCardInformationPage()
    {
        InitializeComponent();
        this.BindingContext = VM = new UpdateCardInformationPageViewModel(this.Navigation);
    }
    #endregion

    #region Event Handler
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await VM.GetCreditCarddInfo();
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    private void StatePicker_SelectedIndexChanged(object sender, EventArgs e)
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

    private void MonthPicker_SelectedIndexChanged(object sender, EventArgs e)
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

    private void YearPicker_SelectedIndexChanged(object sender, EventArgs e)
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
}