using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFlowGraph
{
    public struct ProgramTextBrushes
    {
        public ProgramTextBrushes(Brush Text, Brush Id, Brush Line)
        {
            this.Text = Text;
            this.Id = Id;
            this.Line = Line;
        }

        public Brush Text { get; set; }
        public Brush Id { get; set; }
        public Brush Line { get; set; }
    }
}
