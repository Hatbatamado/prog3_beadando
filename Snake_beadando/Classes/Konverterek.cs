using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Snake_beadando
{
    //https://github.com/Hatbatamado/prog3_beadando
    class PlayerKonverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GeometryGroup gg = new GeometryGroup();
            List<Rect> elemek = (List<Rect>)value;
            if (elemek != null && elemek.Count > 0)
            {
                foreach (Rect r in elemek)
                {
                    RectangleGeometry rg = new RectangleGeometry(r);
                    gg.Children.Add(rg);
                }
            }
            return gg;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
