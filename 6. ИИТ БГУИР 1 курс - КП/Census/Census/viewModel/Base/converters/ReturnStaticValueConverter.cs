using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace Census.viewModel.Base
{
    class ReturnStaticBoolConverter : IValueConverter
    {
        private bool p = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (bool.Parse(parameter.ToString()).Equals(value))
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if ((bool)value == true)
            {
                p = bool.Parse(parameter.ToString());
            }
            return p;
        }
    }
}
