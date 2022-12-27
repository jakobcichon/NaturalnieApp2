namespace NaturalnieApp2.SharedControls.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;

    public class StringMatchToVisibility : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values is null || values.Count() != 2)
            {
                return false;
            }

            string? selectedName = values[0] as string;
            string? currentObjectName = values[1] as string;

            if (selectedName is null || currentObjectName is null)
            {
                return false;
            }

            if (selectedName.Equals(currentObjectName))
            {
                return true;
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
