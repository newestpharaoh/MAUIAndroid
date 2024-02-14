namespace AndroidPatientAppMaui.CustomControls;

public class CustomEntry : Entry
{
    public static readonly BindableProperty IsSearchActiveProperty = BindableProperty.Create(nameof(IsSearchActive), typeof(bool), typeof(CustomEntry), false, BindingMode.OneWayToSource);
    public bool IsSearchActive
    {
        get => (bool)this.GetValue(IsSearchActiveProperty);
        set => this.SetValue(IsSearchActiveProperty, value);
    }

}