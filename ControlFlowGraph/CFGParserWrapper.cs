using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ControlFlowGraph
{
    public static class CFGParserWrapper
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct CodeAndPairs
        {
            public string code;
            public string pairs;
        }

        private static CodeAndPairs parsedData = new CodeAndPairs();


        [DllImport("CFGParserDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void ParseCodeForCFG(ref CodeAndPairs cpstruct, string code_in);

        public static void SetCodeToParse(string code)
        {
            ParseCodeForCFG(ref parsedData, code);
        }

        public static string GetParsedCode()
        {
            return parsedData.code;
        }

        public static string[] GetPairs()
        {
            string[] temp = parsedData.pairs.Replace("\n", "").Split(';');
            string[] pairs = new string[temp.Length - 1];

            for (int i = 0; i < pairs.Length; i++)
            {
                pairs[i] = temp[i];
            }

            return pairs;
        }

    }
}
