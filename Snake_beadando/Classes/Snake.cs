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
        List<PowerUP> powerUp;
        bool changeAble;

        public Snake() { }

        public Snake(int x, int y, int w, int h, Direction d, Player p)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            Elemek = new List<Rect>();
            PowerUp = new List<PowerUP>();
            //alap 3 elem
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

        internal List<PowerUP> PowerUp
        {
            get
            {
                return powerUp;
            }

            set
            {
                powerUp = value;
                OPC();
            }
        }

        public bool ChangeAble
        {
            get
            {
                return changeAble;
            }

            set
            {
                changeAble = value;
            }
        }

        public Status Move(Snake actual, Snake enemy)
        {
            //mozgatás
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

            //ütközések vizsgálata
            Status statusz = Utkozik(actual, enemy);

            return statusz;
        }

        private Status Utkozik(Snake actual, Snake enemy)
        {
            ViewModel vm = ViewModel.Get(0, 0);

            //önmagával való ütközés => game over => egyből return:
            for (int i = 1; i < actual.Elemek.Count; i++)
            {
                if (actual.Elemek[0].IntersectsWith(actual.Elemek[i]))
                    return Status.gameover;
            }

            //ellenséggel való ütközés => game over => egyből return:
            if (enemy != null)
            {
                foreach (Rect r in enemy.Elemek)
                {
                    if (actual.Elemek[0].IntersectsWith(r))
                        return Status.gameover;
                }
            }

            //fallal való ütközés => game over => egyből return:
            //fent és lent nem érzékeli az intersectet PG-vel :(
            //PathGeometry pg = map.Palya.GetFlattenedPathGeometry();
            //PathGeometry rg = new RectangleGeometry(Elemek[0]).GetFlattenedPathGeometry();
            //if (Geometry.Combine(pg, rg, GeometryCombineMode.Intersect, null).GetArea() > 0)
            //    return true;
            foreach (var c in vm.Map.Palya.Children)
            {
                RectangleGeometry rg = (RectangleGeometry)c;
                if (actual.Elemek[0].IntersectsWith(rg.Rect))
                    return Status.gameover;
            }

            //kajával ütközés
            Status statusz = KajaUtkozes(vm.Kaja, actual, vm);
            if (statusz == Status.nothing)
                statusz = KajaUtkozes(vm.Rocket, actual, vm);
            return statusz;
        }

        private Status KajaUtkozes(Food kaja, Snake actual, ViewModel vm)
        {
            int j = 0;
            while (j < kaja.Elemek.Count && !actual.Elemek[0].IntersectsWith(kaja.Elemek[j]))
                j++;
            if (j < kaja.Elemek.Count)
            {
                kaja.Elemek.RemoveAt(j); //kaja törlése
                actual.Elemek.Add(actual.Elemek.Last<Rect>()); //snake elem növélese 1-el
                if (kaja is Rocket)
                {
                    //ha rakéta power up-os volt a kaja, akkor adjuk hozzá a power upokhoz
                    vm.UpdateKaja(Status.rocket);
                    AddPowerUp(actual, Status.rocket, vm);
                    return Status.rocket;
                }
                else
                {
                    vm.UpdateKaja(Status.kaja);
                    return Status.kaja;
                }
            }
            return Status.nothing;
        }

        private void AddPowerUp(Snake actual, Status tipus, ViewModel vm)
        {
            //max 2 különböző power up lehet egy játékosnál
            if (actual.PowerUp.Count < 2)
            {
                //van-e már ilyen power up-unk?
                bool addable = true;
                foreach(PowerUP p in actual.PowerUp)
                {
                    if (p.Tipus == tipus)
                        addable = false;
                }
                if (addable)
                {
                    //ha nincs, akkor vegyük fel
                    actual.PowerUp.Add(new PowerUP((actual == vm.Jatekos ? System.Windows.Input.Key.Space : System.Windows.Input.Key.RightCtrl), "Narancs rakéta", Status.rocket));
                    if (actual == vm.Jatekos)
                        vm.JatekosPowerUp = GetPowerUps(vm.Jatekos.PowerUp);
                    else
                        vm.EllensegPowerUp = GetPowerUps(vm.Ellenseg.PowerUp);
                }
            }
        }

        public void UseRocket(Snake actual, Snake enemy, ViewModel vm, DispatcherTimer dt1, DispatcherTimer dt2, DispatcherTimer dt3)
        {
            //van-e rakétánk amit tudunk használni?
            bool haveRocket = false;
            int i = 0;
            while (i < actual.PowerUp.Count && actual.PowerUp[i].Tipus != Status.rocket)
                i++;
            if (i < actual.PowerUp.Count)
                haveRocket = true;

            if (haveRocket)
            {
                //ha van akkor az ellenség 30%-át vonjuk le
                List<Rect> tmp = new List<Rect>();
                int db = (int)Math.Round(enemy.Elemek.Count * 0.3, 0);
                if (db == 0)
                    db++;
                for (int j = 0; j < enemy.Elemek.Count-db; j++)
                    tmp.Add(enemy.Elemek[j]);

                //ha minden elemét kilőttük az ellennek game over
                if (tmp.Count == 0)
                     vm.GameOver(enemy, (actual == vm.Jatekos ? Player.ellenseg : Player.jatekos), dt1, dt2, dt3);
                else
                {
                    //ha nem, akkor frissítünk
                    enemy.Elemek = tmp;

                    if (actual == vm.Jatekos)
                        vm.JatekosPowerUp = RemovePowerUp(actual.PowerUp[i], actual.PowerUp);
                    else
                        vm.EllensegPowerUp = RemovePowerUp(actual.PowerUp[i], actual.PowerUp);
                }
            }
        }

        private string RemovePowerUp(PowerUP p, List<PowerUP> plist)
        {
            //power up törlése használat után és lista frissítése
            plist.Remove(p);
            return GetPowerUps(plist);
        }

        private string GetPowerUps(List<PowerUP> powerups)
        {
            string s = "";
            foreach (PowerUP p in powerups)
            {
                s += p.ToString();
            }
            return s;
        }
    }
}
