using SimpleNeuralNetwork.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleNeuralNetwork.Layers
{
    public class Input
    {
        public int NumberOfNeurons { get; }

        public IActivationFunction ActivationFunction { get; }
        public MatrixFloat Neurons { get; set; }
        public MatrixFloat Gradient { get; set; }

        private MatrixFloat _bias;

        public Input(int neurons, IActivationFunction activationFunction)
        {
            NumberOfNeurons = neurons;
            Neurons = new MatrixFloat(1, NumberOfNeurons);
            Gradient = new MatrixFloat(1, NumberOfNeurons);
            _bias = new MatrixFloat(1, NumberOfNeurons);
            ActivationFunction = activationFunction;

            for (int i = 0; i < _bias.Columns; i++)
            {
                _bias[0, i] = 1;
            }
        }

        public void ApplyGradToBias()
        {
            var update = _bias.Transpose().Multiply(Gradient);
            _bias -= update;
        }

        public MatrixFloat Activate()
        {
            return (Neurons + _bias).Apply((x) => ActivationFunction.Activate(x));
        }

        public MatrixFloat ActivateDeriv()
        {
            return (Neurons + _bias).Apply((x) => ActivationFunction.ActivateDeriv(x));
        }
    }
}
