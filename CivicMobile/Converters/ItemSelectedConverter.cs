﻿using System.Globalization;

namespace CivicMobile.Converters;

public class ItemSelectedConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ItemTappedEventArgs eventArgs)
            return eventArgs.Item;
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}