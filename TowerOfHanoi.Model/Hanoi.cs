using System;
using System.Collections.Generic;
using System.Text;

namespace TowerOfHanoi.Model
{
    public class Hanoi
    {
        public Hanoi(int discCount)
        {
            if (discCount < 0 || discCount > 100)
                throw new InvalidDiscCountException();
            if (discCount == 0)
                throw new TowerEmptyException();
            StartTower = new Tower(discCount);
            MiddleTower = new Tower();
            EndTower = new Tower();
        }


        public Tower StartTower { get; set; }
        public Tower MiddleTower { get; set; }
        public Tower EndTower { get; set; }

    }
}
