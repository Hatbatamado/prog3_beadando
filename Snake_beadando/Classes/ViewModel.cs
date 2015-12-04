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
        string ellensegUzenet;
        int cw;
        int ch;
        
        public ViewModel(int cw, int ch)
        {
            Map = new Map(cw, ch);
            JatekosUzenet = "Kezdéshez nyomd meg: 'F'-t";
            EllensegUzenet = "Kezdéshez nyomd meg: 'Numpad 0'-t";
            this.ch = ch;
            this.cw = cw;
        }

        public void GameOver(Snake s, Player p)
        {
            if (p == Player.ellenseg)
            {
                Ellenseg = null;
                EllensegUzenet = "Vesztettél!\nKezdéshez nyomd meg: 'Numpad 0'-t";
                JatekosUzenet = "Nyertél!\nKezdéshez nyomd meg: 'F'-t";
            }
            else if (p == Player.jatekos)
            {
                Jatekos = null;
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
            Ellenseg = new Snake(cw-50, ch-250, 10, 10, Direction.fel, Player.ellenseg);
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
    }
}
