using SimpleNeuralNetwork.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleNeuralNetwork.Layers
{
    public class NeuronLayer
    {
        public int NumberOfNeurons { get; }

        public IActivationFunction ActivationFunction { get; }
        public MatrixFloat Neurons { get; set; }
        public MatrixFloat Gradient { get; set; }

        public NeuronLayer(int neurons, IActivationFunction activationFunction)
        {
            NumberOfNeurons = neurons;
            Neurons = new MatrixFloat(1, NumberOfNeurons);
            Gradient = new MatrixFloat(1, NumberOfNeurons);
            ActivationFunction = activationFunction;
        }

        public MatrixFloat Activate()
        {
            return Neurons.Apply((x) => ActivationFunction.Activate(x));
        }

        public MatrixFloat ActivateDeriv()
        {
            return Neurons.Apply((x) => ActivationFunction.ActivateDeriv(x));
        }
    }
}
