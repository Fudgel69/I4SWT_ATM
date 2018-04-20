using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    class SeperationEventArgs : EventArgs
    {
        private Time currentTime;
        private string tag1;
        private string tag2;

        public SeperationEventArgs(Time currentTime, string tag1, string tag2)
        {
            this.currentTime = currentTime;
            this.tag1 = tag1;
            this.tag2 = tag2;
        }

        public Time GetTime()
        {
            return currentTime;
        }

        public string GetTags()
        {
            return tag1 + tag2;
        }
    }
}
