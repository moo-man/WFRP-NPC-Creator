using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WFRP_NPC_Creator
{
    public class SpeciesEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] array = Enum.GetValues(value as Type).OfType<object>().Select(o => o.ToString()).ToArray();
            array[0] = "Test";
            return array;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
