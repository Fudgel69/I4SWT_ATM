using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    //Klassen position der består af flyet som vist i et 3-dimentionelt plan, hvor x- y- og z/altitude-koordinaterne er flyets placering
    public class Position : IPosition
    {
        private int _X { get; set; }
        private int _Y { get; set; }
        private int _Altitude { get; set; }

        //Get- og Set-metoder til de private members
        #region Get-/Set-Metoder

        public int X
        {
            get => _X;
            set => _X = value;
        }

        public int Y
        {
            get => _Y;
            set => _Y = value;
        }

        public int Altitude
        {
            get => _Altitude;
            set => _Altitude = value;
        }

        #endregion

    }
}
