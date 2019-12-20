using System;
using System.Collections.Generic;
using System.Drawing;


namespace ControlFlowGraph
{
    public struct ConnectionPoints
    {
        public ConnectionPoints(PointF Location, SizeF NodeSize)
        {
            Top = new PointF(Location.X + NodeSize.Width / 2, Location.Y + 0);
            Bottom = new PointF(Location.X + NodeSize.Width / 2, Location.Y + NodeSize.Height);
            Left = new PointF(Location.X + 0, Location.Y + NodeSize.Height / 2);
            Right = new PointF(Location.X + NodeSize.Width, Location.Y + NodeSize.Height / 2);
        }

        public PointF Top { get; private set; }
        public PointF Bottom { get; private set; }
        public PointF Left { get; private set; }
        public PointF Right { get; private set; }
    }
}
