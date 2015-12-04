using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_beadando
{
    class ViewModel : Bindable
    {
        Snake jatekos;
        Snake ellenseg;
        Map map;
        string jatekosUzenet;
        //todo: VM statik lekérés ez alapján a kódok átírása

        public ViewModel(int cw, int ch)
        {
            jatekos = new Snake();
            Map = new Map(cw, ch);
            Ellenseg = new Snake(0, 0, 10, 10, Direction.fel, Player.ellenseg);
            JatekosUzenet = "Kezdéshez nyomd meg: 'S'";
        }

        public void GameOver(Snake s, Player p)
        {
            if (p == Player.ellenseg)
            {
                Ellenseg = null;
            }
            else if (p == Player.jatekos)
            {
                Jatekos = null;
                JatekosUzenet = "Vesztettél!\nKezdéshez nyomd meg: 'S'";
            }
        }

        public void JatekosInit()
        {
            jatekos = new Snake(40, 100, 10, 10, Direction.le, Player.jatekos);
            OPC("Jatekos");
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
    }
}
