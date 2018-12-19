using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TowerOfHanoi.Model
{
    public class Tower
    {
        private Stack<Disc> _discs;

        public Tower() : this(0)
        {

        }

        internal Tower(int discCount)
        {
            _discs = new Stack<Disc>(discCount);
            for (int i = 0; i < discCount; i++)
            {
                LayDiscOnTop(new Disc(discCount - i));
            }
        }

        public void LayDiscOnTop(Disc Disc)
        {
            if ((TopLevelDisc?.Diameter ?? int.MaxValue) <= Disc.Diameter)
                throw new DiscTooGreatException();
            _discs.Push(Disc);
        }

        public Disc TopLevelDisc
        {
            get
            {
                if (_discs.Count == 0)
                {
                    return null;
                }
                return _discs.Peek();
            }
        }

        /// <summary>
        /// Gets all the discs in the tower, from bottom to top.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Disc> DiscsFromBottomToTop()
        {
            return _discs.AsEnumerable();
        }

        public Disc TakeDisc()
        {
            if (TopLevelDisc == null)
            {
                throw new TowerEmptyException();
            }
            var Disc = TopLevelDisc;
            _discs.Pop();
            return Disc;
        }
    }
}
