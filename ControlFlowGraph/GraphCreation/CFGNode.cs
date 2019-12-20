using System;
using System.Collections.Generic;
using System.Drawing;


namespace ControlFlowGraph
{
    public struct CFGNode
    {
        public CFGNode(string Text, PointF Location, SizeF NodeSize)
        {
            this.Text = Text;
            this.Location = Location;
            ConnectionPoint = new ConnectionPoints(Location, NodeSize);
        }

        public string Text { get; private set; }
        public PointF Location { get; private set; }
        public ConnectionPoints ConnectionPoint { get; private set; }
    }
}
