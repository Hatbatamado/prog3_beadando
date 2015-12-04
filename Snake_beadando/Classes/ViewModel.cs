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
        String jatekosUzenet;

        //todo: VM statik lekérés ez alapján a kódok átírása

        public ViewModel(int cw, int ch)
        {
            jatekos.JatekosInit();
            Map = new Map(cw, ch);
            Ellenseg = new Snake(0, 0, 10, 10);
            JatekosUzenet = "Kezdéshez nyomd meg: 'S'";
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
            }
        }

        internal Snake Ellenseg
        {
            get
            {
                return ellenseg;
            }

            set
            {
                ellenseg = value;
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
