using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Snake_beadando
{
    class ViewModel : Bindable
    {
        Snake jatekos;
        Snake ellenseg;
        Map map;
        string jatekosUzenet;
        string ellensegUzenet;
        int cw;
        int ch;
        Food kaja;
        Rocket rocket;
        static ViewModel peldany;
        string leiras;
        string jatekosPowerUp;
        string ellensegPowerUp;
        string jatekosIranyitas;
        string ellensegIranyitas;

        public static ViewModel Get(int cw, int ch)
        {
            if (peldany == null)
                peldany = new ViewModel(cw, ch);
            return peldany;
        }

        private ViewModel(int cw, int ch)
        {
            this.ch = ch;
            this.cw = cw;
            Leiras = "Narancs: az ellenfél elveszti a hosszának 30 % -át";
            Map = new Map(cw, ch);
            JatekosUzenet = "Kezdéshez nyomd meg: 'F'-t";
            EllensegUzenet = "Kezdéshez nyomd meg: 'Numpad 0'-t";
            JatekosIranyitas = "Kék\nW: fel\nS: le\nA: balra\nD: jobbra";
            EllensegIranyitas = "Piros\nFel nyíl\nLe nyíl\nBalra nyíl\nJobbra nyíl";
        }

        public void UpdateKaja(Status statusz)
        {
            //frissítés
            switch (statusz)
            {
                case Status.kaja:
                    OPC("Kaja");
                    break;
                case Status.rocket:
                    OPC("Rocket");
                    break;
            }
        }

        public void GameOver(Snake s, Player p, DispatcherTimer dt1, DispatcherTimer dt2, DispatcherTimer dt3)
        {
            //game over esetén minden timer leállítása és minden kaja, játékos törlése
            dt1.Stop();
            dt2.Stop();
            dt3.Stop();

            Ellenseg = null;
            Jatekos = null;
            Kaja = null;
            Rocket = null;
            JatekosPowerUp = "";
            EllensegPowerUp = "";

            if (p == Player.ellenseg)
            {
                EllensegUzenet = "Vesztettél!\nKezdéshez nyomd meg: 'Numpad 0'-t";
                JatekosUzenet = "Nyertél!\nKezdéshez nyomd meg: 'F'-t";
            }
            else if (p == Player.jatekos)
            {
                JatekosUzenet = "Vesztettél!\nKezdéshez nyomd meg: 'F'-t";
                EllensegUzenet = "Nyertél!\nKezdéshez nyomd meg: 'Numpad 0'-t";
            }
        }

        public void JatekosInit()
        {
            Jatekos = new Snake(40, 100, 10, 10, Direction.le, Player.jatekos);
        }

        public void EllensegInit()
        {
            Ellenseg = new Snake(cw-50, ch-250, 10, 10, Direction.le, Player.ellenseg);
        }

        public Snake Jatekos
        {
            get
            {
                return jatekos;
            }

            set
            {
                jatekos = value;
                OPC();
            }
        }

        public Map Map
        {
            get
            {
                return map;
            }

            set
            {
                map = value;
                OPC();
            }
        }

        public Snake Ellenseg
        {
            get
            {
                return ellenseg;
            }

            set
            {
                ellenseg = value;
                OPC();
            }
        }

        public string JatekosUzenet
        {
            get
            {
                return jatekosUzenet;
            }

            set
            {
                jatekosUzenet = value;
                OPC();
            }
        }

        public string EllensegUzenet
        {
            get
            {
                return ellensegUzenet;
            }

            set
            {
                ellensegUzenet = value;
                OPC();
            }
        }

        public Food Kaja
        {
            get
            {
                return kaja;
            }

            set
            {
                kaja = value;
                OPC();
            }
        }

        public Rocket Rocket
        {
            get
            {
                return rocket;
            }

            set
            {
                rocket = value;
                OPC();
            }
        }

        public string Leiras
        {
            get
            {
                return leiras;
            }

            set
            {
                leiras = value;
                OPC();
            }
        }

        public string JatekosPowerUp
        {
            get
            {
                return jatekosPowerUp;
            }

            set
            {
                jatekosPowerUp = value;
                OPC();
            }
        }

        public string EllensegPowerUp
        {
            get
            {
                return ellensegPowerUp;
            }

            set
            {
                ellensegPowerUp = value;
                OPC();
            }
        }

        public string JatekosIranyitas
        {
            get
            {
                return jatekosIranyitas;
            }

            set
            {
                jatekosIranyitas = value;
            }
        }

        public string EllensegIranyitas
        {
            get
            {
                return ellensegIranyitas;
            }

            set
            {
                ellensegIranyitas = value;
            }
        }
    }
}
