﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFDevelopers.Converts
{
    public class IndexToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var num = System.Convert.ToInt32(value);
            //var brushConverter = new BrushConverter();
            //var brush = (Brush)brushConverter.ConvertFromString("#BAE766");
            //if ((num % 2) == 1)
            //    brush = (Brush)brushConverter.ConvertFromString("#B0D440");
            //return brush;
            var brush = Application.Current.Resources["CircularSingularSolidColorBrush"] as SolidColorBrush; 
            if ((num % 2) == 1)
                brush = Application.Current.Resources["CircularDualSolidColorBrush"] as SolidColorBrush;
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
