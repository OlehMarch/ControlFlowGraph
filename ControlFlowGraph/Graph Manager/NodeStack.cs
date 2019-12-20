using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFlowGraph
{
    public sealed class NodeStack : IDisposable
    {
        private List<char> stack;

        public NodeStack()
        {
            this.stack = new List<char>();
        }

        public void AddNode(char nodeName)
        {
            if (this.stack.Exists(ch => ch == nodeName))
            {
                throw new ArgumentException("You can't add already existing node.");
            }
            this.stack.Add(nodeName);
        }

        public bool Exists(char nodeName)
        {
            return this.stack.Exists(ch => ch == nodeName);
        }

        public void Dispose()
        {
            this.stack.Clear();
            this.stack = null;
        }
    }
}
