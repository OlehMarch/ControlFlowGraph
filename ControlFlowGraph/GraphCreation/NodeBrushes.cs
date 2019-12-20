using System;
using System.Collections.Generic;
using System.Drawing;


namespace ControlFlowGraph
{
    public struct NodeBrushes
    {
        public NodeBrushes(Brush Fill, Brush Outline, Brush Pen)
        {
            this.Fill = Fill;
            this.Outline = Outline;
            this.Pen = Pen;
        }

        public Brush Fill { get; set; }
        public Brush Outline { get; set; }
        public Brush Pen { get; set; }
    }
}
