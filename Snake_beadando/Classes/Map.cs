using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Snake_beadando
{
    //https://github.com/Hatbatamado/prog3_beadando
    class Map
    {
        GeometryGroup palya;

        public Map(int cw, int ch)
        {
            palya = new GeometryGroup();

            Rect r = new Rect(30, 50, cw-60, 3); //felső vonal
            Palya.Children.Add(new RectangleGeometry(r));

            r = new Rect(cw-30, 50, 3, ch-175); //jobb oldalt
            Palya.Children.Add(new RectangleGeometry(r));

            r = new Rect(30, ch-125, cw-57, 3); //alul
            Palya.Children.Add(new RectangleGeometry(r));

            r = new Rect(27, 50, 3, ch-172); //bal oldalt
            Palya.Children.Add(new RectangleGeometry(r));
        }

        public GeometryGroup Palya
        {
            get
            {
                return palya;
            }

            set
            {
                palya = value;
            }
        }
    }
}
