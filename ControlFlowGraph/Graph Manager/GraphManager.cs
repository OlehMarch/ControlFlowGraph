using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ControlFlowGraph
{
    public static class GraphManager
    {
        private static uint widthCoefficient; // Coefficient of offset to the right 
        private static uint depthCoefficient; // How deep is graph 
        private const int X_START = 10;
        private const int Y_START = 10;
        private const int X_STEP = 100;
        private const int Y_STEP = 90;

        static GraphManager()
        {
            widthCoefficient = 0;
            depthCoefficient = 0;
        }

        private static void IncreaseDepth()
        {
            widthCoefficient = 0;
            depthCoefficient++;
        }

        public static void BuildGraph(CFGraph graph, string[] nodes)
        {
            if (nodes == null)
            {
                throw new ArgumentException("Null reference on node array");
            }
            if (nodes.Length == 0)
            {
                return;
            }

            // Save created nodes in the stack
            using (var stack = new NodeStack())
            {
                // Go through the string array
                for (int i = 0; i < nodes.Length; i++)
                {
                    char leftNode = nodes[i][0];
                    char rightNode = nodes[i][nodes[i].Length - 1];

                    // Если узел слева уже создан
                    if (stack.Exists(leftNode))
                    {
                        // Проверка : был ли раньше узел слева
                        bool temp = false;
                        for (int j = 0; j < i; j++)
                        {
                            if (nodes[j][0] == leftNode)
                            {
                                temp = true;
                                break;
                            }
                        }

                        if (stack.Exists(rightNode))
                        {
                            // TODO левый узел соединить кривой с правым узлом 
                            graph.AddConnectionCurve(leftNode.ToString(), rightNode.ToString(), ConnectionSide.Right);
                        }
                        else
                        {
                            if (temp == false)
                            {
                                // опускаемся ниже в графе
                                IncreaseDepth();
                                // TODO создать узел справа
                                var x_coord = (X_START + (X_STEP * widthCoefficient));
                                var y_coord = (Y_START + (Y_STEP * depthCoefficient));
                                graph.AddNode(rightNode.ToString(), new PointF(x_coord, y_coord));
                                stack.AddNode(rightNode);
                                // TODO connect left and right
                                graph.AddConnectionLine(leftNode.ToString(), rightNode.ToString());
                            }
                            else
                            {
                                // идем в ширину в графе
                                widthCoefficient++;
                                // TODO создаем узел справа
                                var x_coord = (X_START + (X_STEP * widthCoefficient));
                                var y_coord = (Y_START + (Y_STEP * depthCoefficient));
                                graph.AddNode(rightNode.ToString(), new PointF(x_coord, y_coord));
                                stack.AddNode(rightNode);
                                // TODO connect left and right
                                graph.AddConnectionLine(leftNode.ToString(), rightNode.ToString());
                            }
                        }
                    }
                    else
                    {
                        if (stack.Exists(rightNode))
                        {
                            // TODO создаем LeftNode
                            var x_coord = (X_START + (X_STEP * widthCoefficient));
                            var y_coord = (Y_START + (Y_STEP * depthCoefficient));
                            graph.AddNode(leftNode.ToString(), new PointF(x_coord, y_coord));
                            stack.AddNode(leftNode);
                            // TODO connect left and right
                            graph.AddConnectionLine(leftNode.ToString(), rightNode.ToString());
                        }
                        else
                        {
                            IncreaseDepth();
                            // TODO создаем Left Node
                            var x_coord = (X_START + (X_STEP * widthCoefficient));
                            var y_coord = (Y_START + (Y_STEP * depthCoefficient));
                            graph.AddNode(leftNode.ToString(), new PointF(x_coord, y_coord));
                            stack.AddNode(leftNode);
                            depthCoefficient++;
                            // TODO создаем Right Node
                            y_coord = (Y_START + (Y_STEP * depthCoefficient));
                            graph.AddNode(rightNode.ToString(), new PointF(x_coord, y_coord));
                            stack.AddNode(rightNode);
                            // TODO connect left and right
                            graph.AddConnectionLine(leftNode.ToString(), rightNode.ToString());
                        }
                    }
                }
            }
            // prepare for next graph build
            depthCoefficient = 0;
            widthCoefficient = 0;
            // finish drawing
            graph.EndOfDraw();
        }
    }
}
