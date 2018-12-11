using System;
using System.Collections.Generic;
using System.Text;

namespace TowerOfHanoi.Model
{
    public class Tower
    {
        private Stack<Disc> _Discs;

        public Tower() : this(0)
        {

        }

        internal Tower(int discCount)
        {
            _Discs = new Stack<Disc>(discCount);
            for (int i = 0; i < discCount; i++)
            {
                LayDiscOnTop(new Disc(discCount - i));
            }
        }

        public void LayDiscOnTop(Disc Disc)
        {
            if ((TopLevelDisc?.Diameter ?? int.MaxValue) <= Disc.Diameter)
                throw new DiscTooGreatException();
            _Discs.Push(Disc);
        }

        public Disc TopLevelDisc
        {
            get
            {
                if (_Discs.Count == 0)
                {
                    return null;
                }
                return _Discs.Peek();
            }
        }

        public Disc TakeDisc()
        {
            if (TopLevelDisc == null)
            {
                throw new TowerEmptyException();
            }
            var Disc = TopLevelDisc;
            _Discs.Pop();
            return Disc;
        }
    }
}
