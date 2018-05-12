using SimpleNeuralNetwork.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleNeuralNetwork.Layers
{
    public class WeightLayer
    {
        public MatrixFloat Weights { get; set; }
        public MatrixFloat Gradient { get; set; }

        public WeightLayer(NeuronLayer prev, NeuronLayer next)
        {
            Weights = new MatrixFloat(prev.NumberOfNeurons, next.NumberOfNeurons);
            for (int row = 0; row < prev.NumberOfNeurons; row++)
            {
                for (int column = 0; column < next.NumberOfNeurons; column++)
                {
                    Weights[row, column] = Helper.random.Range(-1f, 1f);
                }
            }
            Gradient = new MatrixFloat(prev.NumberOfNeurons, next.NumberOfNeurons);
        }
    }
}
