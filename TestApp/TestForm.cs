using ControlFlowGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TestApp
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();

            string cCode = "int main() {\n\tint abc; switch(abc) { case 1:{i++;break;} case 2:{i++;break;} default:{asd--;break;} } \n\treturn 0; }\n";
            //string cCode = "if (true) printf(\"k\"); else { printf(\"no\"); cout << \"awsm\"; }";
            //string cCode = "while (true) { ok1(); ok2();  goto someLBL; DEADStatement;  someLBL:   stat1; stat2;}";
            //string cCode = "while (true) {switch (c) {	case 1: break;	case 2: continue;	}	break;}";

            CFGParserWrapper.SetCodeToParse(cCode);

            #region Graph
            graph = new CFGraph(PB_Graph, 40);

            GraphManager.BuildGraph(graph, CFGParserWrapper.GetPairs());
            #endregion

            #region Code
            int offsetV = 40;

            master = new ProgramTextMaster(PB_Code, CFGParserWrapper.GetParsedCode(), 
                new ProgramTextBrushes(Brushes.Black, Brushes.DarkRed, Brushes.DarkGray));
            master.CreateProgramText(offsetV);
            #endregion
        }


        private ProgramTextMaster master;
        private CFGraph graph;


        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            graph.Dispose();
            master.Dispose();
        }

        private void B_Save_Click(object sender, EventArgs e)
        {
            master.SaveToBitmap(Environment.CurrentDirectory, "programCode.jpg");
        }
    }
}
