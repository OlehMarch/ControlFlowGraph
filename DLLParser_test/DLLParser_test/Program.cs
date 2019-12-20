using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DLLParser_test
{
    class Program
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct CodeAndPairs
        {
            public string code;
            public string pairs;
        }


        [DllImport("C:\\Users\\All\\Documents\\Visual Studio 2013\\Projects\\lr1_SoftEngineering_parser\\Release\\CFGParserDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ParseCodeForCFG(ref CodeAndPairs cp, string code_in);
        
        
        static void Main(string[] args)
        {
            string code_in = "void main() { switch (c){	case '1': {	cout << someVar.str();	break;}	case '2': 	case '3': cout << ok();	default: cout << error;} }";

            CodeAndPairs cp = new CodeAndPairs();

            ParseCodeForCFG(ref cp, code_in);

            Console.WriteLine("OUTPUT CODE:\n");
            Console.WriteLine(cp.code);
  
            Console.WriteLine("CFG PAIRS:\n");
            Console.WriteLine(cp.pairs);

            Console.ReadKey();
        }
    }
}
