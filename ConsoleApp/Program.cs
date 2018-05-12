using SimpleNeuralNetwork;
using SimpleNeuralNetwork.ActivationFunctions;
using SimpleNeuralNetwork.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var activation = new Relu();

            var n = new FeedForward();

            n.AddLayer(new NeuronLayer(2, activation));
            n.AddLayer(new NeuronLayer(3, activation));
            n.AddLayer(new NeuronLayer(1, activation));

            n.Construct();

            var str = "";
            for (int i = 0; i < n.Output.Length; i++)
            {
                str += n.Output[i].ToString() + "\n";
            }

            var inp = new MatrixFloat[] {
                new MatrixFloat(new float[,] { { 1, 1 } }),
                new MatrixFloat(new float[,] { { 0, 1 } }),
                new MatrixFloat(new float[,] { { 1, 0 } }),
                new MatrixFloat(new float[,] { { 0, 0 } }),
            };

            var expected = new MatrixFloat[] {
                new MatrixFloat(new float[,] { { 0 } }),
                new MatrixFloat(new float[,] { { 1 } }),
                new MatrixFloat(new float[,] { { 1 } }),
                new MatrixFloat(new float[,] { { 0 } })
            };

            for (int i = 0; i < 500000; i++)
            {
                var error = 0f;
                for (int k = 0; k < 4; k++)
                {
                    var np1 = inp[k];
                    for (int j = 0; j < np1.Columns; j++)
                    {
                        n.Input[j] = np1[0, j];
                    }
                    var exp = expected[k];

                    n.Forward();

                    for (int z = 0; z < exp.Columns; z++)
                    {
                        var tmp = (n.Output[z] - exp[0, z]);
                        error += tmp * tmp;
                    }

                    n.Backward(exp);

                    n.Clear();
                }
                Console.WriteLine(error);
            }

            Console.ReadKey();
        }
    }
}
