using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Snake_beadando
{
    class Food : Bindable
    {
        List<Rect> elemek;
        int top, bottom, left, right;

        public Food(Image img)
        {
            elemek = new List<Rect>();
            top = 60;
            left = 40;
            bottom = (int)img.ActualHeight - 10;
            right = (int)img.ActualWidth - 10;
        }

        public void AddFood(Random R, Snake jatekos, Snake ellenseg)
        {
            Rect elem;
            bool ujra = false;
            do
            {
                elem = new Rect(R.Next(left, right), R.Next(top, bottom), 10, 10);
                bool tmp1 = Utkozes(jatekos, elem);
                bool tmp2 = Utkozes(ellenseg, elem);
                ujra = tmp1 && tmp2;
            } while (!ujra);

            if (elem != null)
            {
                Brush tmp = Brushes.Black;
                int szinek = R.Next(1, 101);
                if (szinek < 50)
                    tmp = Brushes.Yellow;
                Elemek.Add(elem);
                OPC("Elemek");
            }
        }

        private bool Utkozes(Snake elem, Rect mivel)
        {
            int i = 0;
            while (i < elem.Elemek.Count && !mivel.IntersectsWith(elem.Elemek[i]))
                i++;
            if (i == elem.Elemek.Count)
                return true;

            return false;
        }

        public List<Rect> Elemek
        {
            get
            {
                return elemek;
            }

            set
            {
                elemek = value;
                OPC();
            }
        }
    }
}
