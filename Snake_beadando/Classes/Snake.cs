using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Snake_beadando
{
    class Snake : Bindable
    {
        List<Rect> elemek;
        int x, y, w, h;
        Direction direct;
        int move = 15;

        public Snake(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            Elemek = new List<Rect>();
            for (int i = 0; i < 3; i++)
            {
                Elemek.Add(new Rect(x, y, w, h));
                y -= 15;
            }
            Direct = Direction.le;
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
            }
        }

        public Direction Direct
        {
            get
            {
                return direct;
            }

            set
            {
                direct = value; OPC();
            }
        }

        public void Move(DispatcherTimer dt, Snake ellenseg, Map map)
        {
            for (int i = elemek.Count - 1; i > 0; i--)
            {
                elemek[i] = elemek[i - 1];
            }

            Rect elem = Elemek[0];

            switch (direct)
            {
                case Direction.balra:
                    elem.X -= move;
                    break;
                case Direction.jobbra:
                    elem.X += move;
                    break;
                case Direction.fel:
                    elem.Y -= move;
                    break;
                case Direction.le:
                    elem.Y += move;
                    break;
            }

            Elemek[0] = elem;
            OPC("Elemek");

            if (Utkozik(ellenseg, map))
            {
                dt.Stop();
                JatekosInit();
            }
        }

        public void JatekosInit()
        {
            //vm.játékos
            jatekos = new Snake(40, 100, 10, 10);
        }

        private bool Utkozik(Snake ellenseg, Map map)
        {
            //önmagával való ütközés => game over => egyből return:
            for (int i = 1; i < Elemek.Count; i++)
            {
                if (Elemek[0].IntersectsWith(Elemek[i]))
                    return true;
            }

            //ellenséggel való ütközés => game over => egyből return:
            foreach (Rect r in ellenseg.Elemek)
            {
                if (Elemek[0].IntersectsWith(r))
                    return true;
            }

            //fallal való ütközés => game over => egyből return:
            //fent és lent nem érzékeli az intersectet :(
            //PathGeometry pg = map.Palya.GetFlattenedPathGeometry();
            //PathGeometry rg = new RectangleGeometry(Elemek[0]).GetFlattenedPathGeometry();
            //if (Geometry.Combine(pg, rg, GeometryCombineMode.Intersect, null).GetArea() > 0)
            //    return true;
            foreach (var c in map.Palya.Children)
            {
                RectangleGeometry rg = (RectangleGeometry)c;
                if (Elemek[0].IntersectsWith(rg.Rect))
                    return true;
            }

            //todo: kajával ütközés
            return false;
        }
    }
}
