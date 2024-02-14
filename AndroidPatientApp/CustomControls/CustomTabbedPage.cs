using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientApp.CustomControls
{
    public class CustomTabbedPage : TabbedPage
    {
        public Color SelectedIconColor
        {
            get { return (Color)GetValue(SelectedIconColorProperty); }
            set { SetValue(SelectedIconColorProperty, value); }
        }

        public static readonly BindableProperty SelectedIconColorProperty = BindableProperty.Create(
            nameof(SelectedItemProperty),
            typeof(Color),
            typeof(CustomTabbedPage),
            Color.FromHex("#FFFFFF"));

        public Color UnselectedIconColor
        {
            get { return (Color)GetValue(UnelectedIconColorProperty); }
            set { SetValue(UnelectedIconColorProperty, value); }
        }

        public static readonly BindableProperty UnelectedIconColorProperty = BindableProperty.Create(
            nameof(UnselectedIconColor),
            typeof(Color),
            typeof(CustomTabbedPage),
            Color.FromHex("#FFFFFF"));

        public Color SelectedTextColor
        {
            get { return (Color)GetValue(SelectedTextColorProperty); }
            set { SetValue(SelectedTextColorProperty, value); }
        }

        public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(
            nameof(SelectedTextColor),
            typeof(Color),
            typeof(CustomTabbedPage),
            Color.FromHex("#FFFFFF"));

        public Color UnselectedTextColor
        {
            get { return (Color)GetValue(UnselectedTextColorProperty); }
            set { SetValue(UnselectedTextColorProperty, value); }
        }

        public static readonly BindableProperty UnselectedTextColorProperty = BindableProperty.Create(
            nameof(UnselectedTextColor),
            typeof(Color),
            typeof(CustomTabbedPage),
            Color.FromHex("#FFFFFF"));

        public bool IsTabEnabled
        {
            get { return (bool)GetValue(EnableTabProperty); }
            set { SetValue(EnableTabProperty, value); }
        }

        public static readonly BindableProperty EnableTabProperty = BindableProperty.Create(
            nameof(IsTabEnabled),
            typeof(bool),
            typeof(CustomTabbedPage),
            true);
    }
}
