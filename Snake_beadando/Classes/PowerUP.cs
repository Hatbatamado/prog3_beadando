using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_beadando
{
    class PowerUP
    {
        System.Windows.Input.Key kar;
        string szoveg;
        Status tipus;

        public PowerUP(System.Windows.Input.Key kar, string szoveg, Status tipus)
        {
            this.kar = kar;
            this.szoveg = szoveg;
            this.tipus = tipus;
        }

        public System.Windows.Input.Key Kar
        {
            get
            {
                return kar;
            }
        }

        public string Szoveg
        {
            get
            {
                return szoveg;
            }
        }

        public Status Tipus
        {
            get
            {
                return tipus;
            }
        }

        public override string ToString()
        {
            return kar + " : " + tipus + "\n";
        }
    }
}
