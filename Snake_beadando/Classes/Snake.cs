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

        public Snake() { }

        public Snake(int x, int y, int w, int h, Direction d, Player p)
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
            Direct = d;
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

        public bool Move(DispatcherTimer dt1, DispatcherTimer dt2, Snake actual, Snake enemy, Map map)
        {
            for (int i = actual.elemek.Count - 1; i > 0; i--)
            {
                actual.elemek[i] = actual.elemek[i - 1];
            }

            Rect elem = actual.Elemek[0];

            switch (actual.Direct)
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

            actual.Elemek[0] = elem;
            OPC("Elemek");

            if (Utkozik(actual, enemy, map))
            {
                if (dt1 != null)
                    dt1.Stop();
                if (dt2 != null)
                    dt2.Stop();
                return true;
            }
            return false;
        }

        private bool Utkozik(Snake actual, Snake enemy, Map map)
        {
            //önmagával való ütközés => game over => egyből return:
            for (int i = 1; i < actual.Elemek.Count; i++)
            {
                if (actual.Elemek[0].IntersectsWith(actual.Elemek[i]))
                    return true;
            }

            //ellenséggel való ütközés => game over => egyből return:
            if (enemy != null)
            {
                foreach (Rect r in enemy.Elemek)
                {
                    if (actual.Elemek[0].IntersectsWith(r))
                        return true;
                }
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
                if (actual.Elemek[0].IntersectsWith(rg.Rect))
                    return true;
            }

            //todo: kajával ütközés
            return false;
        }
    }
}
