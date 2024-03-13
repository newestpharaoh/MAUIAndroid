using AndroidPatientAppMaui.ViewModels.Home;

namespace AndroidPatientAppMaui.Views.Home;

public partial class PatientPreVisitForSomeoneElse : ContentPage
{
    PatientPreVisitForSomeoneElseViewModel VM;
    #region Constructor
    public PatientPreVisitForSomeoneElse()
    {
        InitializeComponent();
        this.BindingContext = VM = new PatientPreVisitForSomeoneElseViewModel(this.Navigation);
    }
    #endregion

    #region Event Handler
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
    #endregion

    private void spnrYourNamePicker_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void spnrRelationshipPicker_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void spnrPatientNamePicker_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}