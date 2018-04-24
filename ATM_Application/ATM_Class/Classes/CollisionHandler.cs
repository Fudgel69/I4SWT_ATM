using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public class CollisionHandler : ICollisionHandler
    {
        private IList<ICollision> _collisionList = new List<ICollision>();
        private IList<ICollision> _tempCollisionList = new List<ICollision>();


        public void CollisionCheck(IList<ITrack> list)
        {
            //logik til at tjekke om listen er tom for fly
            if (list.Count == 0)

            // logik til at tjekke om der er nogle fly i fare for kollision
            _tempCollisionList = new List<ICollision>();

            foreach (ITrack t in list)
            {
                if(!list.Any(d => (t.CurrentPosition.X ))) /* logik som tjekker at:
                                                        vertical x < 300
                                                        horizontal y < 5000
                                                               */ 
                _tempCollisionList.Add(new Collision(t.Tag, t.Tag, DateTime.Now));
            }

            NewCollision();
            //logger af collision til fil
        }

        private void NewCollision()
        {
            //leder efter nye collisions
            var findCollisions = (from findCollision in _tempCollisionList
                where !_collisionList.Any(
                    testCollision => testCollision.CollisionTag.Equals(findCollision.CollisionTag))
                select findCollision).ToList();


            //beholder de gamle kollisions
            var currentCollisions = (from testCollision in _collisionList
                where _tempCollisionList.Any(findCollision =>
                    findCollision.CollisionTag.Equals(testCollision.CollisionTag))
                select testCollision).ToList(); 

            //tilføjer nye collisions bag på nuværende
            currentCollisions.AddRange(findCollisions);
            _collisionList = currentCollisions;
        }
    }
}