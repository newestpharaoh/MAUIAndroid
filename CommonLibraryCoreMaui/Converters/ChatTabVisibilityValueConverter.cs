using System;
using System.Globalization;
using CommonLibraryCoreMaui.Models;
using MvvmCross.Converters;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.Converters
{
    public class ChatTabVisibilityValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            if (!(value is MvxObservableCollection<WaitingPatient> list))
                return null;

            if (list.Count == (int)parameter)
                return true;
            else
                return false;
        }
    }
}
