using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public class Collision : ICollision
    {
        public DateTime TimeOccurence { get; }
        public List<string> CollisionTag { get; } = new List<string>();


        //constructor
        public Collision(string s1, string s2, DateTime time)
        {
            CollisionTag.Add(s1);
            CollisionTag.Add(s2);
            TimeOccurence = time;
        }

        /*
        public override string ToString()
        {
            
        }
        */
    }
}
